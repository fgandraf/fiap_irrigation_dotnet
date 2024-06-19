namespace Irrigation.Core.Models;

public class _Weather
{
    
}


/* -- FROM JAVA CODE

   public class Weather {

       @Id
       @Column(name = "weather_id")
       @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "SEQ_WEATHER")
       @SequenceGenerator(name = "SEQ_WEATHER", sequenceName = "SEQ_WEATHER", allocationSize = 1)
       private Long id;

       private LocalDateTime timestamp;

       private Integer temperature;

       private Integer humidity;

       private String description;

       @JsonIgnore
       @ManyToOne
       @JoinColumn(name = "sensor_id")
       private Sensor sensor;
   }
   
*/