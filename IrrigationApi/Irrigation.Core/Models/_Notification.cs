namespace Irrigation.Core.Models;

public class _Notification
{
    
}

/* -- FROM JAVA CODE

   public class Notification {

       @Id
       @Column(name = "notification_id")
       @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "SEQ_NOTIFICATION")
       @SequenceGenerator(name = "SEQ_NOTIFICATION", sequenceName = "SEQ_NOTIFICATION", allocationSize = 1)
       private Long id;

       private String description;

       private LocalDateTime timestamp;

       @ManyToOne
       @JoinColumn(name = "sensor_id")
       private Sensor sensor;

   }

*/