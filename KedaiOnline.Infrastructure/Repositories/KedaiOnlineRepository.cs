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

    public async Task<(IEnumerable<Kedai>, int)> GetAllMatchingAsync(string? searchTerm, int pageSize, int pageNumber)
    {
        var searchTermLower = searchTerm?.ToLower();

        var baseQuery = dbContext
            .KedaiOnline
            .Where(r => searchTermLower == null || (r.Nama.ToLower().Contains(searchTermLower)
                                                 || r.Description.ToLower().Contains(searchTermLower)));
        var totalCount = await baseQuery.CountAsync();

        var kedaiOnline = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        //PageSize = 5, PageNumber = 3 : skip => PageSize * (PageNumber - 1) => 5 * (3 - 1) => skip 10 items

        return (kedaiOnline, totalCount);
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
