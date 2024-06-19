using Microsoft.AspNetCore.Mvc;

namespace IrrigationApi.Controllers;

[ApiController]
[Route("v1/weather")]
public class WeatherForecast: ControllerBase
{

    [HttpGet]
    public string GetArea() => "Ok Weather";
    
}