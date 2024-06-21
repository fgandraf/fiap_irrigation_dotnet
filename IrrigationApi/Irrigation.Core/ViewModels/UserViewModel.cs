namespace Irrigation.Core.ViewModels;

public record UserViewModel(
    long Id,
    string Name,
    string Email,
    bool Active,
    List<int> Roles);
    
