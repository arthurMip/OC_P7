using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Iterfaces;

namespace P7CreateRestApi.Repositories;

public class RatingRepository : IGenericRepository<Rating>
{
    private readonly LocalDbContext _context;

    public RatingRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<Rating?> GetByIdAsync(int id)
    {
        return await _context.Ratings.FindAsync(id);
    }

    public async Task<bool> CreateAsync(Rating model)
    {
        _context.Ratings.Add(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Rating model)
    {
        var existingRating = await _context.Ratings.FindAsync(model.Id);

        if (existingRating == null)
        {
            return false;
        }

        _context.Entry(existingRating).CurrentValues.SetValues(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ratingToDelete = await _context.Ratings.FindAsync(id);

        if (ratingToDelete == null)
        {
            return false;
        }

        _context.Ratings.Remove(ratingToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Ratings.AnyAsync(r => r.Id == id);
    }
}
