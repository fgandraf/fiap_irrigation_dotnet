using Irrigation.Core.Contracts;
using Irrigation.Core.ViewModels.Create;
using Irrigation.Core.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController(ITokenService tokenService, INotificationRepository repository): ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Create([FromBody]NotificationCreate model)
    {
        var result = repository.InsertAsync(model).Result;
        return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Value }, new { id = result.Value }) : NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut]
    public IActionResult Update([FromBody]NotificationUpdate model)
    {
        var result = repository.UpdateAsync(model).Result;
        return result.Success ? Ok() : NoContent();
    }
    
    [Authorize(Roles = "admin, user")]
    [HttpGet("/id/{id}")]
    public IActionResult GetById(int id)
    {
        var result = repository.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : NoContent();
    }
    
    [Authorize(Roles = "admin, user")]
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery]int page = 0, 
        [FromQuery]int pageSize = 25)
    {
        var result = repository.GetAllAsync(page, pageSize).Result;
        return result.Success ? Ok(result.Value) : NoContent();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = repository.DeleteAsync(id).Result;
        return result.Success ? Ok() : NoContent();
    }
}