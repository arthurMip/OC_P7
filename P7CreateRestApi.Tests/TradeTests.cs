using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Iterfaces;

namespace P7CreateRestApi.Tests;

public class TradeTests
{
    [Fact]
    public async Task GetItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new Trade
        {
            Account = "Test Account",
            AccountType = "Test AccountType",
        };

        Mock<IGenericRepository<Trade>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(Item);

        // Act
        var controller = new TradeController(mock.Object);
        var result = await controller.GetTrade(1);
        var value = (result as OkObjectResult)?.Value as Trade;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Account", value?.Account);
    }

    [Fact]
    public async Task GetItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<Trade>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Trade?)null);

        // Act
        var controller = new TradeController(mock.Object);
        var result = await controller.GetTrade(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task AddItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new Trade
        {
            Account = "Test Account",
            AccountType = "Test AccountType",
        };

        Mock<IGenericRepository<Trade>> mock = new();
        mock.Setup(repo => repo.CreateAsync(Item)).ReturnsAsync(true);

        // Act
        var controller = new TradeController(mock.Object);
        var result = await controller.AddTrade(Item);
        var value = (result as OkObjectResult)?.Value as Trade;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Account", value?.Account);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new Trade
        {
            Account = "Test Account",
            AccountType = "Test AccountType",
        };

        Mock<IGenericRepository<Trade>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new TradeController(mock.Object);
        var result = await controller.UpdateTrade(1, Item);
        var value = (result as OkObjectResult)?.Value as Trade;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Account", value?.Account);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnNotFound()
    {
        // Arrange
        var Item = new Trade
        {
            Account = "Test Account",
            AccountType = "Test AccountType",
        };

        Mock<IGenericRepository<Trade>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new TradeController(mock.Object);
        var result = await controller.UpdateTrade(1, Item);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNoContent()
    {
        // Arrange
        Mock<IGenericRepository<Trade>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new TradeController(mock.Object);
        var result = await controller.DeleteTrade(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<Trade>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new TradeController(mock.Object);
        var result = await controller.DeleteTrade(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}
