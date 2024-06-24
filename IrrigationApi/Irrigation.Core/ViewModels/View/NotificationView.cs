namespace Irrigation.Core.ViewModels.View;

public record NotificationView(
    int Id,
    string Description,
    DateTime Timestamp,
    int SensorId);