using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FTStore.Domain.Repository;
using FTStore.Infra.Context;

namespace FTStore.Infra.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly FTStoreDbContext FTStoreDbContext;

        public BaseRepository(FTStoreDbContext FTStoreContexto)
        {
            FTStoreDbContext = FTStoreContexto;
        }

        public void Adicionar(TEntity entity)
        {
            FTStoreDbContext.Set<TEntity>().Add(entity);
            FTStoreDbContext.SaveChanges();
        }

        public void Atualizar(TEntity entity)
        {
            // FTStoreDbContext.Entry(entity).State = EntityState.Modified;
            FTStoreDbContext.Set<TEntity>().Update(entity);
            FTStoreDbContext.SaveChanges();
        }

        public TEntity ObterPorId(int id)
        {
            return FTStoreDbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return FTStoreDbContext.Set<TEntity>().ToList();
        }

        public void Remover(TEntity entity)
        {
            FTStoreDbContext.Set<TEntity>().Remove(entity);
            FTStoreDbContext.SaveChanges();
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

                FTStoreDbContext.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BaseRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}