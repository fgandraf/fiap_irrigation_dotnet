using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("v1/user")]
public class UserController: ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}