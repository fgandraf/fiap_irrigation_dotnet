namespace Irrigation.Core.ViewModels;

public record NotificationViewModel(
    int Id,
    string Description,
    DateTime Timestamp,
    int SensorId);