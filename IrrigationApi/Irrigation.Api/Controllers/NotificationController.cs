using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("v1/notification")]
public class NotificationController: ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}