using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record ScheduleCreate
{
    [Required(ErrorMessage = "Start date is required!")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required!")]
    public DateTime EndDate { get; set; }
}