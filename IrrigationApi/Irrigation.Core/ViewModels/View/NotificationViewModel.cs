namespace Irrigation.Core.ViewModels.View;

public record NotificationViewModel(
    int Id,
    string Description,
    DateTime Timestamp,
    int SensorId);