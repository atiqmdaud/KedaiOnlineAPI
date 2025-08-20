using AutoMapper;
using KedaiOnline.Application.Users;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;

public class CreateKedaiCommandHandler(ILogger<CreateKedaiCommandHandler> logger,
    IMapper mapper,IKedaiOnlineRepository kedaiOnlineRepository,
    IUserContext userContext) : IRequestHandler<CreateKedaiCommand, int>
{
    public async Task<int> Handle(CreateKedaiCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{UserEmail} [{UserId}] is creating a new Kedai Online {@Kedai}",
            currentUser.Email,
            currentUser.Id,
            request);

        var kedai = mapper.Map<Kedai>(request);
        kedai.OwnerId = currentUser.Id;

        int id = await kedaiOnlineRepository.CreateAsync(kedai);
        return id;
    }
}
