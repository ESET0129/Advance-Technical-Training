namespace CollegeApp.Data.Repository
{
    public interface ICollegeRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> GetByName(string name);
        Task AddAsync(T entity);    
        Task UpdateAsync(int id,T entity);

        Task DeleteAsync(int id);

    }
}
