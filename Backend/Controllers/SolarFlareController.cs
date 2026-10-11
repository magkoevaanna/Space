using Backend.Data.Entities;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class SolarFlareController : ControllerBase
{
    private readonly ISolarFlareRepository _repository;

    public SolarFlareController(ISolarFlareRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SolarFlare>>> GetAllFlares()
    {
        var flares = await _repository.GetAllAsync();
        return Ok(flares);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SolarFlare>> GetFlareById(string id)
    {
        var flare = await _repository.GetByIdAsync(id);
        if (flare == null)
        {
            return NotFound(new { message = $"Вспышка с ID {id} не найдена." });
        }
        return Ok(flare);
    }

    [HttpPost]
    public async Task<ActionResult<SolarFlare>> CreateFlare([FromBody] SolarFlare flare)
    {
        if (flare == null)
        {
            return BadRequest(new { message = "Некорректные данные вспышки." });
        }

        await _repository.AddAsync(flare);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFlareById), new { id = flare.FlrID }, flare);
    }
}
