using System.ComponentModel.DataAnnotations;

namespace Torneos.Api.DTOs.Torneos;

public class InscribirEquipoDto
{
    [Required]
    public long EquipoId { get; set; }
}