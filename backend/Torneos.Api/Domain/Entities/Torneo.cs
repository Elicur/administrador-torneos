namespace Torneos.Api.Domain.Entities;

public enum TipoTorneo
{
    Liga = 1,
    Eliminatoria = 2,
    GruposYEliminatoria = 3
}

public enum EstadoTorneo
{
    Borrador = 1,
    EnCurso = 2,
    Finalizado = 3
}

public class Torneo : EntityBase
{
    public string Nombre { get; set; } = null!;

    public TipoTorneo Tipo { get; set; } = TipoTorneo.Liga;
    public EstadoTorneo Estado { get; set; } = EstadoTorneo.Borrador;

    public int PuntosVictoria { get; set; } = 3;
    public int PuntosEmpate { get; set; } = 1;
    public int PuntosDerrota { get; set; } = 0;

    public int CantidadRuedas { get; set; } = 1; // 1 = todos contra todos, 2 = ida y vuelta, etc.

    public ICollection<TorneoEquipo> TorneosEquipos { get; set; } = new List<TorneoEquipo>();
    public ICollection<Partido> Partidos { get; set; } = new List<Partido>();
}