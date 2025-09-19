using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pulsesai.Dashboard.Authorization.Roles;
using Pulsesai.Dashboard.Authorization.Users;
using Pulsesai.Dashboard.MultiTenancy;
using Pulsesai.Dashboard.Pulses;

namespace Pulsesai.Dashboard.EntityFrameworkCore;

public class DashboardDbContext : AbpZeroDbContext<Tenant, Role, User, DashboardDbContext>
{
    /* Define a DbSet for each entity of the application */

    public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
        : base(options)
    {
    }
    public DbSet<SensorReading> SensorReadings { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<SensorReading>(b =>
        {
            b.ToTable("AppSensorReadings");
            b.HasKey(x => x.Id);
        });
    }
}
