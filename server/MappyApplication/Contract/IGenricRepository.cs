namespace MappyApplication.Contract;

public interface IGenricRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsyncById(int id);
    Task UpdateAsync(T e);
    Task DeleteAsync(int id);
    Task<T> AddAsync(T e);
}