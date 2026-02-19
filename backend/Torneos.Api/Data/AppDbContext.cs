using Torneos.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Torneos.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Torneo> Torneos => Set<Torneo>();
    public DbSet<Partido> Partidos => Set<Partido>();
    public DbSet<TorneoEquipo> TorneosEquipos => Set<TorneoEquipo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        TouchTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        TouchTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void TouchTimestamps()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not Domain.Entities.EntityBase e) continue;

            if (entry.State == EntityState.Added)
                e.CreatedAtUtc = utcNow;

            if (entry.State is EntityState.Added or EntityState.Modified)
                e.UpdatedAtUtc = utcNow;
        }
    }

}
