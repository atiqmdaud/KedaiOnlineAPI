using System.Net.Http.Json;
using FluentAssertions;
using KedaiOnline.API.Tests;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Repositories;
using KedaiOnline.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace KedaiOnline.API.Controllers.Tests;

public class KedaiOnlineControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IKedaiOnlineRepository> _kedaiOnlineRepositoryMock = new();
    private readonly Mock<IKedaiSeeder> _kedaiSeederMock = new();
    public KedaiOnlineControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // You can configure test-specific services here if needed
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IKedaiOnlineRepository),
                                                 _ => _kedaiOnlineRepositoryMock.Object));
                services.Replace(ServiceDescriptor.Scoped(typeof(IKedaiSeeder),
                                                 _ => _kedaiSeederMock.Object));

            });
        });
    }

    [Fact()]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        // Arrange
        var id = 999; // Assuming this ID does not exist

        _kedaiOnlineRepositoryMock.Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync((Kedai?)null);

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/kedaionline/{id}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact()]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // Arrange
        var id = 1;
        var kedai = new Kedai
        {
            Id = id,
            Nama = "Test Kedai",
            Description = "A test kedai",
            Category = "Food",
            HasDelivery = "Yes",
        };

        _kedaiOnlineRepositoryMock.Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync(kedai);

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/kedaionline/{id}");
        var kedaiDto = await response.Content.ReadFromJsonAsync<KedaiDto>();

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        kedaiDto.Should().NotBeNull();
        kedaiDto.Nama.Should().Be("Test Kedai");
        kedaiDto.Description.Should().Be("A test kedai");
        kedaiDto.Category.Should().Be("Food");
        kedaiDto.HasDelivery.Should().Be("Yes");
    }


    [Fact()]
    public async Task GetAll_ForValidRequest_Return200Ok()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/kedaionline?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact()]
    public async Task GetAll_ForInvalidRequest_Return400BadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/kedaionline");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}