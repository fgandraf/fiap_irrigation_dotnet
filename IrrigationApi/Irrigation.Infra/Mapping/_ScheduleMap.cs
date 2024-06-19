using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class _ScheduleMap: IEntityTypeConfiguration<_Schedule>
{
    public void Configure(EntityTypeBuilder<_Schedule> builder)
    {
        //// Table
        builder.ToTable("tbl_schedule");
        
        // TO DO: IMPLEMENTS (See file __EXAMPLE.cs)
    }
}