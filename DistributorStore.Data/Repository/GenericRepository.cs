using DistributorStore.Data.ApplicationDbContext;
using DistributorStore.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DistributorStoreDbContext dbContext;
        public GenericRepository(DistributorStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public List<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public TEntity GetById(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            return entity;
        }
       
        public void Insert(TEntity entity)
        {
           
            dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return dbContext.Set<TEntity>().Where(expression).AsQueryable();
        }

        public TEntity GetByIdWithInclude(int id, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (currenct, inc) => currenct.Include(inc));
            return query.FirstOrDefault();
        }

        public List<TEntity> GetAllWithInclude(params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (currenct, inc) => currenct.Include(inc));
            return query.ToList();
        }

        public IEnumerable<TEntity> WhereWithInclude(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query.Where(expression);
            query = includes.Aggregate(query, (currenct, inc) => currenct.Include(inc));
            return query.ToList();
        }
    }
}
