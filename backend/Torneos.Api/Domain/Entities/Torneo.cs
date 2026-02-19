namespace Torneos.Api.Domain.Entities;

public class Torneo : EntityBase
{
    public string Nombre { get; set; } = null!;

    public ICollection<TorneoEquipo> TorneosEquipos { get; set; } = new List<TorneoEquipo>();
    public ICollection<Partido> Partidos { get; set; } = new List<Partido>();
}