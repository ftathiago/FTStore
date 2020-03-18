using System;
using System.Collections.Generic;

namespace FTStore.Domain.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Register(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
