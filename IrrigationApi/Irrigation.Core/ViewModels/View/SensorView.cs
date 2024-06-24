namespace Irrigation.Core.ViewModels.View;

public record SensorView(
    int Id,
    string Type,
    string Location,
    int AreaId,
    List<int> WeathersId,
    List<int> NotificationsId);