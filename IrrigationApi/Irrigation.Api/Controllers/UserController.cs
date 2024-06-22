using Irrigation.Api.Services;
using Irrigation.Core.Contracts.Handlers;
using Irrigation.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("v1/user")]
public class UserController(TokenService tokenService, IUserHandler userHandler) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody]UserLoginViewModel model)
    {
        var result = userHandler.GetUserByLoginAsync(model).Result;
        return result.Success ? Ok(tokenService.GenerateToken(result.Value)) : BadRequest(result.Message);
    }
}