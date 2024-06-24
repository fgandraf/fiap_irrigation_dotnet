using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Update;

public record UserUpdateInfo
{
    [Required(ErrorMessage = "'Id' is required!")]
    [DefaultValue("")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "User name is required!")]
    [DefaultValue("")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress]
    [DefaultValue("")]
    public string Email{ get; set; }
    
    [Required(ErrorMessage = "Password is required!")]
    [DefaultValue("")]
    [Length(minimumLength:6, maximumLength:20, ErrorMessage = "Password must be between 6 -20  characters")]
    public string Password { get; set; }
}