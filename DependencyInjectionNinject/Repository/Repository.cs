using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjectionNinject.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DependencyInjectionNinject.Repository
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
    {
        //internal SchoolContext context;
        //internal DbSet<TEntity> dbSet;
        private readonly MovieDBContext _db;
        internal IDbSet<TEntity> dbSet;

        public Repository(MovieDBContext db)
        {
            _db = db;
            dbSet = db.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
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
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }
        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null, object id=null)
        {
            
            if (id!=null)
            {
                return dbSet.Find(id);
            }
            else if (filter != null)
            {
                return _db.Set<TEntity>().Where(filter).FirstOrDefault();
            }
            return null;
            
        }
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public void Delete(TEntity entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _db.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void AddRange(IEnumerable<TEntity> listOfEntity)
        {
            _db.Set<TEntity>().AddRange(listOfEntity);
        }

        public void RemoveRange(IEnumerable<TEntity> listOfEntity)
        {
            _db.Set<TEntity>().RemoveRange(listOfEntity);
        }
    }
}