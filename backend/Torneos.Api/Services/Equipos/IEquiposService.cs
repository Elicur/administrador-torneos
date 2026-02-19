using Torneos.Api.DTOs.Equipos;

namespace Torneos.Api.Services.Equipos;

public interface IEquiposService
{
    Task<EquipoListDto> CreateAsync(EquipoCreateDto dto, CancellationToken ct = default);
    Task<List<EquipoListDto>> ListAsync(CancellationToken ct = default);
}
