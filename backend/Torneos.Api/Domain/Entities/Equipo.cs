namespace Domain.Entities;

public class Equipo : EntityBase
{
    public string Nombre { get; set; } = null!;

    // Navegación many-to-many
    public ICollection<TorneoEquipo> TorneosEquipos { get; set; } = new List<TorneoEquipo>();

    // Navegaciones de partidos (opcional, pero útil)
    public ICollection<Partido> PartidosComoLocal { get; set; } = new List<Partido>();
    public ICollection<Partido> PartidosComoVisitante { get; set; } = new List<Partido>();
}