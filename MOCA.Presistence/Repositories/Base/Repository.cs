using Microsoft.EntityFrameworkCore;
using MOCA.Core.Interfaces.Base;
using MOCA.Presistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? take = null)
        {
            IQueryable<TEntity> query = dbSet;

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

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    string includeProperties = "",
    int? take = null)
        {
            IQueryable<TEntity> query = dbSet;

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

        public virtual TEntity GetEntity()
        {
            var _dbSet = dbSet.SingleOrDefault();
            return _dbSet;
        }

        public async Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filter)
        {
            TEntity _dbSet;
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

        public virtual IEnumerable<TEntity> GetPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));


            IQueryable<TEntity> query = dbSet;

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

        public virtual TEntity GetByID(object id)
        {
            var _dbSet = dbSet.Find(id);
            return _dbSet;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            //context.Entry(entity).State = EntityState.Detached;

        }

        public virtual void InsertRang(List<TEntity> entity)
        {
            dbSet.AddRange(entity);

        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);

        }

        public virtual void UpdateRange(List<TEntity> entity)
        {
            dbSet.UpdateRange(entity);

        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }

        public IQueryable<TEntity> GetAllWithRelatedEntities(string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Count(filter);
            //return dbSet.Where(filter).Count();
        }
        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.CountAsync(filter);
            //return dbSet.Where(filter).Count();
        }



        public decimal GetSum(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, decimal>> property)
        {
            decimal? value = dbSet.Where(filter).Sum(property);
            return value ?? decimal.Zero;
        }
        public int GetSum(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, int>> property)
        {
            int? value = dbSet.Where(filter).Sum(property);
            return value ?? 0;
        }





        public virtual async Task<IQueryable<TEntity>> GetWithFilterAsync(Expression<Func<TEntity, bool>> filter)
        {

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }


    }
}
