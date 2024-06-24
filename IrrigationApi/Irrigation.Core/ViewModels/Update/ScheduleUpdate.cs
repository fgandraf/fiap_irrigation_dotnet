using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Irrigation.Core.ViewModels.Update;

public record ScheduleUpdate
{
    [Required(ErrorMessage = "'Id' is required!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Start date is required!")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required!")]
    public DateTime EndDate { get; set; }
}