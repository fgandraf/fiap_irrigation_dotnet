using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("v1/weather")]
public class WeatherController : ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}