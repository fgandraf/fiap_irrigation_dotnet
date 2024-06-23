namespace Irrigation.Core.ViewModels;

public record WeatherViewModel(
    int Id,
    DateTime Timestamp,
    int Temperature,
    int Humidity,
    string Description,
    int SensorId);