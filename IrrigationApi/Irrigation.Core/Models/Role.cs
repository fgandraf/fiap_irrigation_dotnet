using System.Text.Json.Serialization;

namespace Irrigation.Core.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public List<User> Users { get; set; }
}
