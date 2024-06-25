namespace Irrigation.Core.ViewModels.View;

public record ListView(
    int Total,
    int Page,
    int PageSize,
    Object Content);

