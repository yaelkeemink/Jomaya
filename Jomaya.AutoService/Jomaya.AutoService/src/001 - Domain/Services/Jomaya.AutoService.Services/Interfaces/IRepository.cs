using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Jomaya.AutoService.Services.Interfaces
{
    public interface IRepository<TEntity, TKey>
        : IDisposable
    {
        IEnumerable<TEntity> FindAll();

        TEntity Find(TKey id);

        int Insert(TEntity item);

        int Update(TEntity item);

        int Count();
    }
}
