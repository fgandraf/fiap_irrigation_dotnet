using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels;

public record UserLoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
}