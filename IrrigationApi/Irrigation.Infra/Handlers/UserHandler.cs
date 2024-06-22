using Irrigation.Core;
using Irrigation.Core.Contracts.Handlers;
using Irrigation.Core.Contracts.Repositories;
using Irrigation.Core.Models;
using Irrigation.Core.ViewModels;
using SecureIdentity.Password;

namespace Irrigation.Infra.Handlers;

public class UserHandler(IUserRepository userRepository) : IUserHandler
{
    public async Task<OperationResult<User>> GetUserByLoginAsync(UserLoginViewModel model)
    {
        var user = await userRepository.GetUserByLogin(model); 
        
        if (user is null || !user.Active)
            return OperationResult<User>.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
            return OperationResult<User>.FailureResult("Usuário ou senha inválida!");

        return OperationResult<User>.SuccessResult(user);
    }
}