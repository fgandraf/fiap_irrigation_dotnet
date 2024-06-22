using Irrigation.Core.Contracts.Repositories;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Repositories.Database;

public class UserRepository(IrrigationDataContext context) : IUserRepository
{
    public async Task<User> GetUserByLogin(UserLoginViewModel model)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        return user;
    }
}