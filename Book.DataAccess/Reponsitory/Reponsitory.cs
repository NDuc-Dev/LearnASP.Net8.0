using System.Linq.Expressions;
using Book.DataAccess.Data;
using Book.DataAccess.Reponsitory.IReponsitory;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Reponsitory
{
    public class Reponsitory<T> : IReponsitory<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Reponsitory (ApplicationDbContext db){
            _db = db;
            this.dbSet = _db.Set<T>();
            // _db.Categories == dbSet
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }

}

