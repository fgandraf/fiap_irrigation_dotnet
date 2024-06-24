using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using Irrigation.Core.ViewModels.View;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Repositories.Database;

public class SensorRepository(IrrigationDataContext context) : ISensorRepository
{
    public async Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize)
    {
        var count = await context.Sensors.AsNoTracking().CountAsync();
        
        var sensors = await context
            .Sensors
            .AsNoTracking()
            .Include(x => x.Area)
            .Include(x => x.Weathers)
            .Include(x => x.Notifications)
            .Select(sensor => new SensorViewModel
                (
                    sensor.Id,
                    sensor.Type,
                    sensor.Location,
                    sensor.Area.Id,
                    sensor.Weathers.Select(x => x.Id).ToList(),
                    sensor.Notifications.Select(x => x.Id).ToList()
                )
            )
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (sensors.Count == 0)
            return OperationResult<dynamic>.FailureResult("No sensors registered!");
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            sensors
        });
    }
    
    public async Task<OperationResult<SensorViewModel>> GetByIdAsync(int id)
    {
        var sensor = await context
            .Sensors
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.Area)
            .Include(x => x.Weathers)
            .Include(x => x.Notifications)
            .Select(sensor => new SensorViewModel
                (
                    sensor.Id,
                    sensor.Type,
                    sensor.Location,
                    sensor.Area.Id,
                    sensor.Weathers.Select(x => x.Id).ToList(),
                    sensor.Notifications.Select(x => x.Id).ToList()
                )
            )
            .FirstOrDefaultAsync();
        
        if (sensor is null)
            return OperationResult<SensorViewModel>.FailureResult($"Sensor '{id}' not found!");
        
        return OperationResult<SensorViewModel>.SuccessResult(sensor);
    }
    
    public async Task<OperationResult<int>> InsertAsync(SensorViewModel model)
    {
        var sensor = new Sensor
        {
            Type = model.Type,
            Location = model.Location,
            Area = await context.Areas.Where(x => x.Id == model.AreaId).FirstOrDefaultAsync(),
            Weathers = await context.Weathers.Where(x => model.WeathersId.Contains(x.Id)).ToListAsync(),
            Notifications = await context.Notifications.Where(x => model.NotificationsId.Contains(x.Id)).ToListAsync()
        };
        
        context.Sensors.Add(sensor);
        var id = await context.SaveChangesAsync();
        
        return id > 0 ? OperationResult<int>.SuccessResult(id) : OperationResult<int>.FailureResult("Unable to add sensor!");
    }
    
    public async Task<OperationResult> UpdateAsync(SensorViewModel model)
    {
        var sensor = await context.Sensors.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        sensor.Type = model.Type;
        sensor.Location = model.Location;
        sensor.Area = await context.Areas.Where(x => x.Id == model.AreaId).FirstOrDefaultAsync();
        sensor.Weathers = await context.Weathers.Where(x => model.WeathersId.Contains(x.Id)).ToListAsync();
        sensor.Notifications = await context.Notifications.Where(x => model.NotificationsId.Contains(x.Id)).ToListAsync();
        
        context.Sensors.Update(sensor);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter sensor!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var sensor = await context.Sensors.Where(x => x.Id == id).FirstOrDefaultAsync();
        
        context.Sensors.Remove(sensor);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to delete sensor!");
    }
}