using Irrigation.Core.Contracts;
using Irrigation.Core.ViewModels.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("api/areas")]
public class AreaController(ITokenService tokenService, IAreaRepository repository): ControllerBase
{
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Create([FromBody]AreaCreate model)
    {
        var result = repository.InsertAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    
    
}





/* #################### FROM JAVA ####################
 
   @GetMapping("/id/{id}")
   public ResponseEntity<OutputArea> getById(@PathVariable Long id) {
       return ResponseEntity.ok(service.findById(id));
   }

   @GetMapping("/all")
   public ResponseEntity<Page<OutputArea>> getAll(Pageable pageable) {
       return ResponseEntity.ok(service.findAll(pageable));
   }

   @PutMapping
   public ResponseEntity<OutputArea> update(@RequestBody UpdateArea area) {
       return ResponseEntity.ok(service.update(area));
   }

   @DeleteMapping("/{id}")
   public ResponseEntity<Void> delete(@PathVariable Long id) {
       service.delete(id);
       return ResponseEntity.noContent().build();
   }

}
*/