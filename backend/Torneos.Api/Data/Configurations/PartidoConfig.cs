using Torneos.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Torneos.Api.Data.Configurations;

public class PartidoConfig : IEntityTypeConfiguration<Partido>
{
    public void Configure(EntityTypeBuilder<Partido> b)
    {
        b.ToTable("Partidos", t =>
        {
            t.HasCheckConstraint(
                "CK_Partidos_Local_Distinto_Visitante",
                "\"LocalEquipoId\" <> \"VisitanteEquipoId\""
            );
        });

        b.HasKey(x => x.Id);

        b.HasOne(x => x.Torneo)
            .WithMany(t => t.Partidos)
            .HasForeignKey(x => x.TorneoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Local/Visitante
        b.Property(x => x.LocalEquipoId).IsRequired();
        b.Property(x => x.VisitanteEquipoId).IsRequired();

        // Índices básicos
        b.HasIndex(x => x.TorneoId);
        b.HasIndex(x => x.FechaUtc);

        // =========================
        // Índices recomendados Liga
        // =========================

        // Para consultas por fecha/ronda
        b.HasIndex(x => new { x.TorneoId, x.Ronda });

        // Para tabla (filtrar jugados rápido)
        b.HasIndex(x => new { x.TorneoId, x.Estado });

        // Evitar duplicados exactos en la misma ronda
        b.HasIndex(x => new { x.TorneoId, x.Ronda, x.LocalEquipoId, x.VisitanteEquipoId })
            .IsUnique();
    }
}