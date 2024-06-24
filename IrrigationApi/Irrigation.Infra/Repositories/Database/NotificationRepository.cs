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
            .Select(notification => new NotificationViewModel
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
            return OperationResult<dynamic>.FailureResult("No notifications registered!");
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            notifications
        });
    }
    
    public async Task<OperationResult<NotificationViewModel>> GetByIdAsync(int id)
    {
        var notification = await context
            .Notifications
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.Sensor)
            .Select(notification => new NotificationViewModel
                (
                    notification.Id,
                    notification.Description,
                    notification.Timestamp,
                    notification.Sensor.Id
                )
            )
            .FirstOrDefaultAsync();
        
        if (notification is null)
            return OperationResult<NotificationViewModel>.FailureResult($"Notification '{id}' not found!");
        
        return OperationResult<NotificationViewModel>.SuccessResult(notification);
    }
    
    public async Task<OperationResult<int>> InsertAsync(NotificationCreate model)
    {
        var notification = new Notification
        {
            Description = model.Description,
            Timestamp = model.Timestamp,
            Sensor = await context.Sensors.Where(x => x.Id == model.SensorId).FirstOrDefaultAsync()
        };
        
        context.Notifications.Add(notification);
        var id = await context.SaveChangesAsync();
        
        return id > 0 ? OperationResult<int>.SuccessResult(id) : OperationResult<int>.FailureResult("Unable to add notification!");
    }
    
    public async Task<OperationResult> UpdateAsync(NotificationUpdate model)
    {
        var notification = await context.Notifications.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        notification.Description = model.Description;
        notification.Timestamp = model.Timestamp;
        notification.Sensor = await context.Sensors.Where(x => x.Id == model.SensorId).FirstOrDefaultAsync();
        
        context.Notifications.Update(notification);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter notification!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var notification = await context.Notifications.Where(x => x.Id == id).FirstOrDefaultAsync();
        
        context.Notifications.Remove(notification);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to delete notification!");
    }
}