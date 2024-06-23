using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;

namespace Irrigation.Core.Contracts;

public interface IUserRepository
{
    Task<OperationResult<User>> GetByLoginAsync(UserLoginViewModel model);
    Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize);
    Task<OperationResult<UserViewModel>> GetByEmailAsync(string address);
    Task<OperationResult<UserViewModel>> GetByIdAsync(int id);
    Task<OperationResult<int>> InsertAsync(UserRegisterInput model);
    Task<OperationResult> UpdateAsync(UserUpdateInfoInput model);
    Task<OperationResult> ActivateAsync(int id);
    Task<OperationResult> DeactivateAsync(int id);
    Task<OperationResult> ChangePermission(int userId, int permissionId);
}