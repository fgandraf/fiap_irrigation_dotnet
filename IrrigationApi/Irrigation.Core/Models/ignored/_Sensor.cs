using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irrigation.Core.Models;

[Table("tbl_sensor")]
public class _Sensor
{
    [Key]
    [Column("sensor_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("type")]
    public string Type { get; set; }
    
    [Column("location")]
    public string Location { get; set; }
    
}

/* -- FROM JAVA CODE

       @JsonIgnore
       @ManyToOne
       @JoinColumn(name = "area_id")
       private Area area;

       @OneToMany(mappedBy = "sensor")
       private List<Weather> weathers;

       @OneToMany(mappedBy = "sensor")
       private List<Notification> notifications;
*/