using MappyApplication.Contract;
using MappyApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace MappyApplication.Repository;

public class WorkoutsRepository : GenricRepository<Workouts>, IWorkoutsRepository
{
    private readonly MappyDBContext _dbContext;
    private readonly ILogger<WorkoutsRepository> _logger;

    public WorkoutsRepository(MappyDBContext dbContext, ILogger<WorkoutsRepository> logger) : base(dbContext,
        logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Workouts>> GetWorkoutsByUserId(string userId)
    {
        return await _dbContext.Workouts.Where(w => w.UserId == userId).ToListAsync();
    }
}