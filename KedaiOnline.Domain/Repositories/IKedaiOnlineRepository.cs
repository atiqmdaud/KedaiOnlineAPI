using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Domain.Repositories;

public interface IKedaiOnlineRepository
{
    Task<IEnumerable<Kedai>> GetAllAsync();
    Task<Kedai?> GetByIdAsync(int id);
    Task<int> CreateAsync(Kedai entity);
    Task DeleteAsync(Kedai entity);
    Task SaveChangesAsync();
    Task<IEnumerable<Kedai>> GetAllMatchingAsync(string? searchTerm);
}
