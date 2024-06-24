namespace Irrigation.Core.ViewModels.View;

public record ScheduleView(
    int Id,
    DateTime StartDate,
    DateTime EndDate);