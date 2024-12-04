using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories;

public class RatingRepository
{
    private readonly LocalDbContext _context;

    public RatingRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<Rating?> GetRatingByIdAsync(int id)
    {
        return await _context.Ratings.FindAsync(id);
    }

    public async Task CreateRatingAsync(Rating rating)
    {
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateRatingAsync(Rating rating)
    {
        var existingRating = await _context.Ratings.FindAsync(rating.Id);

        if (existingRating == null)
        {
            return false;
        }

        _context.Entry(existingRating).CurrentValues.SetValues(rating);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRatingAsync(int id)
    {
        var ratingToDelete = await _context.Ratings.FindAsync(id);

        if (ratingToDelete == null)
        {
            return false;
        }

        _context.Ratings.Remove(ratingToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> RatingExistsAsync(int id)
    {
        return _context.Ratings.AnyAsync(r => r.Id == id);
    }
}
