using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DependencyInjectionNinject.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");
        TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null, object id=null);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        void AddRange(IEnumerable<TEntity> listOfEntity);
        void RemoveRange(IEnumerable<TEntity> listOfEntity);

    }
    public interface IUnitOfWork:IDisposable
    {
        void SaveChanges();

        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
