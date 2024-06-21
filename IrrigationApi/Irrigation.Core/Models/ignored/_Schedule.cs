using System.ComponentModel.DataAnnotations.Schema;

namespace Irrigation.Core.Models;

[Table("tbl_schedule")]
public class _Schedule
{
    
}

/* -- FROM JAVA CODE

   public class Schedule {

       @Id
       @Column(name = "schedule_id")
       @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "SEQ_SCHEDULE")
       @SequenceGenerator(name = "SEQ_SCHEDULE", sequenceName = "SEQ_SCHEDULE", allocationSize = 1)
       private Long id;

       @Column(name = "start_time")
       private LocalDateTime startTime;

       @Column(name = "end_time")
       private LocalDateTime endTime;

   }

*/