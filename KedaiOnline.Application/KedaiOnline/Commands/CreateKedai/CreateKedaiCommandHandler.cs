using AutoMapper;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;

public class CreateKedaiCommandHandler(ILogger<CreateKedaiCommandHandler> logger,
    IMapper mapper,IKedaiOnlineRepository kedaiOnlineRepository) : IRequestHandler<CreateKedaiCommand, int>
{
    public async Task<int> Handle(CreateKedaiCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new Kedai Online {@Kedai}", request);
        var kedai = mapper.Map<Kedai>(request);
        int id = await kedaiOnlineRepository.CreateAsync(kedai);
        return id;
    }
}
