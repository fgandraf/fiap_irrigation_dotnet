using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using Irrigation.Core.ViewModels.View;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Repositories.Database;

public class WeatherRepository(IrrigationDataContext context) : IWeatherRepository
{
    public async Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize)
    {
        var count = await context.Weathers.AsNoTracking().CountAsync();
        
        var weathers = await context
            .Weathers
            .AsNoTracking()
            .Include(x => x.Sensor)
            .Select(weather => new WeatherViewModel
                (
                    weather.Id,
                    weather.Timestamp,
                    weather.Temperature,
                    weather.Humidity,
                    weather.Description,
                    weather.Sensor.Id
                )
            )
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (weathers.Count == 0)
            return OperationResult<dynamic>.FailureResult("No weather registered!");
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            weathers
        });
    }
    
    public async Task<OperationResult<WeatherViewModel>> GetByIdAsync(int id)
    {
        var weather = await context
            .Weathers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(y => y.Sensor)
            .Select(weather => new WeatherViewModel
                (
                    weather.Id,
                    weather.Timestamp,
                    weather.Temperature,
                    weather.Humidity,
                    weather.Description,
                    weather.Sensor.Id
                )
            )
            .FirstOrDefaultAsync();
        
        if (weather is null)
            return OperationResult<WeatherViewModel>.FailureResult($"Weather '{id}' not found!");
        
        return OperationResult<WeatherViewModel>.SuccessResult(weather);
    }
    
    public async Task<OperationResult<int>> InsertAsync(WeatherViewModel model)
    {
        var weather = new Weather
        {
            Timestamp = model.Timestamp,
            Temperature = model.Temperature,
            Humidity = model.Humidity,
            Description = model.Description,
            Sensor = context.Sensors.FirstOrDefault(x => x.Id == model.SensorId)
        };
        
        context.Weathers.Add(weather);
        var id = await context.SaveChangesAsync();
        return id > 0 ? OperationResult<int>.SuccessResult(id) : OperationResult<int>.FailureResult("Unable to add weather!");
    }
    
    public async Task<OperationResult> UpdateAsync(WeatherViewModel model)
    {
        var weather = await context.Weathers.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        weather.Humidity = model.Humidity;
        weather.Temperature = model.Temperature;
        weather.Humidity = model.Humidity;
        weather.Description = model.Description;
        weather.Sensor = context.Sensors.FirstOrDefault(x => x.Id == model.SensorId);
        
        context.Weathers.Update(weather);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter weather!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var weather = await context.Weathers.Where(x => x.Id == id).FirstOrDefaultAsync();
        context.Weathers.Remove(weather);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to delete weather!");
    }
}