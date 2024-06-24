using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Update;

public record NotificationUpdate
{
    [Required(ErrorMessage = "'Id' is required!")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Description is required!")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Timestamp is required!")]
    public DateTime Timestamp { get; set; }
    
    [Required(ErrorMessage = "Sensor Id is required!")]
    public int SensorId { get; set; }
}