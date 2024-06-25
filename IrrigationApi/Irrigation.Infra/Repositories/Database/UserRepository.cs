using Irrigation.Core;
using Irrigation.Core.Contracts.Repositories;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Irrigation.Infra.Repositories.Database;

public class UserRepository(IrrigationDataContext context) : IUserRepository
{
    
    public async Task<OperationResult<User>> GetByLoginAsync(UserLoginView model)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        if (user is null || !user.Active)
            return OperationResult<User>.FailureResult($"User '{model.Email}' not found or not active!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
            return OperationResult<User>.FailureResult("User name or password invalid!");

        return OperationResult<User>.SuccessResult(user);
    }


    public async Task<OperationResult<ListView>> GetAllAsync(int page, int pageSize)
    {
        var count = await context.Users.AsNoTracking().CountAsync();

        var users = await context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Select(user => new UserView
                (
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Active,
                    user.Roles.Select(x => x.Id).ToList())
            )
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (users.Count == 0)
            return OperationResult<ListView>.FailureResult();
        
        return OperationResult<ListView>.SuccessResult(new ListView(
            Total: count, Page: page, PageSize:pageSize, Content: users)
        );
    }

    public async Task<OperationResult<UserView>> GetByEmailAsync(string address)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Where(x => x.Email == address)
            .Include(roles => roles.Roles)
            .Select(user => new UserView
                (
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Active,
                    user.Roles.Select(x => x.Id).ToList())
            )
            .FirstOrDefaultAsync();
        
        if (user is null)
            return OperationResult<UserView>.FailureResult();
        
        return OperationResult<UserView>.SuccessResult(user);
    }

    public async Task<OperationResult<UserView>> GetByIdAsync(int id)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(roles => roles.Roles)
            .Select(user => new UserView
                (
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Active,
                    user.Roles.Select(x => x.Id).ToList())
            )
            .FirstOrDefaultAsync();
        
        if (user is null)
            return OperationResult<UserView>.FailureResult();
        
        return OperationResult<UserView>.SuccessResult(user);
    }

    public async Task<OperationResult<int>> InsertAsync(UserCreate model)
    {
        var userRole = await context.Roles.Where(x => x.Id == 2).FirstOrDefaultAsync();
        var user = new User
        {
            Active = false,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            Name = model.Name,
            Roles = new List<Role>
            {
                userRole
            }
        };
        context.Users.Add(user);
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult<int>.SuccessResult(user.Id) : OperationResult<int>.FailureResult("Unable to add user!");
    }

    public async Task<OperationResult> UpdateAsync(UserUpdateInfo model)
    {
        var user = await context.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

        if (user is null)
            return OperationResult.FailureResult($"User {model.Id} not found!");
        
        user.Name = model.Name;
        user.Email = model.Email;
        user.PasswordHash = PasswordHasher.Hash(model.Password);
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter user!");
    }
    
    public async Task<OperationResult> ActivateAsync(int id)
    {
        var user = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (user is null)
            return OperationResult.FailureResult();
        
        user.Active = true;
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter user state!");
    }
    
    public async Task<OperationResult> DeactivateAsync(int id)
    {
        var user = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (user is null)
            return OperationResult.FailureResult();
        
        user.Active = false;
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter user state!");
    }

    public async Task<OperationResult> ChangePermission(int userId, int roleId)
    {
        var user = await context.
            Users
            .Include(x=> x.Roles)
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return OperationResult.FailureResult($"User {userId} not found!");
        
        var role = await context.Roles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
        
        if (role is null)
            return OperationResult.FailureResult($"Role {roleId} not found!");
        
        user.Roles = new List<Role> { role };
        
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Unable to alter user permissions!");
    }
}