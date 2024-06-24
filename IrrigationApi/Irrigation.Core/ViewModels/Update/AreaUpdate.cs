using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Update;

public record AreaUpdate
{
    [Required(ErrorMessage = "'Id' is required!")]
    [DefaultValue("")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Description is required!")]
    [DefaultValue("")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Location is required!")]
    [DefaultValue("")]
    public string Location { get; set; }
    
    [Required(ErrorMessage = "Size is required!")]
    [DefaultValue("")]
    public string Size { get; set; }
    
    [DefaultValue("[]")]
    public List<int> SensorsId { get; set; }
}