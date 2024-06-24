using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.View;

public record UserLoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
}