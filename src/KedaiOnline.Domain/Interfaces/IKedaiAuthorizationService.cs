using KedaiOnline.Domain.Constants;
using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Domain.Interfaces;

public interface IKedaiAuthorizationService
{
    bool Authorize(Kedai kedai, ResourceOperation resourceOperation);
}