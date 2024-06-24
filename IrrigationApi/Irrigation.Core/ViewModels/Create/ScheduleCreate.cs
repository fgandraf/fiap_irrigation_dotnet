using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Create;

public record ScheduleCreate
{
    [Required(ErrorMessage = "Start date is required!")]
    [DefaultValue("")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required!")]
    [DefaultValue("")]
    public DateTime EndDate { get; set; }
}