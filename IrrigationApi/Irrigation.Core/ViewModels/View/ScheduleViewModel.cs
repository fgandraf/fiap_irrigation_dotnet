namespace Irrigation.Core.ViewModels.View;

public record ScheduleViewModel(
    int Id,
    DateTime StartDate,
    DateTime EndDate);