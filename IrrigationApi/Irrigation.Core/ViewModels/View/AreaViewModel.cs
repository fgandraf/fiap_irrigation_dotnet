namespace Irrigation.Core.ViewModels.View;

public record AreaViewModel(
    int Id,
    string Description,
    string Location,
    string Size,
    List<int> SensorsId);