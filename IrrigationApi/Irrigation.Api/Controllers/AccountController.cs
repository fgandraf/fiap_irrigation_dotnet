using Asp.Versioning;
using Irrigation.Core.Contracts;
using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Irrigation.Core.ViewModels.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class AccountController(ITokenService tokenService, IUserRepository repository) : ControllerBase
{
    [AllowAnonymous]
    [MapToApiVersion(1)]
    [HttpPost("login")]
    public IActionResult GetByLogin([FromBody]UserLoginView model)
    {
        var result = repository.GetByLoginAsync(model).Result;
        return result.Success ? Ok(tokenService.GenerateToken(result.Value)) : BadRequest(result.Message);
    }
    
    [AllowAnonymous]
    [MapToApiVersion(1)]
    [HttpPost("register")]
    public IActionResult Register([FromBody]UserCreate model)
    {
        var result = repository.InsertAsync(model).Result;
        return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Value }, new { id = result.Value }) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin, user")]
    [MapToApiVersion(1)]
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery]int page = 0, 
        [FromQuery]int pageSize = 25)
    {
        var result = repository.GetAllAsync(page, pageSize).Result;
        return result.Success ? Ok(result.Value) : NoContent();
    }
    
    [Authorize(Roles = "admin, user")]
    [MapToApiVersion(1)]
    [HttpGet("email/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var result = repository.GetByEmailAsync(email).Result;
        return result.Success ? Ok(result.Value) : NotFound();
    }
    
    [Authorize(Roles = "admin, user")]
    [MapToApiVersion(1)]
    [HttpGet("id/{id}")]
    public IActionResult GetById(int id)
    {
        var result = repository.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : NotFound();
    }
    
    [Authorize(Roles = "admin, user")]
    [MapToApiVersion(1)]
    [HttpPut]
    public IActionResult Update([FromBody]UserUpdateInfo model)
    {
        var result = repository.UpdateAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [MapToApiVersion(1)]
    [HttpPut("activate/{id}")]
    public IActionResult Activate(int id)
    {
        var result = repository.ActivateAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [MapToApiVersion(1)]
    [HttpPut("deactivate/{id}")]
    public IActionResult Deactivate(int id)
    {
        var result = repository.DeactivateAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [MapToApiVersion(1)]
    [HttpPut("permission/{userId},{roleId}")]
    public IActionResult ChangePermission(int userId, int roleId)
    {
        var result = repository.ChangePermission(userId, roleId).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
}