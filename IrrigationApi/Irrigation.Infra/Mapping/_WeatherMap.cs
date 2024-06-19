using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class _WeatherMap : IEntityTypeConfiguration<_Weather>
{
    public void Configure(EntityTypeBuilder<_Weather> builder)
    {
        //// Table
        builder.ToTable("tbl_weather");
        
        // TO DO: IMPLEMENTS (See file __EXAMPLE.cs)
    }
}