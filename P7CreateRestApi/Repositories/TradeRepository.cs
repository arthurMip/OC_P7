using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Iterfaces;

namespace P7CreateRestApi.Repositories;

public class TradeRepository : IGenericRepository<Trade>
{
    private readonly LocalDbContext _context;

    public TradeRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<Trade?> GetByIdAsync(int id)
    {
        return await _context.Trades.FindAsync(id);
    }

    public async Task<bool> CreateAsync(Trade model)
    {
        _context.Trades.Add(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Trade model)
    {
        var existingTrade = await _context.Trades.FindAsync(model.TradeId);

        if (existingTrade == null)
        {
            return false;
        }

        _context.Entry(existingTrade).CurrentValues.SetValues(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tradeToDelete = await _context.Trades.FindAsync(id);

        if (tradeToDelete == null)
        {
            return false;
        }

        _context.Trades.Remove(tradeToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Trades.AnyAsync(t => t.TradeId == id);
    }
}