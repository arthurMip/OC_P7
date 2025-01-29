using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Iterfaces;


namespace P7CreateRestApi.Tests;

public class CurvePointTests
{
    [Fact]
    public async Task GetItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new CurvePoint
        {
            Term = 1,
            CurveId = 1
        };

        Mock<IGenericRepository<CurvePoint>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(Item);

        // Act
        var controller = new CurvePointController(mock.Object);
        var result = await controller.GetCurvePoint(1);
        var value = (result as OkObjectResult)?.Value as CurvePoint;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, value?.Term);
    }

    [Fact]
    public async Task GetItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<CurvePoint>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((CurvePoint?)null);

        // Act
        var controller = new CurvePointController(mock.Object);
        var result = await controller.GetCurvePoint(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task AddItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new CurvePoint
        {
            Term = 1,
            CurveId = 1
        };

        Mock<IGenericRepository<CurvePoint>> mock = new();
        mock.Setup(repo => repo.CreateAsync(Item)).ReturnsAsync(true);

        // Act
        var controller = new CurvePointController(mock.Object);
        var result = await controller.AddCurvePoint(Item);
        var value = (result as OkObjectResult)?.Value as CurvePoint;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, value?.Term);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new CurvePoint
        {
            Term = 1,
            CurveId = 1
        };

        Mock<IGenericRepository<CurvePoint>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new CurvePointController(mock.Object);
        var result = await controller.UpdateCurvePoint(1, Item);
        var value = (result as OkObjectResult)?.Value as CurvePoint;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, value?.Term);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnNotFound()
    {
        // Arrange
        var Item = new CurvePoint
        {
            Term = 1,
            CurveId = 1
        };

        Mock<IGenericRepository<CurvePoint>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new CurvePointController(mock.Object);
        var result = await controller.UpdateCurvePoint(1, Item);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNoContent()
    {
        // Arrange
        Mock<IGenericRepository<CurvePoint>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new CurvePointController(mock.Object);
        var result = await controller.DeleteCurvePoint(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<CurvePoint>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new CurvePointController(mock.Object);
        var result = await controller.DeleteCurvePoint(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}
