using AutoMapper;
using FluentAssertions;
using KedaiOnline.Application.Users;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KedaiOnline.Application.KedaiOnline.Commands.CreateKedai.Tests;

public class CreateKedaiCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        //arrange
        var loggerMock = new Mock<ILogger<CreateKedaiCommandHandler>>();

        var mapperMock = new Mock<IMapper>();
        var command = new CreateKedaiCommand();
        var kedai = new Kedai();

        mapperMock
            .Setup(m => m.Map<Kedai>(command))
            .Returns(kedai);

        var kedaiOnlineRepositoryMock = new Mock<IKedaiOnlineRepository>();
        kedaiOnlineRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Kedai>()))
            .ReturnsAsync(1); // Simulate returning a created Kedai ID

        var userContextMock = new Mock<IUserContext>();
        var CurrentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock
            .Setup(uc => uc.GetCurrentUser())
            .Returns(CurrentUser);


        var commandHandler = new CreateKedaiCommandHandler(
            loggerMock.Object,
            mapperMock.Object,
            kedaiOnlineRepositoryMock.Object,
            userContextMock.Object
        );

        //act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //assert
        result.Should().Be(1);
        kedai.OwnerId.Should().Be("owner-id");
        kedaiOnlineRepositoryMock.Verify(repo => repo.CreateAsync(kedai), Times.Once);

    }
}