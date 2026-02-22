namespace Torneos.Api.Services.Fixtures;

public interface IFixtureService
{
    // Genera el fixture para un torneo de tipo Liga.
    Task GenerarLigaAsync(long torneoId, CancellationToken ct = default);

    // Obtiene el fixture completo para un torneo, ordenado por ronda
    Task<List<RondaFixtureDto>> GetFixtureAsync(long torneoId, CancellationToken ct = default);
}