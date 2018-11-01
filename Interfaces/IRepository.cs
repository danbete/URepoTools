using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DBTools
{
    internal interface IRepository<T> where T : EntityBase
    {
        T GetByPK<P>(P PrimeryKey);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
       // IEnumerable<T> Paging();
    }
}
