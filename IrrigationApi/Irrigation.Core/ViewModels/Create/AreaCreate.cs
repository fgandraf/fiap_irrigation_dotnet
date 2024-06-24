using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record AreaCreate
{
    [Required(ErrorMessage = "Description is required!")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Location is required!")]
    public string Location { get; set; }
    
    [Required(ErrorMessage = "Size is required!")]
    public string Size { get; set; }
    
    [DefaultValue("[]")]
    public List<int> SensorsId { get; set; }
}