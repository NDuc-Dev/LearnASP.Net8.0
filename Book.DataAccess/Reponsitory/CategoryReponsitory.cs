using System.Linq.Expressions;
using Book.DataAccess.Data;
using Book.DataAccess.Reponsitory.IReponsitory;
using Book.Models;

namespace Book.DataAccess.Reponsitory
{
    public class CategoryReponsitory : Reponsitory<Category>, ICategoryReponsitory
    {
        private ApplicationDbContext _db;
        public CategoryReponsitory(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}