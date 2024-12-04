using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories;

public class BidListRepository
{
    private readonly LocalDbContext _context;

    public BidListRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<BidList?> GetBidListByIdAsync(int id)
    {
        return await _context.BidLists.FindAsync(id);
    }

    public async Task CreateBidListAsync(BidList bidList)
    {
        _context.BidLists.Add(bidList);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateBidListAsync(BidList bidList)
    {
        var existingBidList = await _context.BidLists.FindAsync(bidList.BidListId);

        if (existingBidList == null)
        {
            return false;
        }

        _context.Entry(existingBidList).CurrentValues.SetValues(bidList);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBidListAsync(int id)
    {
        var bidListToDelete = await _context.BidLists.FindAsync(id);

        if (bidListToDelete == null)
        {
            return false;
        }

        _context.BidLists.Remove(bidListToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> BidListExistsAsync(int id)
    {
        return _context.BidLists.AnyAsync(b => b.BidListId == id);
    }
}
