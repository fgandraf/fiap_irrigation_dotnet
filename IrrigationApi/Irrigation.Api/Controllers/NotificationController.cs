using Irrigation.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController(ITokenService tokenService, INotificationRepository repository): ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}

/* #################### FROM JAVA ####################
 
@RestController
   @RequestMapping("/api/notifications")
   public class NotificationController {
   
       @Autowired
       private NotificationService service;
   
       @PostMapping
       public ResponseEntity<OutputNotification> create(@RequestBody @Valid CreateNotification createNotification) {
           OutputNotification outputNotification = service.create(createNotification);
           URI location = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}").buildAndExpand(outputNotification.id()).toUri();
           return ResponseEntity.created(location).body(outputNotification);
       }
   
       @GetMapping("/id/{id}")
       public ResponseEntity<OutputNotification> getById(@PathVariable Long id) {
           return ResponseEntity.ok(new OutputNotification(service.findById(id)));
       }
   
       @GetMapping("/all")
       public Page<OutputNotification> getAll(Pageable pageable){
           return service.findAll(pageable);
       }
   
   
       @PutMapping
       public ResponseEntity<OutputNotification> update(@RequestBody @Valid UpdateNotification updateNotification) {
           OutputNotification outputNotification = service.update(updateNotification);
           return ResponseEntity.ok(outputNotification);
       }
   
       @DeleteMapping("/{id}")
       public ResponseEntity<Void> delete(@PathVariable Long id) {
           service.deleteById(id);
           return ResponseEntity.noContent().build();
       }
   }
   
*/