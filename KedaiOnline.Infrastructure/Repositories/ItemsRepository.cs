using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using KedaiOnline.Infrastructure.Persistence;

namespace KedaiOnline.Infrastructure.Repositories;

internal class ItemsRepository(KedaiOnlineDbContext dbContext) : IItemsRepository
{
    public async Task<int> CreateAsync(Item entity)
    {
        dbContext.Items.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
