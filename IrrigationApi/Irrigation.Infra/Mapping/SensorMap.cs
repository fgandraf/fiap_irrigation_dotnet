using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class SensorMap: IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        //// Table
        builder.ToTable("tbl_sensor");
        
        // Primary Key - Identity
             builder.HasKey(x => x.Id);
             builder.Property(x => x.Id)
                 .HasColumnName("sensor_id")
                 .ValueGeneratedOnAdd()
                 .UseIdentityColumn();
             
            //// Properties
            builder.Property(x => x.Type)
                .IsRequired(false)
                .HasColumnName("type")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);
            
            builder.Property(x => x.Location)
                .IsRequired(false)
                .HasColumnName("location")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11);
            
            //// Relation
            // Area -> Sensor
            builder.HasOne(x => x.Area)
                .WithMany(x => x.Sensors)
                .HasConstraintName("FK_Area_Sensor");
    }
}