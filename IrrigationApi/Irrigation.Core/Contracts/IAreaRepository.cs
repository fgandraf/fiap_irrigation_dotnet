using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts;

public interface IAreaRepository
{
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<AreaViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(AreaViewModel model);
    Task<OperationResult> UpdateAsync(AreaViewModel model);
    Task<OperationResult> DeleteAsync(int id);
}