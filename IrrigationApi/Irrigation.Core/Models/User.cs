
using Irrigation.Core.Enums;

namespace Irrigation.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email{ get; set; }
    public string PasswordHash { get; set; }
    public bool Active { get; set; }
    
    public EUserRole Role { get; set; }
}