namespace Irrigation.Core.Models;

public class _Sensor
{
    
}

/* -- FROM JAVA CODE

   public class Sensor {

       @Id
       @Column(name = "sensor_id")
       @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "SEQ_SENSOR")
       @SequenceGenerator(name = "SEQ_SENSOR", sequenceName = "SEQ_SENSOR", allocationSize = 1)
       private Long id;

       private String type;

       private String location;

       @JsonIgnore
       @ManyToOne
       @JoinColumn(name = "area_id")
       private Area area;

       @OneToMany(mappedBy = "sensor")
       private List<Weather> weathers;

       @OneToMany(mappedBy = "sensor")
       private List<Notification> notifications;
   }

*/