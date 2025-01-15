using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories;


namespace P7CreateRestApi.Tests;

public class BidListTests
{
    private readonly LocalDbContext _context;

    public BidListTests()
    {
        var options = new DbContextOptionsBuilder<LocalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new LocalDbContext(options);
    }

    [Fact]
    public async Task CreateBidList_ShouldAddNewBidList()
    {
        // Arrange
        var repository = new BidListRepository(_context);

        var bidList = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };


        // Act
        await repository.CreateBidListAsync(bidList);
        var createdBidList = await _context.BidLists.FindAsync(bidList.BidListId);

        // Assert
        Assert.NotNull(createdBidList);
        Assert.Equal(bidList.Account, createdBidList.Account);
        Assert.Equal(bidList.BidType, createdBidList.BidType);
        Assert.Equal(bidList.BidQuantity, createdBidList.BidQuantity);
    }

    [Fact]
    public async Task GetBidListById_ShouldReturnBidList()
    {
        // Arrange
        var repository = new BidListRepository(_context);

        var bidList = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };


        // Act
        await repository.CreateBidListAsync(bidList);
        var result = await repository.GetBidListByIdAsync(bidList.BidListId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bidList.Account, result.Account);
        Assert.Equal(bidList.BidType, result.BidType);
        Assert.Equal(bidList.BidQuantity, result.BidQuantity);
    }

    [Fact]  
    public async Task UpdateBidList_ShouldUpdateExistingBidList()
    {
        // Arrange
        var repository = new BidListRepository(_context);

        var bidList = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };

        // Act
        await repository.CreateBidListAsync(bidList);
        var updatedBidList = new BidList
        {
            BidListId = bidList.BidListId,
            Account = "Updated Account",
            BidType = "Updated Type",
            BidQuantity = 200
        };

        await repository.UpdateBidListAsync(updatedBidList);
        var existingBidList = await _context.BidLists.FindAsync(bidList.BidListId);

        // Assert
        Assert.NotNull(existingBidList);
        Assert.Equal(updatedBidList.Account, existingBidList.Account);
        Assert.Equal(updatedBidList.BidType, existingBidList.BidType);
        Assert.Equal(updatedBidList.BidQuantity, existingBidList.BidQuantity);
    }

    [Fact]
    public async Task DeleteBidList_ShouldDeleteExistingBidList()
    {
        // Arrange
        var repository = new BidListRepository(_context);

        var bidList = new BidList
        {
            Account = "Test Account",
            BidType = "Test Type",
            BidQuantity = 100
        };

        await repository.CreateBidListAsync(bidList);

        // Act
        await repository.DeleteBidListAsync(bidList.BidListId);
        var deletedBidList = await _context.BidLists.FindAsync(bidList.BidListId);

        // Assert
        Assert.Null(deletedBidList);
    }
}
