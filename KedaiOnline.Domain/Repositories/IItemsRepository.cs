using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Domain.Repositories;

public interface IItemsRepository
{
    Task<int> CreateAsync(Item entity);
}
