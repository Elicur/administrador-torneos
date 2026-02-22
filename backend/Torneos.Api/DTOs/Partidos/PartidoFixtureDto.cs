namespace Torneos.Api.DTOs.Partidos;

public class PartidoFixtureDto
{
    public long Id { get; set; }

    public int? Ronda { get; set; }

    public string Local { get; set; } = null!;
    public string Visitante { get; set; } = null!;

    public DateTime? FechaUtc { get; set; }

    public int? GolesLocal { get; set; }
    public int? GolesVisitante { get; set; }

    public string Estado { get; set; } = null!;
}