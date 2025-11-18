using MappyApplication.Data;

namespace MappyApplication.Contract;

public interface ICitiesRepository : IGenricRepository<Cities>
{
    Task<List<Cities>> GetCitiesByUserId(string userId);
}