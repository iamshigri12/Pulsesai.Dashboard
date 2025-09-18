using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Pulsesai.Dashboard.EntityFrameworkCore;

public static class DashboardDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<DashboardDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<DashboardDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
