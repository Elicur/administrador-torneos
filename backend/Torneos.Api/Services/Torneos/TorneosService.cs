using Microsoft.EntityFrameworkCore;
using Torneos.Api.Data;
using Torneos.Api.Domain.Entities;
using Torneos.Api.DTOs.Torneos;
using Torneos.Api.DTOs.Equipos;

namespace Torneos.Api.Services.Torneos;

public class TorneosService : ITorneosService
{
    private readonly AppDbContext _db;

    public TorneosService(AppDbContext db)
    {
        _db = db;
    }

    // Crear torneo
    public async Task<TorneoListDto> CreateAsync(TorneoCreateDto dto, CancellationToken ct = default)
    {
        var nombre = (dto.Nombre ?? "").Trim();

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del torneo es obligatorio.");

        if (dto.CantidadRuedas < 1)
            throw new ArgumentException("La cantidad de ruedas debe ser al menos 1.");

        var entity = new Torneo
        {
            Nombre = nombre,
            CantidadRuedas = dto.CantidadRuedas,
            Tipo = dto.Tipo,
            PuntosVictoria = dto.PuntosVictoria,
            PuntosEmpate = dto.PuntosEmpate,
            PuntosDerrota = dto.PuntosDerrota
        };

        _db.Torneos.Add(entity);
        await _db.SaveChangesAsync(ct);

        return new TorneoListDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            CantidadRuedas = entity.CantidadRuedas,
            Tipo = entity.Tipo,
            PuntosVictoria = entity.PuntosVictoria,
            PuntosEmpate = entity.PuntosEmpate,
            PuntosDerrota = entity.PuntosDerrota
        };
    }

    // Listar torneos
    public async Task<List<TorneoListDto>> ListAsync(CancellationToken ct = default)
    {
        return await _db.Torneos
            .AsNoTracking()
            .Select(t => new TorneoListDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                CantidadRuedas = t.CantidadRuedas,
                Tipo = t.Tipo,
                PuntosVictoria = t.PuntosVictoria,
                PuntosEmpate = t.PuntosEmpate,
                PuntosDerrota = t.PuntosDerrota,
                CantidadEquipos = _db.TorneosEquipos.Count(te => te.TorneoId == t.Id)
            })
            .OrderBy(t => t.Nombre)
            .ToListAsync(ct);
    }

    // Inscribir equipo en torneo
    public async Task InscribirEquipoAsync(long torneoId, long equipoId, CancellationToken ct = default)
    {
        // 1) Validar que existe el torneo
        var torneoExists = await _db.Torneos.AnyAsync(t => t.Id == torneoId, ct);
        if (!torneoExists)
            throw new KeyNotFoundException("No existe el torneo indicado.");

        // 2) Validar que existe el equipo
        var equipoExists = await _db.Equipos.AnyAsync(e => e.Id == equipoId, ct);
        if (!equipoExists)
            throw new KeyNotFoundException("No existe el equipo indicado.");

        // 3) Evitar duplicados (además de PK compuesta)
        var already = await _db.TorneosEquipos
            .AnyAsync(te => te.TorneoId == torneoId && te.EquipoId == equipoId, ct);

        if (already)
            throw new InvalidOperationException("El equipo ya está inscripto en este torneo.");

        // 4) Crear relación
        _db.TorneosEquipos.Add(new TorneoEquipo
        {
            TorneoId = torneoId,
            EquipoId = equipoId
        });

        await _db.SaveChangesAsync(ct);
    }

    // Listar equipos inscriptos en torneo
    public async Task<List<EquipoListDto>> ListEquiposAsync(long torneoId, CancellationToken ct = default)
    {
        // Validar torneo (para devolver 404 en vez de lista vacía si no existe)
        var torneoExists = await _db.Torneos.AnyAsync(t => t.Id == torneoId, ct);
        if (!torneoExists)
            throw new KeyNotFoundException("No existe el torneo indicado.");

        // Traer equipos inscriptos (join TorneosEquipos -> Equipos)
        return await _db.TorneosEquipos
            .AsNoTracking()
            .Where(te => te.TorneoId == torneoId)
            .Join(_db.Equipos.AsNoTracking(),
                te => te.EquipoId,
                e => e.Id,
                (te, e) => new EquipoListDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre
                })
            .OrderBy(x => x.Nombre)
            .ToListAsync(ct);
    }
}