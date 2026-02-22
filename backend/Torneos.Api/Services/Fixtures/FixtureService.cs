using Microsoft.EntityFrameworkCore;
using Torneos.Api.Data;
using Torneos.Api.Domain.Entities;
using Torneos.Api.DTOs.Partidos;

namespace Torneos.Api.Services.Fixtures;

public class FixtureService : IFixtureService
{
    private readonly AppDbContext _db;

    public FixtureService(AppDbContext db)
    {
        _db = db;
    }

    public async Task GenerarLigaAsync(long torneoId, CancellationToken ct = default)
    {
        var torneo = await _db.Torneos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == torneoId, ct);

        if (torneo is null)
            throw new ArgumentException("Torneo no encontrado.");

        if (torneo.Tipo != TipoTorneo.Liga)
            throw new ArgumentException("El torneo no es de tipo Liga.");

        if (torneo.CantidadRuedas < 1)
            throw new ArgumentException("CantidadRuedas inválida (debe ser >= 1).");

        var yaTienePartidos = await _db.Partidos.AnyAsync(p => p.TorneoId == torneoId, ct);
        if (yaTienePartidos)
            throw new InvalidOperationException("El torneo ya tiene partidos. No se puede generar el fixture nuevamente.");

        // Equipos inscriptos al torneo
        var equipoIds = await _db.TorneosEquipos
            .Where(te => te.TorneoId == torneoId)
            .Select(te => te.EquipoId)
            .OrderBy(id => id) // determinístico
            .ToListAsync(ct);

        if (equipoIds.Count < 2)
            throw new ArgumentException("Se requieren al menos 2 equipos para generar un fixture.");

        // Circle method necesita lista "mutable"
        var lista = new List<long>(equipoIds);

        // BYE si impar
        const long BYE = -1;
        var tieneBye = (lista.Count % 2 != 0);
        if (tieneBye)
            lista.Add(BYE);

        var n = lista.Count;          // par
        var partidosPorRonda = n / 2;
        var rondasPorRueda = n - 1;   // fechas

        var partidos = new List<Partido>(capacity: rondasPorRueda * partidosPorRonda * torneo.CantidadRuedas);

        // Copia de trabajo para rotación
        var rot = new List<long>(lista);

        // Generación por ruedas
        for (int rueda = 0; rueda < torneo.CantidadRuedas; rueda++)
        {
            // Para cada rueda, arrancamos con la misma rotación base
            rot = new List<long>(lista);

            for (int ronda = 1; ronda <= rondasPorRueda; ronda++)
            {
                for (int i = 0; i < partidosPorRonda; i++)
                {
                    var a = rot[i];
                    var b = rot[n - 1 - i];

                    if (a == BYE || b == BYE)
                        continue;

                    // Localías:
                    // - En rueda 0 (ida), asignación base.
                    // - En rueda 1 (vuelta), invertimos local/visitante.
                    // - En ruedas 2+, alternamos inversión por paridad.
                    var invertir = (rueda % 2 == 1);

                    long localId = invertir ? b : a;
                    long visitId = invertir ? a : b;

                    // Truquito para balancear mejor el primer "ancla"
                    // (ayuda a que no quede siempre local el mismo equipo en la primera pareja)
                    if (i == 0 && (ronda % 2 == 0))
                    {
                        (localId, visitId) = (visitId, localId);
                    }

                    partidos.Add(new Partido
                    {
                        TorneoId = torneoId,
                        LocalEquipoId = localId,
                        VisitanteEquipoId = visitId,
                        FechaUtc = null, // se agenda después
                        Estado = EstadoPartido.Programado,
                        Ronda = (rueda * rondasPorRueda) + ronda,
                        GolesLocal = null,
                        GolesVisitante = null
                    });
                }

                // Rotación circle method:
                // dejamos fijo el primero, rotamos el resto
                // [0] fijo, [1..n-1] rota a la derecha 1
                var last = rot[n - 1];
                for (int j = n - 1; j >= 2; j--)
                    rot[j] = rot[j - 1];
                rot[1] = last;
            }
        }

        // Guardar
        _db.Partidos.AddRange(partidos);
        await _db.SaveChangesAsync(ct);
    }

    // Obtiene el fixture completo para un torneo, ordenado por ronda
    public async Task<List<RondaFixtureDto>> GetFixtureAsync(long torneoId, CancellationToken ct = default)
    {
        var existe = await _db.Torneos
            .AnyAsync(t => t.Id == torneoId, ct);

        if (!existe)
            throw new ArgumentException("Torneo no encontrado.");

        var partidos = await _db.Partidos
            .Where(p => p.TorneoId == torneoId)
            .Include(p => p.LocalEquipo)
            .Include(p => p.VisitanteEquipo)
            .OrderBy(p => p.Ronda)
            .ThenBy(p => p.Id)
            .ToListAsync(ct);

        var resultado = partidos
            .GroupBy(p => p.Ronda ?? 0)
            .OrderBy(g => g.Key)
            .Select(g => new RondaFixtureDto
            {
                Ronda = g.Key,
                Partidos = g.Select(p => new PartidoFixtureDto
                {
                    Id = p.Id,
                    Ronda = p.Ronda,
                    Local = p.LocalEquipo.Nombre,
                    Visitante = p.VisitanteEquipo.Nombre,
                    FechaUtc = p.FechaUtc,
                    GolesLocal = p.GolesLocal,
                    GolesVisitante = p.GolesVisitante,
                    Estado = p.Estado.ToString()
                }).ToList()
            })
            .ToList();

        return resultado;
    }
}