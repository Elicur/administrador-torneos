using System.ComponentModel.DataAnnotations;
using Torneos.Api.Domain.Entities;

namespace Torneos.Api.DTOs.Torneos;

public class TorneoCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(120, ErrorMessage = "El nombre no puede superar los 120 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Range(1, 10, ErrorMessage = "La cantidad de ruedas debe ser al menos 1.")]
    public int CantidadRuedas { get; set; } = 1;

    [Required]
    public TipoTorneo Tipo { get; set; } = TipoTorneo.Liga;

    // Opcional
    [Range(0, 10)]
    public int PuntosVictoria { get; set; } = 3;

    [Range(0, 10)]
    public int PuntosEmpate { get; set; } = 1;

    [Range(0, 10)]
    public int PuntosDerrota { get; set; } = 0;
}