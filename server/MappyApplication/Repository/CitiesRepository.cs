using MappyApplication.Contract;
using MappyApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace MappyApplication.Repository;

public class CitiesRepository : GenricRepository<Cities>, ICitiesRepository
{
    private readonly MappyDBContext _dbContext;
    private readonly ILogger<CitiesRepository> _logger;

    public CitiesRepository(MappyDBContext dbContext, ILogger<CitiesRepository> logger) : base(dbContext,
        logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Cities>> GetCitiesByUserId(string userId)
    {
        return await _dbContext.Cities.Where(c => c.UserId.Equals(userId)).ToListAsync();
    }
}