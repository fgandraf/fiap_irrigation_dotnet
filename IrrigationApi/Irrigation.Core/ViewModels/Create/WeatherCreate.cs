using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record WeatherCreate
{
    [Required(ErrorMessage = "Timestamp is required!")]
    [DefaultValue("")]
    public DateTime Timestamp { get; set; }
    
    [Required(ErrorMessage = "Temperature is required!")]
    [DefaultValue("")]
    public int Temperature { get; set; }
    
    [Required(ErrorMessage = "Humidity is required!")]
    [DefaultValue("")]
    public int Humidity { get; set; }
    
    [Required(ErrorMessage = "Description is required!")]
    [DefaultValue("")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Sensor Id is required!")]
    [DefaultValue("")]
    public int SensorId { get; set; }
}