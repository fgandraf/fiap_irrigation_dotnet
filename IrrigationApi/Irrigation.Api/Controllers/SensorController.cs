using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("v1/sensor")]
public class SensorController: ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}