namespace EnterpriseManagement.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync<Tid>(Tid id) where Tid : notnull;
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
