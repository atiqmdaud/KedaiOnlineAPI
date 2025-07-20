using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Domain.Repositories;

public interface IKedaiOnlineRepository
{
    Task<IEnumerable<Kedai>> GetAllAsync();
    Task<Kedai?> GetByIdAsync(int id);
}
