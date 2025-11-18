using MappyApplication.Contract;
using MappyApplication.Data;
using MappyApplication.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MappyApplication.Repository;

public class GenricRepository<T> : IGenricRepository<T> where T : class
{
    private readonly MappyDBContext _dbContext;
    private readonly ILogger<GenricRepository<T>> _logger;

    public GenricRepository(MappyDBContext dbContext, ILogger<GenricRepository<T>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetAsyncById(int id)
    {
        var e = await _dbContext.Set<T>().FindAsync(id);
        if (e == null)
        {
            _logger.LogError($"we cannot found {typeof(T).Name} {id}");
            throw new AppErrorResponse($"we cannot found {typeof(T).Name} {id}");
        }

        return e;
    }

    public async Task UpdateAsync(T e)
    {
        _dbContext.Set<T>().Update(e);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Updated {typeof(T).Name} {e}");
    }

    public async Task DeleteAsync(int id)
    {
        var e = await GetAsyncById(id);
        _dbContext.Set<T>().Remove(e);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Deleted {typeof(T).Name} {e}");
    }

    public async Task<T> AddAsync(T e)
    {
        var entity = await _dbContext.Set<T>().AddAsync(e);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Added {typeof(T).Name} {e}");
        return entity.Entity;
    }
}