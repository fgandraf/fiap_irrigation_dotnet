using System.ComponentModel.DataAnnotations.Schema;

namespace Irrigation.Core.Models;

[Table("tbl_user")]
public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email{ get; set; }
    public string PasswordHash { get; set; }
    public bool Active { get; set; }
    public List<Role> Roles { get; set; }
}