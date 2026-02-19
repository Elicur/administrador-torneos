using Torneos.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Torneos.Api.Data.Configurations;


public class TorneoConfig : IEntityTypeConfiguration<Torneo>
{
    public void Configure(EntityTypeBuilder<Torneo> b)
    {
        b.ToTable("Torneos");

        b.HasKey(x => x.Id);

        b.Property(x => x.Nombre)
            .HasMaxLength(120)
            .IsRequired();

        b.HasIndex(x => x.Nombre);
    }
}