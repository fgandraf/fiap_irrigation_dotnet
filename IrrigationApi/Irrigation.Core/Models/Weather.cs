namespace Irrigation.Core.Models;

public class Weather
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int Temperature { get; set; }
    public int Humidity{ get; set; }
    public string Description { get; set; }
    public Sensor Sensor { get; set; }
}