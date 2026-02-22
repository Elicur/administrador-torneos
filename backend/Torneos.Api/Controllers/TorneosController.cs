using Microsoft.AspNetCore.Mvc;
using Torneos.Api.DTOs.Torneos;
using Torneos.Api.DTOs.Equipos;
using Torneos.Api.Services.Torneos;
using Torneos.Api.Services.Fixtures;

namespace Torneos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TorneosController : ControllerBase
{
    private readonly ITorneosService _service;
    private readonly IFixtureService _fixtureService;

    public TorneosController(ITorneosService service, IFixtureService fixtureService)
    {
        _service = service;
        _fixtureService = fixtureService;
    }

    // GET api/torneos
    [HttpGet]
    public async Task<ActionResult<List<TorneoListDto>>> List(CancellationToken ct)
    {
        var result = await _service.ListAsync(ct);
        return Ok(result);
    }

    // POST api/torneos
    [HttpPost]
    public async Task<ActionResult<TorneoListDto>> Create([FromBody] TorneoCreateDto dto, CancellationToken ct)
    {
        try
        {
            var created = await _service.CreateAsync(dto, ct);
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

    // POST api/torneos/{torneoId}/equipos
    [HttpPost("{torneoId:long}/equipos")]
    public async Task<IActionResult> InscribirEquipo(
        [FromRoute] long torneoId,
        [FromBody] InscribirEquipoDto dto,
        CancellationToken ct)
    {
        try
        {
            await _service.InscribirEquipoAsync(torneoId, dto.EquipoId, ct);
            return NoContent(); // 204
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message }); // 409
        }
    }

    // GET api/torneos/{torneoId}/equipos
    [HttpGet("{torneoId:long}/equipos")]
    public async Task<ActionResult<List<EquipoListDto>>> ListEquipos(
        [FromRoute] long torneoId,
        CancellationToken ct)
    {
        try
        {
            var result = await _service.ListEquiposAsync(torneoId, ct);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST api/torneos/{torneoId}/fixture/liga
    [HttpPost("{torneoId:long}/fixture/liga")]
    public async Task<ActionResult> GenerarFixtureLiga(long torneoId, CancellationToken ct)
    {
        await _fixtureService.GenerarLigaAsync(torneoId, ct);
        return NoContent();
    }

    // GET api/torneos/{torneoId}/fixture
    [HttpGet("{torneoId:long}/fixture")]
    public async Task<ActionResult<List<RondaFixtureDto>>> GetFixture(long torneoId, CancellationToken ct)
    {
        var fixture = await _fixtureService.GetFixtureAsync(torneoId, ct);
        return Ok(fixture);
    }
}