using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class NotificationMap: IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        //// Table
        builder.ToTable("tbl_notification");
        
        //// Primary Key - Identity
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("notification_id")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        //// Properties
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnName("description");
        
        builder.Property(x => x.Timestamp)
            .IsRequired()
            .HasColumnName("timestamp");
        
        // Notification -> Sensor
        builder.HasOne(x => x.Sensor)
            .WithMany(x => x.Notifications)
            .HasForeignKey("sensor_id")
            .HasConstraintName("FK_Notification_Sensor");
    }
}

