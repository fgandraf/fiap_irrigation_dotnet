namespace Irrigation.Core.ViewModels;

public record SensorViewModel(
    int Id,
    string Type,
    string Location,
    int AreaId,
    List<int> WeathersId,
    List<int> NotificationsId);