using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irrigation.Core.Models;

[Table("tbl_weather")]
public class _Weather
{
    [Key]
    [Column("weather_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("timestamp")]
    public DateTime Timestamp { get; set; }
    
    [Column("temperature")]
    public int Temperature { get; set; }
    
    [Column("humidity")]
    public int Humidity{ get; set; }
    
    [Column("description")]
    public string Description { get; set; }
}


/* -- FROM JAVA CODE
       @JsonIgnore
       @ManyToOne
       @JoinColumn(name = "sensor_id")
       private Sensor sensor;
   }
   
*/