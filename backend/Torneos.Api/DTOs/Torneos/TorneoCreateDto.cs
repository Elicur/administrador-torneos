using System.ComponentModel.DataAnnotations;

namespace Torneos.Api.DTOs.Torneos;

public class TorneoCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(120, ErrorMessage = "El nombre no puede superar los 120 caracteres.")]
    public string Nombre { get; set; } = null!;
}