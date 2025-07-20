using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using KedaiOnline.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KedaiOnline.Infrastructure.Repositories;

internal class KedaiOnlineRepository(KedaiOnlineDbContext dbContext) : IKedaiOnlineRepository
{
    public async Task<IEnumerable<Kedai>> GetAllAsync()
    {
        var kedaiOnline = await dbContext.KedaiOnline.ToListAsync();
        return kedaiOnline;

    }

    public async Task<Kedai?> GetByIdAsync(int id)
    {
        var kedai = await dbContext.KedaiOnline
            .Include(k => k.Items)
            .FirstOrDefaultAsync(k => k.Id == id);
        return kedai;
    }
}
