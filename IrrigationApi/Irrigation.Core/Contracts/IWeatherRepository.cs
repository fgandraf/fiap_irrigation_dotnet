using Irrigation.Core.ViewModels;

namespace Irrigation.Core.Contracts;

public interface IWeatherRepository
{
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<WeatherViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(WeatherViewModel model);
    Task<OperationResult> UpdateAsync(WeatherViewModel model);
    Task<OperationResult> DeleteAsync(int id);
}