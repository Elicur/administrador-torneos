using Torneos.Api.DTOs.Torneos;
using Torneos.Api.DTOs.Equipos;

namespace Torneos.Api.Services.Torneos;

public interface ITorneosService
{
    Task<TorneoListDto> CreateAsync(TorneoCreateDto dto, CancellationToken ct = default);
    Task<List<TorneoListDto>> ListAsync(CancellationToken ct = default);

    Task InscribirEquipoAsync(long torneoId, long equipoId, CancellationToken ct = default);
    Task<List<EquipoListDto>> ListEquiposAsync(long torneoId, CancellationToken ct = default);
}