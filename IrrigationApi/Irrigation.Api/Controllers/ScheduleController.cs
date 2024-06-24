using Irrigation.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("api/schedules")]
public class ScheduleController(ITokenService tokenService, IScheduleRepository repository): ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}


/* #################### FROM JAVA ####################

@RestController
   @RequestMapping("/api/schedules")
   public class ScheduleController {
   
       @Autowired
       private ScheduleService service;
   
       @PostMapping
       public ResponseEntity<Schedule> create(@RequestBody @Valid CreateSchedule schedule) {
           Schedule savedSchedule = service.save(schedule);
           URI location = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}").buildAndExpand(savedSchedule.getId()).toUri();
           return ResponseEntity.created(location).body(savedSchedule);
       }
   
       @GetMapping("/id/{id}")
       public ResponseEntity<Schedule> getById(@PathVariable Long id) {
           Optional<Schedule> schedule = service.findById(id);
           return schedule.map(ResponseEntity::ok).orElse(ResponseEntity.notFound().build());
       }
   
       @GetMapping("/all")
       public ResponseEntity<Page<Schedule>> getAll(Pageable pageable) {
           Page<Schedule> schedules = service.findAll(pageable);
           return ResponseEntity.ok(schedules);
       }
   
       @PutMapping
       public ResponseEntity<Schedule> update(@RequestBody @Valid UpdateSchedule schedule) {
               return ResponseEntity.ok(service.update(schedule));
       }
   
       @DeleteMapping("/{id}")
       public ResponseEntity<Void> delete(@PathVariable Long id) {
           if (!service.findById(id).isPresent()) {
               return ResponseEntity.notFound().build();
           }
           service.deleteById(id);
           return ResponseEntity.noContent().build();
       }
   }
   
*/