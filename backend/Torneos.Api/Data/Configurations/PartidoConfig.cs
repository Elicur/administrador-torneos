using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Torneos.Api.Data.Configurations;

public class PartidoConfig : IEntityTypeConfiguration<Partido>
{
    public void Configure(EntityTypeBuilder<Partido> b)
    {
        b.ToTable("Partidos");

        b.HasKey(x => x.Id);

        b.HasOne(x => x.Torneo)
            .WithMany(t => t.Partidos)
            .HasForeignKey(x => x.TorneoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Local/Visitante se configuran desde EquipoConfig también (está ok duplicar si mantenés consistente)
        // Acá agregamos una validación simple: no dejar nulls
        b.Property(x => x.LocalEquipoId).IsRequired();
        b.Property(x => x.VisitanteEquipoId).IsRequired();

        b.HasIndex(x => x.TorneoId);
        b.HasIndex(x => x.FechaUtc);
    }
}