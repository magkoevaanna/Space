using Backend.Data.Entities;
using Backend.Repositories;
using System.Text.Json;

namespace Backend.Services;

public class NasaApiService
{
    private readonly HttpClient _httpClient;
    private readonly ISolarFlareRepository _repository;
    private readonly string _apiKey = "P4RrAbhnfRyMQWKA35loajHxabKpuzmDPgYqgNIQ"; 

    public NasaApiService(HttpClient httpClient, ISolarFlareRepository repository)
    {
        _httpClient = httpClient;
        _repository = repository;
    }

    public async Task<int> FetchAndSaveSolarFlaresAsync(string startDate, string endDate)
    {
        var url = $"https://ccmc.gsfc.nasa.gov/DONKI-API/get/FLR?startDate={startDate}&endDate={endDate}&api_key={_apiKey}";

        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ошибка при обращении к NASA API: {response.ReasonPhrase}");
        }

        // Читаем ответ как строку
        var jsonString = await response.Content.ReadAsStringAsync();

        using var jsonDoc = JsonDocument.Parse(jsonString);
        int addedCount = 0;

        foreach (var element in jsonDoc.RootElement.EnumerateArray())
        {
            var flrId = element.GetProperty("flrID").GetString() ?? string.Empty;

            var existingFlare = await _repository.GetByIdAsync(flrId);
            if (existingFlare != null) continue;

            var flare = new SolarFlare
            {
                FlrID = flrId,
                Catalog = element.GetProperty("catalog").GetString(),
                ClassType = element.GetProperty("classType").GetString(),
                SourceLocation = element.GetProperty("sourceLocation").GetString(),
                Note = element.GetProperty("note").GetString(),
                Link = element.GetProperty("link").GetString(),
                
                BeginTime = element.TryGetProperty("beginTime", out var bTime) && bTime.ValueKind != JsonValueKind.Null ? bTime.GetDateTime() : null,
                PeakTime = element.TryGetProperty("peakTime", out var pTime) && pTime.ValueKind != JsonValueKind.Null ? pTime.GetDateTime() : null,
                EndTime = element.TryGetProperty("endTime", out var eTime) && eTime.ValueKind != JsonValueKind.Null ? eTime.GetDateTime() : null,
                SubmissionTime = element.TryGetProperty("submissionTime", out var subTime) && subTime.ValueKind != JsonValueKind.Null ? subTime.GetDateTime() : null,
                
                ActiveRegionNum = element.TryGetProperty("activeRegionNum", out var regNum) && regNum.ValueKind == JsonValueKind.Number ? regNum.GetInt32() : null,
                VersionId = element.TryGetProperty("versionId", out var verId) && verId.ValueKind == JsonValueKind.Number ? verId.GetInt32() : null,

                Instruments = element.GetProperty("instruments").GetRawText(),
                LinkedEvents = element.GetProperty("linkedEvents").GetRawText(),
                SentNotifications = element.TryGetProperty("sentNotifications", out var notes) ? notes.GetRawText() : null
            };

            await _repository.AddAsync(flare);
            addedCount++;
        }

        if (addedCount > 0)
        {
            await _repository.SaveChangesAsync();
        }

        return addedCount;
    }
}
