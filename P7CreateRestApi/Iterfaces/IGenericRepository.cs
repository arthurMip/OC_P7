using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Iterfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<bool> CreateAsync(T model);
    Task<bool> UpdateAsync(T model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
