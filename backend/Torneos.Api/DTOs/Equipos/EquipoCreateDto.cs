using System.ComponentModel.DataAnnotations;

namespace Torneos.Api.DTOs.Equipos;

public class EquipoCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(120, ErrorMessage = "El nombre no puede superar los 120 caracteres.")]
    public string Nombre { get; set; } = null!;
}
