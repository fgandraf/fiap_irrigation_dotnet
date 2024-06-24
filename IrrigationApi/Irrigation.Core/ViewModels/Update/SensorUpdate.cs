using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Update;

public record SensorUpdate
{
    [Required(ErrorMessage = "'Id' is required!")]
    [DefaultValue("")]
    public int Id { get; set; }
    
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