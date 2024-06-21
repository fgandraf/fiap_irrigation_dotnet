using Irrigation.Core.Contracts;
using Irrigation.Core.ViewModels;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Repositories.Database;

public class UserRepository : IUserRepository
{
    private IrrigationDataContext _context;

    public UserRepository(IrrigationDataContext context)
        => _context = context;
    
    
    
    public async Task<List<UserViewModel>>  GetAllAsync()
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Select(user => new UserViewModel
            (
                user.Id,
                user.Name,
                user.Email,
                user.Active,
                user.Roles.Select(x => x.Id).ToList()
            ))
            .ToListAsync();
        
        return users;
    }
}