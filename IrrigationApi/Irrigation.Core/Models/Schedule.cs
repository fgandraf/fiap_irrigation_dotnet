namespace Irrigation.Core.Models;

public class Schedule
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}