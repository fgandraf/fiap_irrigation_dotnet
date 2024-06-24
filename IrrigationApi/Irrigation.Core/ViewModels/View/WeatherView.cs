namespace Irrigation.Core.ViewModels.View;

public record WeatherView(
    int Id,
    DateTime Timestamp,
    int Temperature,
    int Humidity,
    string Description,
    int SensorId);