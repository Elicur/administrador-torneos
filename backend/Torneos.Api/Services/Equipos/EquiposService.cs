using Microsoft.EntityFrameworkCore;
using Torneos.Api.Data;
using Torneos.Api.Domain.Entities;
using Torneos.Api.DTOs.Equipos;

namespace Torneos.Api.Services.Equipos;

public class EquiposService : IEquiposService
{
    private readonly AppDbContext _db;

    public EquiposService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<EquipoListDto> CreateAsync(EquipoCreateDto dto, CancellationToken ct = default)
    {
        var nombre = (dto.Nombre ?? "").Trim();

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del equipo es obligatorio.");


        var entity = new Equipo
        {
            Nombre = nombre
        };

        _db.Equipos.Add(entity);
        await _db.SaveChangesAsync(ct);

        return new EquipoListDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre
        };
    }

    public async Task<List<EquipoListDto>> ListAsync(CancellationToken ct = default)
    {
        return await _db.Equipos
            .AsNoTracking()
            .OrderBy(e => e.Nombre)
            .Select(e => new EquipoListDto
            {
                Id = e.Id,
                Nombre = e.Nombre
            })
            .ToListAsync(ct);
    }
}
