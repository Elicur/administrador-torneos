using Microsoft.EntityFrameworkCore;

namespace Torneos.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Después agregaremos DbSet<Equipo>, DbSet<Torneo>, etc.
}
