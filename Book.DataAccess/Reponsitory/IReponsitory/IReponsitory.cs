using System;
using System.Linq;
using System.Linq.Expressions;

namespace Book.DataAccess.Reponsitory.IReponsitory
{
    public interface IReponsitory<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }

}

