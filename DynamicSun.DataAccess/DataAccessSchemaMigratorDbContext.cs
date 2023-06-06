using Microsoft.EntityFrameworkCore;

namespace DynamicSun.DataAccess;

internal sealed class DataAccessSchemaMigratorDbContext : DbContext
{
    public DataAccessSchemaMigratorDbContext(
        DbContextOptions<DataAccessSchemaMigratorDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataAccessSchemaMigratorDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}