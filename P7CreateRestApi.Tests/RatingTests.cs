using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7CreateRestApi.Tests;

public class RatingTests
{
    [Fact]
    public async Task GetItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new Rating
        {
            MoodysRating = "Test MoodysRating",
            SandPRating = "Test SandPRating",
            FitchRating = "Test FitchRating",
            OrderNumber = 1
        };

        Mock<IGenericRepository<Rating>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(Item);

        // Act
        var controller = new RatingController(mock.Object);
        var result = await controller.GetRating(1);
        var value = (result as OkObjectResult)?.Value as Rating;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test MoodysRating", value?.MoodysRating);
    }

    [Fact]
    public async Task GetItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<Rating>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Rating?)null);

        // Act
        var controller = new RatingController(mock.Object);
        var result = await controller.GetRating(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task AddItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new Rating
        {
            MoodysRating = "Test MoodysRating",
            SandPRating = "Test SandPRating",
            FitchRating = "Test FitchRating",
            OrderNumber = 1
        };

        Mock<IGenericRepository<Rating>> mock = new();
        mock.Setup(repo => repo.CreateAsync(Item)).ReturnsAsync(true);

        // Act
        var controller = new RatingController(mock.Object);
        var result = await controller.AddRating(Item);
        var value = (result as OkObjectResult)?.Value as Rating;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test MoodysRating", value?.MoodysRating);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new Rating
        {
            MoodysRating = "Test MoodysRating",
            SandPRating = "Test SandPRating",
            FitchRating = "Test FitchRating",
            OrderNumber = 1
        };

        Mock<IGenericRepository<Rating>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new RatingController(mock.Object);
        var result = await controller.UpdateRating(1, Item);
        var value = (result as OkObjectResult)?.Value as Rating;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test MoodysRating", value?.MoodysRating);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnNotFound()
    {
        // Arrange
        var Item = new Rating
        {
            MoodysRating = "Test MoodysRating",
            SandPRating = "Test SandPRating",
            FitchRating = "Test FitchRating",
            OrderNumber = 1
        };

        Mock<IGenericRepository<Rating>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new RatingController(mock.Object);
        var result = await controller.UpdateRating(1, Item);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNoContent()
    {
        // Arrange
        Mock<IGenericRepository<Rating>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new RatingController(mock.Object);
        var result = await controller.DeleteRating(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<Rating>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new RatingController(mock.Object);
        var result = await controller.DeleteRating(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}
