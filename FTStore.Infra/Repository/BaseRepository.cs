using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using FTStore.Domain.Repository;

using FTStore.Infra.Context;


namespace FTStore.Infra.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly FTStoreDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(FTStoreDbContext ftStoreContext)
        {
            Context = ftStoreContext;
            DbSet = Context.Set<TEntity>();
        }

        public void Register(TEntity entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            // FTStoreDbContext.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
            Context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                Context.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~BaseRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
