using DependencyInjection.Controllers;
using DependencyInjection.Entities;
using DependencyInjection.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class ItemsControllerTests
{
    [Fact]
    public async Task GetAllAsync_WithNoItems_ReturnsEmptyList()
    {
        // Arrange
        var repositoryMock = new Mock<IItemsRepository>();
        repositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Item>());

        var controller = new ItemsController(repositoryMock.Object);

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_WithItems_ReturnsAllItems()
    {
        // Arrange
        var items = new List<Item>
        {
            new Item { Id = Guid.NewGuid(), Name = "Wilson Pro Staff", Price = 250, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Babolat Pure Aero", Price = 230, CreatedDate = DateTimeOffset.UtcNow }  
        };

        var repositoryMock = new Mock<IItemsRepository>();
        repositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(items);

        var controller = new ItemsController(repositoryMock.Object);

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByIdAsync_WithUnexistingItem_ReturnsNotFound()
    {
        // Arrange
        var item = new Item { Id = Guid.NewGuid(), Name = "Tennis Balls Can"};

        var repositoryMock = new Mock<IItemsRepository>();
        repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);

        var controller = new ItemsController(repositoryMock.Object);

        // Act
        var result = await controller.GetByIdAsync(item.Id);

        result.Result.Should().BeOfType<NotFoundResult>();
        
    }
}