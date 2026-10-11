using Backend.Data.Entities;
namespace Backend.Repositories;


public interface ISolarFlareRepository
{
    Task<IEnumerable<SolarFlare>> GetAllAsync();
    Task<SolarFlare?> GetByIdAsync(string id);
    Task AddAsync(SolarFlare flare);
    Task UpdateAsync(SolarFlare flare);
    Task DeleteAsync(string id);
    Task<bool> SaveChangesAsync();
}
