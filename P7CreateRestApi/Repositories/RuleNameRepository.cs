using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Iterfaces;

namespace P7CreateRestApi.Repositories;

public class RuleNameRepository : IGenericRepository<RuleName>
{
    private readonly LocalDbContext _context;

    public RuleNameRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<RuleName?> GetByIdAsync(int id)
    {
        return await _context.RuleNames.FindAsync(id);
    }

    public async Task<bool> CreateAsync(RuleName model)
    {
        _context.RuleNames.Add(model);
       return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(RuleName model)
    {
        var existingRuleName = await _context.RuleNames.FindAsync(model.Id);

        if (existingRuleName == null)
        {
            return false;
        }

        _context.Entry(existingRuleName).CurrentValues.SetValues(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ruleNameToDelete = await _context.RuleNames.FindAsync(id);

        if (ruleNameToDelete == null)
        {
            return false;
        }

        _context.RuleNames.Remove(ruleNameToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.RuleNames.AnyAsync(r => r.Id == id);
    }
}
