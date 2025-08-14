using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using KedaiOnline.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KedaiOnline.Infrastructure.Repositories;

internal class KedaiOnlineRepository(KedaiOnlineDbContext dbContext) : IKedaiOnlineRepository
{
    public async Task<int> CreateAsync(Kedai entity)
    {
        dbContext.KedaiOnline.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public Task DeleteAsync(Kedai entity)
    {
        dbContext.KedaiOnline.Remove(entity);
        //dbContext.Remove(entity);
        return dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Kedai>> GetAllAsync()
    {
        var kedaiOnline = await dbContext.KedaiOnline.ToListAsync();
        return kedaiOnline;
    }

    public async Task<IEnumerable<Kedai>> GetAllMatchingAsync(string? searchTerm)
    {
        var searchTermLower = searchTerm?.ToLower();
        var kedaiOnline = await dbContext
            .KedaiOnline
            .Where(r => searchTermLower == null || (r.Nama.ToLower().Contains(searchTermLower)
                                                 || r.Description.ToLower().Contains(searchTermLower)))
            .ToListAsync();
        return kedaiOnline;
    }

    public async Task<Kedai?> GetByIdAsync(int id)
    {
        var kedai = await dbContext.KedaiOnline
            .Include(k => k.Items)
            .FirstOrDefaultAsync(k => k.Id == id);
        return kedai;
    }

    public Task SaveChangesAsync() { 
        return dbContext.SaveChangesAsync(); 
    }
}
