using Irrigation.Core.ViewModels;

namespace Irrigation.Core.Contracts;

public interface IUserRepository
{
    //Task<User> GetUserByLogin(LoginRequest model);
    
    Task<List<UserViewModel>> GetAllAsync();
    
    
    
    //Task<long> InsertAsync(User user);
    //Task<UserViewModel> GetByIdAsync(int id);
    //Task<UserViewModel> GetByEmailAsync(string address);
    //Task<int> UpdateAsync(User user);
    
    //Task<User> GetUserByIdAsync(int id);
    //Task<List<User>> GetEntitiesByIdsAsync(List<int> ids);
}