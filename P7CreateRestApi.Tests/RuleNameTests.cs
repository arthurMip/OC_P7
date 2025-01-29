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

public class RuleNameTests
{
    [Fact]
    public async Task GetItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new RuleName
        {
            Name = "Test Name",
            Description = "Test Description"
        };

        Mock<IGenericRepository<RuleName>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(Item);

        // Act
        var controller = new RuleNameController(mock.Object);
        var result = await controller.GetRuleName(1);
        var value = (result as OkObjectResult)?.Value as RuleName;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Name", value?.Name);
    }

    [Fact]
    public async Task GetItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<RuleName>> mock = new();
        mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((RuleName?)null);

        // Act
        var controller = new RuleNameController(mock.Object);
        var result = await controller.GetRuleName(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task AddItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new RuleName
        {
            Name = "Test Name",
            Description = "Test Description"
        };

        Mock<IGenericRepository<RuleName>> mock = new();
        mock.Setup(repo => repo.CreateAsync(Item)).ReturnsAsync(true);

        // Act
        var controller = new RuleNameController(mock.Object);
        var result = await controller.AddRuleName(Item);
        var value = (result as OkObjectResult)?.Value as RuleName;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Name", value?.Name);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnItem()
    {
        // Arrange
        var Item = new RuleName
        {
            Name = "Test Name",
            Description = "Test Description"
        };

        Mock<IGenericRepository<RuleName>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new RuleNameController(mock.Object);
        var result = await controller.UpdateRuleName(1, Item);
        var value = (result as OkObjectResult)?.Value as RuleName;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Test Name", value?.Name);
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnNotFound()
    {
        // Arrange
        var Item = new RuleName
        {
            Name = "Test Name",
            Description = "Test Description"
        };

        Mock<IGenericRepository<RuleName>> mock = new();
        mock.Setup(repo => repo.UpdateAsync(Item)).ReturnsAsync(true);
        mock.Setup(repo => repo.ExistsAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new RuleNameController(mock.Object);
        var result = await controller.UpdateRuleName(1, Item);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNoContent()
    {
        // Arrange
        Mock<IGenericRepository<RuleName>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var controller = new RuleNameController(mock.Object);
        var result = await controller.DeleteRuleName(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNotFound()
    {
        // Arrange
        Mock<IGenericRepository<RuleName>> mock = new();
        mock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(false);

        // Act
        var controller = new RuleNameController(mock.Object);
        var result = await controller.DeleteRuleName(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}
