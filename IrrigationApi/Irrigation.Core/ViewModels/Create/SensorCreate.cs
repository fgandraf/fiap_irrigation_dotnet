using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record SensorCreate
{
    [Required(ErrorMessage = "Type is required!")]
    [DefaultValue("")]
    public string Type { get; set; }
    
    [Required(ErrorMessage = "Location is required!")]
    [DefaultValue("")]
    public string Location { get; set; }
    
    [Required(ErrorMessage = "Area Id is required!")]
    [DefaultValue("")]
    public int AreaId { get; set; }
    
    [DefaultValue("[]")]
    public List<int> WeathersId { get; set; }
    
    [DefaultValue("[]")]
    public List<int> NotificationsId { get; set; }
}