using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class DataRepository<TContext, TEntity> : IDataRepository<TEntity>
        where TContext : IUnitOfWork
        where TEntity : class
    {
        protected TContext _context;
        /// <summary>
        /// Constructor that takes a context
        /// </summary>
        /// <param name="context">An established data context</param>
        public DataRepository(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Select()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Single(predicate);
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().First(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("Cannot add a null entity");

            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("Cannot delete a null entity");

            _context.Set<TEntity>().Remove(entity);
        }

        public void Attach(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("Cannot attach a null entity");

            _context.Set<TEntity>().Attach(entity);
        }

        #region IDisposable implementation
        private bool disposedValue;

        public void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state here if required
                }
                // dispose unmanaged objects and set large fields to null
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
