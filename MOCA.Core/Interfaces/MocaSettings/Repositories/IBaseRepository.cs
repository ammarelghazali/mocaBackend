namespace MOCA.Core.Interfaces.MocaSettings.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);

        Task<T> AddAsync(T entity);

        T Update(T entity);

        void UpdateRange(IList<T> entities);

        void Delete(long id);
    }
}
