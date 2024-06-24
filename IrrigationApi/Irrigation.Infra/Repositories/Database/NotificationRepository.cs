using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Repositories.Database;

public class NotificationRepository(IrrigationDataContext context) : INotificationRepository
{
    public async Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize)
    {
        var count = await context.Notifications.AsNoTracking().CountAsync();
        
        var notifications = await context
            .Notifications
            .AsNoTracking()
            .Include(x => x.Sensor)
            .Select(notification => new NotificationView
                (
                    notification.Id,
                    notification.Description,
                    notification.Timestamp,
                    notification.Sensor.Id
                )
            )
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (notifications.Count == 0)
            return OperationResult<dynamic>.FailureResult();
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            notifications
        });
    }
    
    public async Task<OperationResult<NotificationView>> GetByIdAsync(int id)
    {
        var notification = await context
            .Notifications
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.Sensor)
            .Select(notification => new NotificationView
                (
                    notification.Id,
                    notification.Description,
                    notification.Timestamp,
                    notification.Sensor.Id
                )
            )
            .FirstOrDefaultAsync();
        
        if (notification is null)
            return OperationResult<NotificationView>.FailureResult();
        
        return OperationResult<NotificationView>.SuccessResult(notification);
    }
    
    public async Task<OperationResult<int>> InsertAsync(NotificationCreate model)
    {
        var sensor = await context.Sensors.Where(x => x.Id == model.SensorId).FirstOrDefaultAsync();
        if (sensor is null)
            return OperationResult<int>.FailureResult($"Sensor {model.SensorId} not found!");
        
        var notification = new Notification
        {
            Description = model.Description,
            Timestamp = model.Timestamp,
            Sensor = sensor
        };
        
        context.Notifications.Add(notification);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult<int>.SuccessResult(notification.Id) : OperationResult<int>.FailureResult("Unable to add notification!");
    }
    
    public async Task<OperationResult> UpdateAsync(NotificationUpdate model)
    {
        var notification = await context.Notifications.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        if (notification is null)
            return OperationResult.FailureResult($"Notification {model.Id} not found!");
        
        var sensor = await context.Sensors.Where(x => x.Id == model.SensorId).FirstOrDefaultAsync();
        if (sensor is null)
            return OperationResult<int>.FailureResult($"Sensor {model.SensorId} not found!");
        
        notification.Description = model.Description;
        notification.Timestamp = model.Timestamp;
        notification.Sensor = sensor;
        
        context.Notifications.Update(notification);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter notification!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var notification = await context.Notifications.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (notification is null)
            return OperationResult.FailureResult($"Notification {id} not found!");
        
        context.Notifications.Remove(notification);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to delete notification!");
    }
}