using System.Data;
using System.Linq.Expressions;


namespace MOCA.Core.Interfaces.Base
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> GetByIdAsync(long id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entities);
        Task<int> DeleteRangeAsync(List<T> entities);
        Task<int> UpdateRangeAsync(List<T> entities);
        List<TSource> OrderBy<TSource>(List<TSource> source, string propertyName);
        int GetTotalRecords();

        ///////////////////////////

        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default);

        ////////////////////////////////

        Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default);


        /////////////////////////////

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                          string includeProperties = "",
                          int? take = null);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                            string includeProperties = "",
                                            int? take = null);

        T GetEntity();

        IEnumerable<T> GetPaged(int pageIndex, int pageCount, Expression<Func<T, bool>> filter = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                      string includeProperties = "");

        IQueryable<T> GetAll();

        IQueryable<T> GetAllWithRelatedEntities(string includeProperties = "");


        T GetByID(object id);

        Task<T> GetByIdAsync(object id);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> filter);

        void Insert(T entity);
        void InsertRang(List<T> entity);
        void Update(T entity);
        void UpdateRange(List<T> entity);

        void Delete(object id);

        void Delete(T entityToDelete);

        int GetCount(Expression<Func<T, bool>> filter);

        decimal GetSum(Expression<Func<T, bool>> filter, Expression<Func<T, decimal>> property);
        int GetSum(Expression<Func<T, bool>> filter, Expression<Func<T, int>> property);
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter);


        Task<IEnumerable<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter);


    }
}
