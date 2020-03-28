using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using FTStore.Domain.Repository;

using FTStore.Infra.Context;
using AutoMapper;

namespace FTStore.Infra.Repository
{
    public abstract class BaseRepository<TEntity, TDTO> : IBaseRepository<TEntity>
        where TEntity : class
        where TDTO : class
    {
        protected readonly FTStoreDbContext Context;
        protected readonly DbSet<TDTO> DbSet;
        protected readonly IMapper _mapper;

        public BaseRepository(FTStoreDbContext ftStoreContext, IMapper mapper)
        {
            Context = ftStoreContext;
            DbSet = Context.Set<TDTO>();
            _mapper = mapper;
        }

        public void Register(TEntity entity)
        {
            var data = _mapper.Map<TDTO>(entity);
            data = BeforePost(data, EntityState.Added);
            DbSet.Add(data);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var data = _mapper.Map<TDTO>(entity);
            Context.Entry(data).State = EntityState.Modified;
            data = BeforePost(data, EntityState.Modified);
            DbSet.Update(data);
            Context.SaveChanges();
            Context.Entry(data).State = EntityState.Detached;
        }

        public void Remove(TEntity entity)
        {
            var data = _mapper.Map<TDTO>(entity);
            DbSet.Remove(data);
            Context.SaveChanges();
        }

        protected virtual TDTO BeforePost(TDTO model, EntityState state)
        {
            return model;
        }

        public TEntity GetById(int id)
        {
            var data = DbSet.Find(id);
            if (data == null)
                return null;
            Context.Entry(data).State = EntityState.Detached;
            var entity = _mapper.Map<TEntity>(data);
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().Select(dto => _mapper.Map<TEntity>(dto)).ToList();
        }

        protected void Unchange<T>(T model)
        {
            if (model == null)
                return;
            Context.Entry(model).State = EntityState.Unchanged;
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
