using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record SensorCreate
{
    [Required(ErrorMessage = "Type is required!")]
    public string Type { get; set; }
    
    [Required(ErrorMessage = "Location is required!")]
    public string Location { get; set; }
    
    [Required(ErrorMessage = "Area Id is required!")]
    public int AreaId { get; set; }
    
    [DefaultValue("[]")]
    public List<int> WeathersId { get; set; }
    
    [DefaultValue("[]")]
    public List<int> NotificationsId { get; set; }
}