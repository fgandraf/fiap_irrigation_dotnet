using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts.Repositories;

public interface IScheduleRepository
{
    Task<OperationResult<ListView>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<ScheduleView>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(ScheduleCreate model);
    Task<OperationResult> UpdateAsync(ScheduleUpdate model);
    Task<OperationResult> DeleteAsync(int id);
}