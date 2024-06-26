using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class ScheduleMap: IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        //// Table
        builder.ToTable("tbl_schedule");
        
        // Primary Key - Identity
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("schedule_id")
            .ValueGeneratedOnAdd()
            .ValueGeneratedOnAdd();
        
        //// Properties
        builder.Property(x => x.StartTime)
            .IsRequired()
            .HasColumnName("start_time");
        
        builder.Property(x => x.EndTime)
            .IsRequired()
            .HasColumnName("end_time");
    }
}