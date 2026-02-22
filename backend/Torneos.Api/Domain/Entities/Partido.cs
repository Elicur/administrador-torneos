namespace Torneos.Api.Domain.Entities;

public enum EstadoPartido
{
    Programado = 1,
    Jugado = 2,
    Suspendido = 3
}

public enum TipoDefinicion
{
    NoAplica = 0,      // liga / grupos
    Noventa = 1,       // se definió en 90'
    Alargue = 2,       // se definió en 120'
    Penales = 3        // se definió por penales
}

public class Partido : EntityBase
{
    public long TorneoId { get; set; }
    public Torneo Torneo { get; set; } = null!;

    public long LocalEquipoId { get; set; }
    public Equipo LocalEquipo { get; set; } = null!;

    public long VisitanteEquipoId { get; set; }
    public Equipo VisitanteEquipo { get; set; } = null!;
    
    public DateTime? FechaUtc { get; set; }

    public EstadoPartido Estado { get; set; } = EstadoPartido.Programado;

    // Metadata de fixture
    public int? Ronda { get; set; }      // fecha 1..N (liga) o round (elim)
    public int? Llave { get; set; }      // 1..N para bracket

    // Resultado en 90' (SIEMPRE que se juegue)
    public int? GolesLocal { get; set; }
    public int? GolesVisitante { get; set; }

    // Alargue (solo si aplica)
    public int? GolesLocalAlargue { get; set; }      // goles adicionales en ET
    public int? GolesVisitanteAlargue { get; set; }  // goles adicionales en ET

    // Penales (solo si aplica)
    public int? PenalesLocal { get; set; }
    public int? PenalesVisitante { get; set; }

    // Para resolver avance en eliminatoria
    public TipoDefinicion TipoDefinicion { get; set; } = TipoDefinicion.NoAplica;
    public long? GanadorEquipoId { get; set; } // Local o Visitante si define avance; null si no aplica
}