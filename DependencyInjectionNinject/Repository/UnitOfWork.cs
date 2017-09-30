using DependencyInjectionNinject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionNinject.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private MovieDBContext _db;

        public UnitOfWork(MovieDBContext db)
        {
            _db = db;
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IRepository<TEntity>;
            }
            IRepository<TEntity> repo = new Repository<TEntity>(_db);
            repositories.Add(typeof(TEntity), repo);
            return repo;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}