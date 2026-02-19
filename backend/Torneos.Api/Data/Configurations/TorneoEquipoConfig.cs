using Torneos.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Torneos.Api.Data.Configurations;

public class TorneoEquipoConfig : IEntityTypeConfiguration<TorneoEquipo>
{
    public void Configure(EntityTypeBuilder<TorneoEquipo> b)
    {
        b.ToTable("TorneosEquipos");

        b.HasKey(x => new { x.TorneoId, x.EquipoId });

        b.HasOne(x => x.Torneo)
            .WithMany(t => t.TorneosEquipos)
            .HasForeignKey(x => x.TorneoId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Equipo)
            .WithMany(e => e.TorneosEquipos)
            .HasForeignKey(x => x.EquipoId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.EquipoId);
        b.HasIndex(x => x.TorneoId);
    }
}