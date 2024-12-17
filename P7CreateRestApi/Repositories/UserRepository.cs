using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories;

public class UserRepository
{
    private readonly LocalDbContext _context;

    public UserRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);

        if (existingUser == null)
        {
            return false;
        }

        _context.Entry(existingUser).CurrentValues.SetValues(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var userToDelete = await _context.Users.FindAsync(userId);

        if (userToDelete == null)
        {
            return false;
        }

        _context.Users.Remove(userToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> UserExistsAsync(int userId)
    {
        return _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<bool> ContainAsync(string username, string password)
    {
        return await _context.Users.AnyAsync(u => u.Username == username && u.Password == password);
    }
}