using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class _AreaMap: IEntityTypeConfiguration<_Area>
{
    public void Configure(EntityTypeBuilder<_Area> builder)
    {
        //// Table
        builder.ToTable("tbl_area");
        
        // TO DO: IMPLEMENTS (See file __EXAMPLE.cs)
    }
}