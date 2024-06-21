using Irrigation.Core.Models;
using Irrigation.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Data;

public class IrrigationDataContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    public IrrigationDataContext() { }
    public IrrigationDataContext(DbContextOptions<IrrigationDataContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        base.OnModelCreating(modelBuilder);
    }
}