using Abp.Zero.EntityFrameworkCore;
using Pulsesai.Dashboard.Authorization.Roles;
using Pulsesai.Dashboard.Authorization.Users;
using Pulsesai.Dashboard.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Pulsesai.Dashboard.EntityFrameworkCore;

public class DashboardDbContext : AbpZeroDbContext<Tenant, Role, User, DashboardDbContext>
{
    /* Define a DbSet for each entity of the application */

    public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
        : base(options)
    {
    }
}
