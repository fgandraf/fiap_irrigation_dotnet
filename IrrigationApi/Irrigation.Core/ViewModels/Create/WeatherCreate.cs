using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record WeatherCreate
{
    [Required(ErrorMessage = "Timestamp is required!")]
    public DateTime Timestamp { get; set; }
    
    [Required(ErrorMessage = "Temperature is required!")]
    public int Temperature { get; set; }
    
    [Required(ErrorMessage = "Humidity is required!")]
    public int Humidity { get; set; }
    
    [Required(ErrorMessage = "Description is required!")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Sensor Id is required!")]
    public int SensorId { get; set; }
}