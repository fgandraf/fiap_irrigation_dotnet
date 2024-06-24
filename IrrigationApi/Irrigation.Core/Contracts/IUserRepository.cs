using Irrigation.Core.Models;
using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;

namespace Irrigation.Core.Contracts;

public interface IUserRepository
{
    Task<OperationResult<User>> GetByLoginAsync(UserLoginViewModel model);
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<UserViewModel>> GetByEmailAsync(string address);
    Task<OperationResult<UserViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(UserCreate model);
    Task<OperationResult> UpdateAsync(UserUpdateInfo model);
    Task<OperationResult> ActivateAsync(int id);
    Task<OperationResult> DeactivateAsync(int id);
    Task<OperationResult> ChangePermission(int userId, int permissionId);
}