using System.Text.Json.Serialization;

namespace Irrigation.Core.Models;

public class Sensor
{
    public long Id { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
    public Area Area { get; set; }
    
    [JsonIgnore]
    public List<Weather> Weathers { get; set; }
    
    [JsonIgnore]
    public List<Notification> Notifications { get; set; }
    
}