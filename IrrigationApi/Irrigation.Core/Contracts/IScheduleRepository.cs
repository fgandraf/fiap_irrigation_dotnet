using Irrigation.Core.ViewModels;

namespace Irrigation.Core.Contracts;

public interface IScheduleRepository
{
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<ScheduleViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(ScheduleViewModel model);
    Task<OperationResult> UpdateAsync(ScheduleViewModel model);
    Task<OperationResult> DeleteAsync(int id);
}