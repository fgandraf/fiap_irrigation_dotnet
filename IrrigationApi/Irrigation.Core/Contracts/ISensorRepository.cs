using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts;

public interface ISensorRepository
{
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<SensorViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(SensorViewModel model);
    Task<OperationResult> UpdateAsync(SensorViewModel model);
    Task<OperationResult> DeleteAsync(int id);
}