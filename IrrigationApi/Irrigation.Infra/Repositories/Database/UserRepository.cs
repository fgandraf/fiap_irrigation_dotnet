using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using Irrigation.Infra.Data;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Irrigation.Infra.Repositories.Database;

public class UserRepository(IrrigationDataContext context) : IUserRepository
{
    
    public async Task<OperationResult<User>> GetByLoginAsync(UserLoginViewModel model)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        if (user is null || !user.Active)
            return OperationResult<User>.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
            return OperationResult<User>.FailureResult("Usuário ou senha inválida!");

        return OperationResult<User>.SuccessResult(user);
    }


    public async Task<OperationResult<dynamic>> GetAllAsync(int page, int pageSize)
    {
        var count = await context.Users.AsNoTracking().CountAsync();

        var users = await context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Select(user => new UserViewModel
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
            return OperationResult<dynamic>.FailureResult("Nenhum usuário cadastrado!");
        
        return OperationResult<dynamic>.SuccessResult(new
        {
            total = count,
            page,
            pageSize,
            users
        });
    }

    public async Task<OperationResult<UserViewModel>> GetByEmailAsync(string address)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Where(x => x.Email == address)
            .Include(roles => roles.Roles)
            .Select(user => new UserViewModel
                (
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Active,
                    user.Roles.Select(x => x.Id).ToList())
            )
            .FirstOrDefaultAsync();
        
        if (user is null)
            return OperationResult<UserViewModel>.FailureResult($"Usuário '{address}' não encontrado!");
        
        return OperationResult<UserViewModel>.SuccessResult(user);
    }

    public async Task<OperationResult<UserViewModel>> GetByIdAsync(int id)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(roles => roles.Roles)
            .Select(user => new UserViewModel
                (
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Active,
                    user.Roles.Select(x => x.Id).ToList())
            )
            .FirstOrDefaultAsync();
        
        if (user is null)
            return OperationResult<UserViewModel>.FailureResult($"Usuário '{id}' não encontrado!");
        
        return OperationResult<UserViewModel>.SuccessResult(user);
    }

    public async Task<OperationResult<int>> InsertAsync(UserRegisterInput model)
    {
        var userRole = context.Roles.FirstOrDefault(x => x.Id == 2);
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
        var id = await context.SaveChangesAsync();
        return id > 0 ? OperationResult<int>.SuccessResult(id) : OperationResult<int>.FailureResult("Não foi possível inserir o usuário!");
    }

    public async Task<OperationResult> UpdateAsync(UserUpdateInfoInput model)
    {
        var user = await context.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        user.Name = model.Name;
        user.Email = model.Email;
        user.PasswordHash = PasswordHasher.Hash(model.Password);
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }
    
    public async Task<OperationResult> ActivateAsync(int id)
    {
        var user = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        user.Active = true;
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o estado do usuário!");
    }
    
    public async Task<OperationResult> DeactivateAsync(int id)
    {
        var user = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        user.Active = false;
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o estado do usuário!");
    }

    public async Task<OperationResult> ChangePermission(int userId, int permissionId)
    {
        var user = await context.
            Users
            .Include(x=> x.Roles)
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();
        
        var role = await context.Roles.Where(x => x.Id == permissionId).FirstOrDefaultAsync();
        
        user.Roles = new List<Role> { role };
        
        context.Users.Update(user);
        
        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar as permissões do usuário!");
    }
}