using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class _UserMap: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //// Table
        builder.ToTable("tbl_user");
        
        // TO DO: IMPLEMENTS (See file __EXAMPLE.cs)
    }
}