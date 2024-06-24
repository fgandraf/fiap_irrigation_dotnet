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
            .Select(sensor => new SensorView
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
            return OperationResult<dynamic>.FailureResult();
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            sensors
        });
    }
    
    public async Task<OperationResult<SensorView>> GetByIdAsync(int id)
    {
        var sensor = await context
            .Sensors
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.Area)
            .Include(x => x.Weathers)
            .Include(x => x.Notifications)
            .Select(sensor => new SensorView
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
            return OperationResult<SensorView>.FailureResult();
        
        return OperationResult<SensorView>.SuccessResult(sensor);
    }
    
    public async Task<OperationResult<int>> InsertAsync(SensorCreate model)
    {
        var area = await context.Areas.Where(x => x.Id == model.AreaId).FirstOrDefaultAsync();
        if (area is null)
            return OperationResult<int>.FailureResult($"Area {model.AreaId} not found!");
        
        var sensor = new Sensor
        {
            Type = model.Type,
            Location = model.Location,
            Area = area,
            Weathers = await context.Weathers.Where(x => model.WeathersId.Contains(x.Id)).ToListAsync(),
            Notifications = await context.Notifications.Where(x => model.NotificationsId.Contains(x.Id)).ToListAsync()
        };
        
        context.Sensors.Add(sensor);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult<int>.SuccessResult(sensor.Id) : OperationResult<int>.FailureResult("Unable to add sensor!");
    }
    
    public async Task<OperationResult> UpdateAsync(SensorUpdate model)
    {
        var sensor = await context.Sensors.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        if (sensor is null)
            return OperationResult.FailureResult($"Sensor {model.Id} not found!");
        
        var area = await context.Areas.Where(x => x.Id == model.AreaId).FirstOrDefaultAsync();
        if (area is null)
            return OperationResult<int>.FailureResult($"Area {model.AreaId} not found!");
        
        sensor.Type = model.Type;
        sensor.Location = model.Location;
        sensor.Area = area;
        sensor.Weathers = await context.Weathers.Where(x => model.WeathersId.Contains(x.Id)).ToListAsync();
        sensor.Notifications = await context.Notifications.Where(x => model.NotificationsId.Contains(x.Id)).ToListAsync();
        
        context.Sensors.Update(sensor);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter sensor!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var sensor = await context.Sensors.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (sensor is null)
            return OperationResult.FailureResult($"Sensor {id} not found!");
        
        context.Sensors.Remove(sensor);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to delete sensor!");
    }
}