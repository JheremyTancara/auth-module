namespace Api.Repositories.Interface

{
    public interface IRepository<T, U>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(U entity);
        Task Update(int id, U entity);
    }
}

