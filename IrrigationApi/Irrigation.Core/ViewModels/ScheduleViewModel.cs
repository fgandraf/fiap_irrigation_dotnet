namespace Irrigation.Core.ViewModels;

public record ScheduleViewModel(
    int Id,
    DateTime StartDate,
    DateTime EndDate);