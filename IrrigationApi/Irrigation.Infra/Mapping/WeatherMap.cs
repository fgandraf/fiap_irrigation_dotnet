using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class WeatherMap : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        //// Table
        builder.ToTable("tbl_weather");
        
        // Primary Key - Identity
         builder.HasKey(x => x.Id);
         builder.Property(x => x.Id)
             .HasColumnName("weather_id")
             .ValueGeneratedOnAdd()
             .UseIdentityColumn();
         
         //// Properties
         builder.Property(x => x.Timestamp)
             .IsRequired(false)
             .HasColumnName("timestamp");

         builder.Property(x => x.Temperature)
             .IsRequired(false)
             .HasColumnName("temperature");

         builder.Property(x => x.Humidity)
             .IsRequired(false)
             .HasColumnName("humidity");

         builder.Property(x => x.Description)
             .IsRequired(false)
             .HasColumnName("description");
         
         //// Relation
         // Weather -> Sensor
         builder.HasOne(x => x.Sensor)
             .WithMany(x => x.Weathers)
             .HasConstraintName("FK_Weather_Sensor");
    }
}