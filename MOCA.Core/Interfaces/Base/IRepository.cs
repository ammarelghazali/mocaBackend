using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 string includeProperties = "",
                                 int? take = null);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            string includeProperties = "",
                                            int? take = null);

        TEntity GetEntity();

        IEnumerable<TEntity> GetPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> filter = null,
                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                      string includeProperties = "");

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllWithRelatedEntities(string includeProperties = "");


        TEntity GetByID(object id);

        Task<TEntity> GetByIdAsync(object id);

        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filter);

        void Insert(TEntity entity);

        void InsertRang(List<TEntity> entity);

        void Update(TEntity entity);

        void UpdateRange(List<TEntity> entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void DeleteRange(IList<TEntity> entities);

        int GetCount(Expression<Func<TEntity, bool>> filter);

        decimal GetSum(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, decimal>> property);

        int GetSum(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, int>> property);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter);

        Task<IQueryable<TEntity>> GetWithFilterAsync(Expression<Func<TEntity, bool>> filter);
    }
}
