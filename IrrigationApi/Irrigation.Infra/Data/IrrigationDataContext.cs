using Irrigation.Core.Models;
using Irrigation.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Infra.Data;

public class IrrigationDataContext: DbContext
{
    
    public virtual DbSet<Area> Areas { get; set; } = null!;
    public virtual DbSet<Notification> Notifications { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<Schedule> Schedules { get; set; } = null!;
    public virtual DbSet<Sensor> Sensors { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Weather> Weathers { get; set; } = null!;
    

    public IrrigationDataContext() { }
    public IrrigationDataContext(DbContextOptions<IrrigationDataContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AreaMap());
        modelBuilder.ApplyConfiguration(new NotificationMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new ScheduleMap());
        modelBuilder.ApplyConfiguration(new SensorMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new WeatherMap());
        base.OnModelCreating(modelBuilder);
    }
}