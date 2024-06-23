using System.Text.Json.Serialization;

namespace Irrigation.Core.Models;

public class Area
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Size { get; set; }
    
    [JsonIgnore]
    public List<Sensor> Sensors { get; set; }
}