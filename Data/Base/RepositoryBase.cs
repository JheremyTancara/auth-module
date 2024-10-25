using Api.Repositories.Interface;

namespace Api.Repositories.Base

{
    public abstract class RepositoryBase<T, U> : IRepository<T, U>
    {
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract Task<T?> GetByIdAsync(int id);
        public abstract Task<T> CreateAsync(U entity);
        public abstract Task Update(int id, U entity);
    }
}
