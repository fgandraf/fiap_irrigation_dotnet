namespace Irrigation.Core.ViewModels.View;

public record UserView(
    int Id,
    string Name,
    string Email,
    bool Active,
    List<int> Roles);
    
