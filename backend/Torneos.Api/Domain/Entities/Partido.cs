namespace Torneos.Api.Domain.Entities;

public class Partido : EntityBase
{
    public long TorneoId { get; set; }
    public Torneo Torneo { get; set; } = null!;

    public long LocalEquipoId { get; set; }
    public Equipo LocalEquipo { get; set; } = null!;

    public long VisitanteEquipoId { get; set; }
    public Equipo VisitanteEquipo { get; set; } = null!;
    
    public DateTime FechaUtc { get; set; }

    public int? GolesLocal { get; set; }
    public int? GolesVisitante { get; set; }
}