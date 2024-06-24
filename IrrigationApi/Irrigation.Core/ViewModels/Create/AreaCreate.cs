using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record AreaCreate
{
    [Required(ErrorMessage = "Description is required!")]
    [DefaultValue("")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Location is required!")]
    [DefaultValue("")]
    public string Location { get; set; }
    
    [Required(ErrorMessage = "Size is required!")]
    [DefaultValue("")]
    public string Size { get; set; }
    


}