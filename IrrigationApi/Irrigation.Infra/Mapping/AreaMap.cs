using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class AreaMap: IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        //// Table
        builder.ToTable("tbl_area");
        
        //// Primary Key - Identity
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("area_id")
            .ValueGeneratedOnAdd()
            .ValueGeneratedOnAdd();
        
        //// Properties
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnName("description");

        builder.Property(x => x.Location)
            .IsRequired(false)
            .HasColumnName("location");

        builder.Property(x => x.Size)
            .IsRequired(false)
            .HasColumnName("area_size");
    }
}