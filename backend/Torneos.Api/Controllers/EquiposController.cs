using Microsoft.AspNetCore.Mvc;
using Torneos.Api.DTOs.Equipos;
using Torneos.Api.Services.Equipos;

namespace Torneos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquiposController : ControllerBase
{
    private readonly IEquiposService _service;

    public EquiposController(IEquiposService service)
    {
        _service = service;
    }

    // GET api/equipos
    [HttpGet]
    public async Task<ActionResult<List<EquipoListDto>>> List(CancellationToken ct)
    {
        var result = await _service.ListAsync(ct);
        return Ok(result);
    }

    // POST api/equipos
    [HttpPost]
    public async Task<ActionResult<EquipoListDto>> Create([FromBody] EquipoCreateDto dto, CancellationToken ct)
    {
        try
        {
            var created = await _service.CreateAsync(dto, ct);
            // 201 + ubicación del recurso (si querés endpoint GET by id después)
            return CreatedAtAction(nameof(List), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
