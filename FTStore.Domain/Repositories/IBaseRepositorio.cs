using System;
using System.Collections.Generic;

namespace FTStore.Domain.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Adicionar(TEntity entity);
        TEntity ObterPorId(int id);
        IEnumerable<TEntity> ObterTodos();
        void Atualizar(TEntity entity);
        void Remover(TEntity entity);
    }
}