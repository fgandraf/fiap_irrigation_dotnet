using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("v1/schedule")]
public class ScheduleController: ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}