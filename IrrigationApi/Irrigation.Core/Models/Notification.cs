namespace Irrigation.Core.Models;

public class Notification
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
    public Sensor Sensor { get; set; }
}
