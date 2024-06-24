using Irrigation.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("api/sensors")]
public class SensorController(ITokenService tokenService, ISensorRepository repository): ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}


/* #################### FROM JAVA ####################

@RestController
   @RequestMapping("/api/sensors")
   public class SensorController {
   
       @Autowired
       private SensorService service;
   
       @PostMapping
       public ResponseEntity<OutputSensor> create(@RequestBody CreateSensor createSensor){
           OutputSensor outputSensor = service.create(createSensor);
           URI location = ServletUriComponentsBuilder.fromCurrentRequest().path("{/id}").buildAndExpand(outputSensor.id()).toUri();
           return ResponseEntity.created(location).body(outputSensor);
       }
   
       @GetMapping("/id/{id}")
       public ResponseEntity<OutputSensor> getById(@PathVariable Long id){
           return ResponseEntity.ok(service.findById(id));
       }
   
       @GetMapping("/all")
       public Page<OutputSensor> getAll(Pageable pageable){
           return service.findAll(pageable);
       }
   
       @PutMapping
       public OutputSensor update(@RequestBody UpdateSensor updateSensor){
           return service.update(updateSensor);
       }
   
       @DeleteMapping("/{id}")
       public ResponseEntity<Void> delete(@PathVariable Long id){
           service.delete(id);
           return ResponseEntity.noContent().build();
       }
   
   }
   
*/