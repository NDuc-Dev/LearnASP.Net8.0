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
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        //Category, convertType
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in includeProperties
                    .Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
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

