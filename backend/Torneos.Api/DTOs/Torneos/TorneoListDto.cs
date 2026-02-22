using Torneos.Api.Domain.Entities;

namespace Torneos.Api.DTOs.Torneos;

public class TorneoListDto
{
    public long Id { get; set; }
    public string Nombre { get; set; } = null!;
    public int CantidadRuedas { get; set; }
    public TipoTorneo Tipo { get; set; }
    public int PuntosVictoria { get; set; }
    public int PuntosEmpate { get; set; }
    public int PuntosDerrota { get; set; }

    public int CantidadEquipos { get; set; }
}