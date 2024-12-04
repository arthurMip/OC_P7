using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories;

public class CurvePointRepository
{
    private readonly LocalDbContext _context;

    public CurvePointRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<CurvePoint?> GetCurvePointByIdAsync(int id)
    {
        return await _context.CurvePoints.FindAsync(id);
    }

    public async Task CreateCurvePointAsync(CurvePoint curvePoint)
    {
        _context.CurvePoints.Add(curvePoint);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateCurvePointAsync(CurvePoint curvePoint)
    {
        var existingCurvePoint = await _context.CurvePoints.FindAsync(curvePoint.Id);

        if (existingCurvePoint == null)
        {
            return false;
        }

        _context.Entry(existingCurvePoint).CurrentValues.SetValues(curvePoint);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCurvePointAsync(int id)
    {
        var curvePointToDelete = await _context.CurvePoints.FindAsync(id);

        if (curvePointToDelete == null)
        {
            return false;
        }

        _context.CurvePoints.Remove(curvePointToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> CurvePointExistsAsync(int id)
    {
        return _context.CurvePoints.AnyAsync(c => c.Id == id);
    }
}