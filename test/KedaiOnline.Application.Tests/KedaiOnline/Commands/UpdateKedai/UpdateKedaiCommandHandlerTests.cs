using AutoMapper;
using FluentAssertions;
using KedaiOnline.Domain.Constants;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Interfaces;
using KedaiOnline.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai.Tests;

public class UpdateKedaiCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateKedaiCommandHandler>> _loggerMock;
    private readonly Mock<IKedaiOnlineRepository> _kedaiOnlineRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IKedaiAuthorizationService> _kedaiAuthorizationServiceMock;

    private readonly UpdateKedaiCommandHandler _handler;

    public UpdateKedaiCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateKedaiCommandHandler>>();
        _kedaiOnlineRepositoryMock = new Mock<IKedaiOnlineRepository>();
        _mapperMock = new Mock<IMapper>();
        _kedaiAuthorizationServiceMock = new Mock<IKedaiAuthorizationService>();

        _handler = new UpdateKedaiCommandHandler(
            _loggerMock.Object,
            _kedaiOnlineRepositoryMock.Object,
            _mapperMock.Object,
            _kedaiAuthorizationServiceMock.Object);
    }


    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateKedaiOnline()
    {

        // Arrange
        var kedaiId = 1;
        var command = new UpdateKedaiCommand
        {
            Id = kedaiId,
            Nama = "Updated Kedai",
            Description = "Updated Description",
            HasDelivery = "Ya",
        };

        var kedai = new Kedai
        {
            Id = kedaiId,
            Nama = "Old Kedai",
            Description = "Old Description"
        };

        _kedaiOnlineRepositoryMock.Setup(repo => repo.GetByIdAsync(kedaiId))
            .ReturnsAsync(kedai);

        _kedaiAuthorizationServiceMock.Setup(auth => auth.Authorize(kedai, Domain.Constants.ResourceOperation.Update))
            .Returns(true);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _kedaiOnlineRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, kedai), Times.Once);

    }

    [Fact()]
    public async Task Handle_WithNonExistingKedai_ShouldThrowNotFoundException()
    { 
        // Arrange
        var kedaiId = 2;
        var request = new UpdateKedaiCommand
        {
            Id = kedaiId,
        };

        _kedaiOnlineRepositoryMock.Setup(repo => repo.GetByIdAsync(kedaiId))
            .ReturnsAsync((Kedai?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Kedai with id: {kedaiId} doesnt exist");
    }
    [Fact()]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {         // Arrange
        var kedaiId = 3;
        var request = new UpdateKedaiCommand
        {
            Id = kedaiId,
        };
        var existingKedai = new Kedai
        { 
            Id = kedaiId,
        };

        _kedaiOnlineRepositoryMock.Setup(repo => repo.GetByIdAsync(kedaiId))
            .ReturnsAsync(existingKedai);

        _kedaiAuthorizationServiceMock.Setup(auth => auth.Authorize(existingKedai, ResourceOperation.Update))
            .Returns(false);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbidException>();

    }

}