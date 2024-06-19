namespace Irrigation.Core.Models;

public class _Area
{
    
}

/* -- FROM JAVA CODE

   public class Area {

       @Id
       @Column(name = "area_id")
       @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "SEQ_AREA")
       @SequenceGenerator(name = "SEQ_AREA", sequenceName = "SEQ_AREA", allocationSize = 1)
       private Long id;

       private String description;

       private String location;

       @Column(name = "area_size")
       private String size;

       @OneToMany(mappedBy = "area")
       private List<Sensor> sensor;
   }
   
*/