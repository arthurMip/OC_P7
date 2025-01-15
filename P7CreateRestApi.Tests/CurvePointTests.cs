using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Tests;

public class CurvePointTests
{
    private readonly LocalDbContext _context;

    public CurvePointTests()
    {
        var options = new DbContextOptionsBuilder<LocalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new LocalDbContext(options);
    }

    [Fact]
    public async Task CreateCurvePoint_ShouldAddNewCurvePoint()
    {
        // Arrange
        var repository = new CurvePointRepository(_context);
        var curvePoint = new CurvePoint
        {
            CurveId = 1,
            Term = 2,
            CurvePointValue = 3,
        };

        // Act
        await repository.CreateCurvePointAsync(curvePoint);
        var createdCurvePoint = await _context.CurvePoints.FindAsync(curvePoint.Id);

        // Assert
        Assert.NotNull(createdCurvePoint);
        Assert.Equal(curvePoint.CurveId, createdCurvePoint.CurveId);
        Assert.Equal(curvePoint.Term, createdCurvePoint.Term);
        Assert.Equal(curvePoint.CurvePointValue, createdCurvePoint.CurvePointValue);
    }

    [Fact]
    public async Task GetCurvePointById_ShouldReturnCurvePoint()
    {
        // Arrange
        var repository = new CurvePointRepository(_context);

        var curvePoint = new CurvePoint
        {
            CurveId = 1,
            Term = 2,
            CurvePointValue = 3,
        };

        // Act
        await repository.CreateCurvePointAsync(curvePoint);
        var result = await repository.GetCurvePointByIdAsync(curvePoint.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(curvePoint.CurveId, result.CurveId);
        Assert.Equal(curvePoint.Term, result.Term);
        Assert.Equal(curvePoint.CurvePointValue, result.CurvePointValue);
    }

    [Fact]
    public async Task UpdateCurvePoint_ShouldUpdateExistingCurvePoint()
    {
        // Arrange
        var repository = new CurvePointRepository(_context);

        var curvePoint = new CurvePoint
        {
            CurveId = 1,
            Term = 2,
            CurvePointValue = 3,
        };

        // Act
        await repository.CreateCurvePointAsync(curvePoint);
        var updatedCurvePoint = new CurvePoint
        {
            Id = curvePoint.Id,
            CurveId = 4,
            Term = 5,
            CurvePointValue = 6,
        };

        await repository.UpdateCurvePointAsync(updatedCurvePoint);
        var result = await repository.GetCurvePointByIdAsync(curvePoint.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedCurvePoint.CurveId, result.CurveId);
        Assert.Equal(updatedCurvePoint.Term, result.Term);
        Assert.Equal(updatedCurvePoint.CurvePointValue, result.CurvePointValue);
    }

    [Fact]
    public async Task DeleteCurvePoint_ShouldDeleteExistingCurvePoint()
    {
        // Arrange
        var repository = new CurvePointRepository(_context);
        var curvePoint = new CurvePoint
        {
            CurveId = 1,
            Term = 2,
            CurvePointValue = 3,
        };

        // Act
        await repository.CreateCurvePointAsync(curvePoint);
        await repository.DeleteCurvePointAsync(curvePoint.Id);
        var result = await repository.GetCurvePointByIdAsync(curvePoint.Id);

        // Assert
        Assert.Null(result);
    }
}
