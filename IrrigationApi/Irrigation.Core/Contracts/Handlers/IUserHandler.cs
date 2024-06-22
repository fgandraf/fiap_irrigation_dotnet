using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;

namespace Irrigation.Core.Contracts.Handlers;

public interface IUserHandler
{
    Task<OperationResult<User>> GetUserByLoginAsync(UserLoginViewModel model);
}