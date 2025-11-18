using MappyApplication.Data;

namespace MappyApplication.Contract;

public interface IWorkoutsRepository : IGenricRepository<Workouts>
{
    Task<List<Workouts>> GetWorkoutsByUserId(string userId);
}