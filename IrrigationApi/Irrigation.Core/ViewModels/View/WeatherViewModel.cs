namespace Irrigation.Core.ViewModels.View;

public record WeatherViewModel(
    int Id,
    DateTime Timestamp,
    int Temperature,
    int Humidity,
    string Description,
    int SensorId);