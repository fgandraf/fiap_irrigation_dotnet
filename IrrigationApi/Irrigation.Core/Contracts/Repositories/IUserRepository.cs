using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;

namespace Irrigation.Core.Contracts.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByLogin(UserLoginViewModel model);
}