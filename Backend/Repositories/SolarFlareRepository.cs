using Backend.Data;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Backend.Repositories;

namespace Backend.Repositories;

public class SolarFlareRepository : ISolarFlareRepository
{
    private readonly AppDbContext _context;

    public SolarFlareRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SolarFlare>> GetAllAsync()
    {
        return await _context.SolarFlares.ToListAsync();
    }

    public async Task<SolarFlare?> GetByIdAsync(string id)
    {
        return await _context.SolarFlares.FindAsync(id);
    }

    public async Task AddAsync(SolarFlare flare)
    {
        await _context.SolarFlares.AddAsync(flare);
    }

    public async Task UpdateAsync(SolarFlare flare)
    {
        _context.SolarFlares.Update(flare);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(string id)
    {
        var flare = await _context.SolarFlares.FindAsync(id);
        if (flare != null)
        {
            _context.SolarFlares.Remove(flare);
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
