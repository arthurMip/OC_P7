using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Iterfaces;

namespace P7CreateRestApi.Repositories;

public class CurvePointRepository : IGenericRepository<CurvePoint>
{
    private readonly LocalDbContext _context;

    public CurvePointRepository(LocalDbContext context)
    {
        _context = context;
    }
    public async Task<CurvePoint?> GetByIdAsync(int id)
    {
        return await _context.CurvePoints.FindAsync(id);
    }

    public async Task<bool> CreateAsync(CurvePoint model)
    {
        _context.CurvePoints.Add(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(CurvePoint model)
    {
        var existingCurvePoint = await _context.CurvePoints.FindAsync(model.Id);

        if (existingCurvePoint == null)
        {
            return false;
        }

        _context.Entry(existingCurvePoint).CurrentValues.SetValues(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var curvePointToDelete = await _context.CurvePoints.FindAsync(id);

        if (curvePointToDelete == null)
        {
            return false;
        }

        _context.CurvePoints.Remove(curvePointToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.CurvePoints.AnyAsync(c => c.Id == id);
    }
}