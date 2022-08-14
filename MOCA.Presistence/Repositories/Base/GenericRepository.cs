using Dapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Interfaces.Base;
using MOCA.Presistence.Contexts;
using System.Data;
using System.Linq.Expressions;


namespace MOCA.Presistence.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new ()
    {
        private readonly ApplicationDbContext _context;
        public DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public List<TSource> OrderBy<TSource>(List<TSource> source, string propertyName)
        {
            // LAMBDA: x => x.[PropertyName]
            var parameter = Expression.Parameter(typeof(TSource), "x");
            Expression property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            // REFLECTION: source.OrderBy(x => x.Property)
            var orderByMethod = typeof(List<TSource>).GetMethods().First(x => x.Name == "OrderBy" && x.GetParameters().Length == 2);

            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TSource), property.Type);
            var result = orderByGeneric.Invoke(null, new object[] { source, lambda });

            return (List<TSource>)result;
        }

        #region StoreProcedure
        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {

            return await _context.Connection.ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _context.Connection.ExecuteScalarAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await _context.Connection.QueryAsync<T>(sql, param, transaction, commandType: commandType)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _context.Connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        #endregion

        #region Get
        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {


            return await _context
                .Set<T>().Where(x => x.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
              .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _context
                                  .Set<T>().Where(x => x.IsDeleted == false).AsNoTracking()
                                  .ToListAsync();
            }

            catch (Exception e)
            {

            }
            return await _context
                            .Set<T>().Where(x => x.IsDeleted == false).AsNoTracking()
                            .ToListAsync();
        }
        public int GetTotalRecords()
        {
            return _context.Set<T>().Where(x => x.IsDeleted == false).Count();
        }
        public IEnumerable<T> Get(
                                Expression<Func<T, bool>> filter = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                string includeProperties = "",
                                int? take = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (take == null)
                    return orderBy(query).ToList();
                else
                    return orderBy(query).Take(take.Value).ToList();
            }
            else
            {
                if (take == null)

                    return query.ToList();
                else
                    return query.Take(take.Value).ToList();
            }
        }
        public async Task<IEnumerable<T>> GetAsync(
                                                    Expression<Func<T, bool>> filter = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    string includeProperties = "",
                                                    int? take = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (take == null)
                    return await orderBy(query).ToListAsync();
                else
                    return await orderBy(query).Take(take.Value).ToListAsync();
            }
            else
            {
                if (take == null)
                    return await query.AsQueryable().ToListAsync();
                else
                    return await query.Take(take.Value).ToListAsync();
            }
        }
        public virtual T GetEntity()
        {
            var _dbSet = dbSet.Where(x => x.IsDeleted == false).SingleOrDefault();
            return _dbSet;
        }
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> filter)
        {
            T _dbSet;
            if (filter != null)
            {
                _dbSet = await dbSet.FirstOrDefaultAsync(filter);
            }
            else
            {
                _dbSet = await dbSet.FirstOrDefaultAsync();
            }

            return _dbSet;
        }
        public virtual IEnumerable<T> GetPaged(int pageIndex, int pageCount, Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(string.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));


            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                //return orderBy(query)
                //    .Skip(pageCount * pageIndex)
                //    .Take(pageCount)
                //    .ToList();

                return orderBy(query)
                    .Skip((pageIndex - 1) * pageCount)
                    .Take(pageCount)
                    .ToList();
            }
            else
            {
                //return query.Skip(pageCount * pageIndex)
                //    .Take(pageCount).ToList();

                return query.Skip((pageIndex - 1) * pageCount)
                    .Take(pageCount).ToList();
            }
        }
        public virtual T GetByID(object id)
        {
            var _dbSet = dbSet.Find(id);
            return _dbSet;
        }
        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = dbSet.Where(x => x.IsDeleted == false);
            return query;
        }
        public async Task<IQueryable<T>> GetAllIQueryable()
        {
            IQueryable<T> query = dbSet.Where(x => x.IsDeleted == false);
            return query;
        }

        public IQueryable<T> GetAllWithRelatedEntities(string includeProperties = "")
        {
            IQueryable<T> query = dbSet.Where(x => x.IsDeleted == false);
            foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public int GetCount(Expression<Func<T, bool>> filter)
        {
            return dbSet.Count(filter);
            //return dbSet.Where(filter).Count();
        }
        public Task<int> GetCountAsync(Expression<Func<T, bool>> filter)
        {
            return dbSet.CountAsync(filter);
            //return dbSet.Where(filter).Count();
        }
        public virtual async Task<IEnumerable<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.Where(filter).ToListAsync();
        }
        #endregion

        #region Insert
        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
            //context.Entry(entity).State = EntityState.Detached;

        }
        public virtual void InsertRang(List<T> entity)
        {
            dbSet.AddRange(entity);

        }
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }
        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }

            return entities;
        }

        #endregion

        #region Update
        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
            //_context.SaveChangesAsync();
        }
        public virtual void UpdateRange(List<T> entity)
        {
            dbSet.UpdateRange(entity);

        }
        public async Task<int> UpdateRangeAsync(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public async Task<int> DeleteRangeAsync(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
        #endregion

        public decimal GetSum(Expression<Func<T, bool>> filter, Expression<Func<T, decimal>> property)
        {
            decimal? value = dbSet.Where(filter).Sum(property);
            return value ?? decimal.Zero;
        }
        public int GetSum(Expression<Func<T, bool>> filter, Expression<Func<T, int>> property)
        {
            int? value = dbSet.Where(filter).Sum(property);
            return value ?? 0;
        }
        
    }
}

