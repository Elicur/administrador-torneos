using Torneos.Api.DTOs.Partidos;

public class RondaFixtureDto
{
    public int Ronda { get; set; }
    public List<PartidoFixtureDto> Partidos { get; set; } = new();
}