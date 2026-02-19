namespace Torneos.Api.DTOs.Torneos;

public class TorneoListDto
{
    public long Id { get; set; }
    public string Nombre { get; set; } = null!;
    public int CantidadEquipos { get; set; }
}