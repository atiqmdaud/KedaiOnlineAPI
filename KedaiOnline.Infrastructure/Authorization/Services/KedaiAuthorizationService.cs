using KedaiOnline.Application.Users;
using KedaiOnline.Domain.Constants;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Infrastructure.Authorization.Services;

public class KedaiAuthorizationService(ILogger<KedaiAuthorizationService> logger,
    IUserContext userContext) : IKedaiAuthorizationService
{
    public bool Authorize(Kedai kedai, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for kedai {KedaiName}",
            user.Email,
            resourceOperation,
            kedai.Nama);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/Read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user.Id == kedai.OwnerId)
        {
            logger.LogInformation("Kedai owner - successful authorization");
            return true;
        }

        return false;

    }

}
