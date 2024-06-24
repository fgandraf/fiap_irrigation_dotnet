using Irrigation.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Irrigation.Api.Controllers;

[ApiController]
[Route("api/weathers")]
public class WeatherController(ITokenService tokenService, IWeatherRepository repository) : ControllerBase
{
    [HttpGet]
    public string HelloWorld() => "Hello World";
}


/* #################### FROM JAVA ####################

@RestController
   @RequestMapping("/api/weathers")
   public class WeatherController {
   
       @Autowired
       private WeatherService service;
       @PostMapping
       public ResponseEntity<OutputWeather> create(@RequestBody @Valid CreateWeather weather) {
           OutputWeather savedWeather = service.create(weather);
           URI location = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}").buildAndExpand(savedWeather.id()).toUri();
           return ResponseEntity.created(location).body(savedWeather);
       }
   
       @GetMapping("/id/{id}")
       public ResponseEntity<Weather> getById(@PathVariable Long id) {
           Optional<Weather> weather = service.findById(id);
           return weather.map(ResponseEntity::ok).orElse(ResponseEntity.notFound().build());
       }
   
       @GetMapping("/all")
       public ResponseEntity<Page<Weather>> getAll(Pageable pageable) {
           Page<Weather> weathers = service.findAll(pageable);
           return ResponseEntity.ok(weathers);
       }
   
       @PutMapping
       public OutputWeather update(@RequestBody @Valid UpdateWeather weather) {
           return service.update(weather);
       }
   
       @DeleteMapping("/{id}")
       public ResponseEntity<Void> delete(@PathVariable Long id) {
           service.deleteById(id);
           return ResponseEntity.noContent().build();
       }
   }
   
*/