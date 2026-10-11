using Backend.Data.Entities;
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolarFlareController : ControllerBase
{
    private readonly ISolarFlareRepository _repository;
    private readonly NasaApiService _nasaApiService;

    // Внедряем и репозиторий, и сервис NASA через конструктор
    public SolarFlareController(ISolarFlareRepository repository, NasaApiService nasaApiService)
    {
        _repository = repository;
        _nasaApiService = nasaApiService;
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

    // НОВЫЙ МЕТОД: POST /api/solarflare/fetch-nasa
    [HttpPost("fetch-nasa")]
    public async Task<IActionResult> FetchNasaData([FromQuery] string startDate = "2023-01-01", [FromQuery] string endDate = "2023-01-10")
    {
        try
        {
            int addedFlares = await _nasaApiService.FetchAndSaveSolarFlaresAsync(startDate, endDate);
            return Ok(new { message = "Синхронизация с NASA завершена успешно!", recordsAdded = addedFlares });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Ошибка при синхронизации: {ex.Message}" });
        }

    }
}
