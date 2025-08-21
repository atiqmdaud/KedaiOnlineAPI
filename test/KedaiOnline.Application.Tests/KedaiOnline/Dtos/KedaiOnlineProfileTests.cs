using AutoMapper;
using FluentAssertions;
using KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;
using KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai;
using KedaiOnline.Domain.Entities;
using Xunit;

namespace KedaiOnline.Application.KedaiOnline.Dtos.Tests;

public class KedaiOnlineProfileTests
{
    private IMapper _mapper;

    public KedaiOnlineProfileTests()
    {
        var configuration = new AutoMapper
            .MapperConfiguration(cfg => cfg.AddProfile<KedaiOnlineProfile>());

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
    {
        // Arrange
        var kedai = new Kedai()
        {
            Id = 1,
            Nama = "Test Kedai",
            Description = "This is a test kedai.",
            Category = "Test Category",
            HasDelivery = "Ya",
            ContactEmail = "test@testagain.com",
            ContactNumber = "1234567890",
            Address = new Address
            {
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12-345"
            }

        };

        // Act
        var kedaiDto = _mapper.Map<KedaiDto>(kedai);

        // Assert
        kedaiDto.Should().NotBeNull();
        kedaiDto.Id.Should().Be(kedai.Id);
        kedaiDto.Nama.Should().Be(kedai.Nama);
        kedaiDto.Description.Should().Be(kedai.Description);
        kedaiDto.Category.Should().Be(kedai.Category);
        kedaiDto.HasDelivery.Should().Be(kedai.HasDelivery);
        kedaiDto.City.Should().Be(kedai.Address.City);
        kedaiDto.Street.Should().Be(kedai.Address.Street);
        kedaiDto.PostalCode.Should().Be(kedai.Address.PostalCode);


    }

    [Fact()]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // Arrange
        var command = new CreateKedaiCommand()
        {
            Nama = "Test Kedai",
            Description = "This is a test kedai.",
            Category = "Test Category",
            HasDelivery = "Ya",
            ContactEmail = "test@testagain.com",
            ContactNumber = "1234567890",
            City = "Test City",
            Street = "Test Street",
            PostalCode = "12-345"

        };

        // Act
        var kedai = _mapper.Map<Kedai>(command);

        // Assert
        kedai.Should().NotBeNull();
        kedai.Nama.Should().Be(command.Nama);
        kedai.Description.Should().Be(command.Description);
        kedai.Category.Should().Be(command.Category);
        kedai.HasDelivery.Should().Be(command.HasDelivery);
        kedai.ContactEmail.Should().Be(command.ContactEmail);
        kedai.ContactNumber.Should().Be(command.ContactNumber);
        kedai.Address.Should().NotBeNull();
        kedai.Address.City.Should().Be(command.City);
        kedai.Address.Street.Should().Be(command.Street);
        kedai.Address.PostalCode.Should().Be(command.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // Arrange
        var command = new UpdateKedaiCommand()
        {
            Nama = "Updated Kedai",
            Description = "This is an updated desc",
            HasDelivery = "Ya"
        };

        // Act
        var kedai = _mapper.Map<Kedai>(command);

        // Assert
        kedai.Should().NotBeNull();
        kedai.Id.Should().Be(command.Id);
        kedai.Nama.Should().Be(command.Nama);
        kedai.Description.Should().Be(command.Description);
        kedai.HasDelivery.Should().Be(command.HasDelivery);
    }
}