namespace Irrigation.Core.Models;

public class Schedule
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}