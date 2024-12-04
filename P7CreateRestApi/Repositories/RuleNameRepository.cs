using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories;

public class RuleNameRepository
{
    private readonly LocalDbContext _context;

    public RuleNameRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<RuleName?> GetRuleNameByIdAsync(int id)
    {
        return await _context.RuleNames.FindAsync(id);
    }

    public async Task CreateRuleNameAsync(RuleName ruleName)
    {
        _context.RuleNames.Add(ruleName);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateRuleNameAsync(RuleName ruleName)
    {
        var existingRuleName = await _context.RuleNames.FindAsync(ruleName.Id);

        if (existingRuleName == null)
        {
            return false;
        }

        _context.Entry(existingRuleName).CurrentValues.SetValues(ruleName);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRuleNameAsync(int id)
    {
        var ruleNameToDelete = await _context.RuleNames.FindAsync(id);

        if (ruleNameToDelete == null)
        {
            return false;
        }

        _context.RuleNames.Remove(ruleNameToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> RuleNameExistsAsync(int id)
    {
        return _context.RuleNames.AnyAsync(r => r.Id == id);
    }
}
