namespace Irrigation.Core.ViewModels.View;

public record AreaView(
    int Id,
    string Description,
    string Location,
    string Size,
    List<int> SensorsId);