using AutoMapper;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline;

internal class KedaiOnlineService(IKedaiOnlineRepository kedaiOnlineRepository, ILogger<KedaiOnlineService> logger, IMapper mapper) : IKedaiOnlineService
{
    public async Task<int> Create(CreateKedaiDto dto)
    {
        logger.LogInformation("Creating a new Kedai Online");
        var kedai = mapper.Map<Kedai>(dto);
        int id = await kedaiOnlineRepository.CreateAsync(kedai);
        return id;
    }

    public async Task<IEnumerable<KedaiDto>> GetAllKedaiOnline()
    {
        logger.LogInformation("Fetching all Kedai Online");
        var kedaiOnline = await kedaiOnlineRepository.GetAllAsync();
        //var kedaiDtos = kedaiOnline.Select(KedaiDto.FromEntity);
        var kedaiDtos = mapper.Map<IEnumerable<KedaiDto>>(kedaiOnline);
        return kedaiDtos!;// Ensure non-null return type mybe empty list if no kedai found
    }

    public async Task<KedaiDto?> GetKedaiOnlineById(int id)
    {
        logger.LogInformation($"Fetching Kedai Online with ID: {id}");
        var kedai = await kedaiOnlineRepository.GetByIdAsync(id);
        //var kedaiDto = kedai != null ? KedaiDto.FromEntity(kedai) : null;
        //var kedaiDto = KedaiDto.FromEntity(kedai);
        var kedaiDto = mapper.Map<KedaiDto?>(kedai);
        return kedaiDto;
    }
}
