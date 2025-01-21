using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Iterfaces;

namespace P7CreateRestApi.Repositories;

public class BidListRepository : IGenericRepository<BidList>
{
    private readonly LocalDbContext _context;

    public BidListRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<BidList?> GetByIdAsync(int id)
    {
        return await _context.BidLists.FindAsync(id);
    }

    public async Task<bool> CreateAsync(BidList model)
    {
        _context.BidLists.Add(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(BidList model)
    {
        var existingBidList = await _context.BidLists.FindAsync(model.BidListId);

        if (existingBidList == null)
        {
            return false;
        }

        _context.Entry(existingBidList).CurrentValues.SetValues(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var bidListToDelete = await _context.BidLists.FindAsync(id);

        if (bidListToDelete == null)
        {
            return false;
        }

        _context.BidLists.Remove(bidListToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.BidLists.AnyAsync(b => b.BidListId == id);
    }
}
