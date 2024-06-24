using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts;

public interface IAreaRepository
{
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<AreaViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(AreaCreate model);
    Task<OperationResult> UpdateAsync(AreaUpdate model);
    Task<OperationResult> DeleteAsync(int id);
}