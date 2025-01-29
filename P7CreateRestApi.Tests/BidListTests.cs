using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Iterfaces;

namespace P7CreateRestApi.Tests;

public class BidListTests
{
    [Fact]
    public async Task GetItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };

        Mock<IGenericRepository<BidList>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(Item);

        // Act
        var controller = new BidListController(mock.Object);
        var result = await controller.GetBidList(1);
        var value = (result as OkObjectResult)?.Value as BidList;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Account", value?.Account);
    }

    [Fact]
    public async Task GetItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<BidList>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((BidList?)null);

        // Act
        var controller = new BidListController(mock.Object);
        var result = await controller.GetBidList(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task AddItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };

        Mock<IGenericRepository<BidList>> mock = new();
        mock.Setup(repo => repo.CreateAsync(Item)).ReturnsAsync(true);

        // Act
        var controller = new BidListController(mock.Object);
        var result = await controller.AddBidList(Item);
        var value = (result as OkObjectResult)?.Value as BidList;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Account", value?.Account);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };

        Mock<IGenericRepository<BidList>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new BidListController(mock.Object);
        var result = await controller.UpdateBidList(1, Item);
        var value = (result as OkObjectResult)?.Value as BidList;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Account", value?.Account);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnNotFound()
    {
        // Arrange
        var Item = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };

        Mock<IGenericRepository<BidList>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new BidListController(mock.Object);
        var result = await controller.UpdateBidList(1, Item);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNoContent()
    {
        // Arrange
        Mock<IGenericRepository<BidList>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new BidListController(mock.Object);
        var result = await controller.DeleteBidList(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<BidList>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new BidListController(mock.Object);
        var result = await controller.DeleteBidList(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}
