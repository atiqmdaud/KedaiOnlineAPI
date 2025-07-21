using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Application.KedaiOnline
{
    public interface IKedaiOnlineService
    {
        Task<int> Create(CreateKedaiDto createKedaiDto);
        Task<IEnumerable<KedaiDto>> GetAllKedaiOnline();
        Task<KedaiDto?> GetKedaiOnlineById(int id);
    }
}