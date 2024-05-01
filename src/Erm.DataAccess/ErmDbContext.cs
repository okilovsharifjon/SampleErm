using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace Erm.DataAccess;

public sealed class ErmDbContext(DbContextOptions dbContext): DbContext(dbContext)
{
    public DbSet<BusinessProcess> BusinessProcesses { get; set; } = null!;
    public DbSet<RiskProfile> RiskProfiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
