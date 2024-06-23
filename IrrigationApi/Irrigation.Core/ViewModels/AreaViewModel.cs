namespace Irrigation.Core.ViewModels;

public record AreaViewModel(
    int Id,
    string Description,
    string Location,
    string Size,
    List<int> SensorsId);