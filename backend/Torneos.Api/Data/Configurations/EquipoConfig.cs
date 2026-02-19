using Torneos.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Torneos.Api.Data.Configurations;

public class EquipoConfig : IEntityTypeConfiguration<Equipo>
{
    public void Configure(EntityTypeBuilder<Equipo> b)
    {
        b.ToTable("Equipos");

        b.HasKey(x => x.Id);

        b.Property(x => x.Nombre)
            .HasMaxLength(120)
            .IsRequired();

        b.HasIndex(x => x.Nombre);

        // Navegaciones de partidos: restrict para evitar cascadas conflictivas
        b.HasMany(x => x.PartidosComoLocal)
            .WithOne(p => p.LocalEquipo)
            .HasForeignKey(p => p.LocalEquipoId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasMany(x => x.PartidosComoVisitante)
            .WithOne(p => p.VisitanteEquipo)
            .HasForeignKey(p => p.VisitanteEquipoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}