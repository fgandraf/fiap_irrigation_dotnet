using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts;

public interface IWeatherRepository
{
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<WeatherViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(WeatherCreate model);
    Task<OperationResult> UpdateAsync(WeatherUpdate model);
    Task<OperationResult> DeleteAsync(int id);
}