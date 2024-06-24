namespace Irrigation.Core.ViewModels.View;

public record UserViewModel(
    int Id,
    string Name,
    string Email,
    bool Active,
    List<int> Roles);
    