using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts.Repositories;

public interface ISensorRepository
{
    Task<OperationResult<ListView>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<SensorView>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(SensorCreate model);
    Task<OperationResult> UpdateAsync(SensorUpdate model);
    Task<OperationResult> DeleteAsync(int id);
}