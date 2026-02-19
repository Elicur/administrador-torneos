namespace Domain.Entities;

public class TorneoEquipo
{
    // PK compuesta por TorneoId y EquipoId
    public long TorneoId { get; set; }
    public Torneo Torneo { get; set; } = null!;

    public long EquipoId { get; set; }
    public Equipo Equipo { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}