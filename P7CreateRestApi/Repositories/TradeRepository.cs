using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories;

public class TradeRepository
{
    private readonly LocalDbContext _context;

    public TradeRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<Trade?> GetTradeByIdAsync(int tradeId)
    {
        return await _context.Trades.FindAsync(tradeId);
    }

    public async Task CreateTradeAsync(Trade trade)
    {
        _context.Trades.Add(trade);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateTradeAsync(Trade trade)
    {
        var existingTrade = await _context.Trades.FindAsync(trade.TradeId);

        if (existingTrade == null)
        {
            return false;
        }

        _context.Entry(existingTrade).CurrentValues.SetValues(trade);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTradeAsync(int tradeId)
    {
        var tradeToDelete = await _context.Trades.FindAsync(tradeId);

        if (tradeToDelete == null)
        {
            return false;
        }

        _context.Trades.Remove(tradeToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> TradeExistsAsync(int tradeId)
    {
        return _context.Trades.AnyAsync(t => t.TradeId == tradeId);
    }
}