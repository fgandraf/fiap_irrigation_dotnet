using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("v1/area")]
public class AreaController: ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}