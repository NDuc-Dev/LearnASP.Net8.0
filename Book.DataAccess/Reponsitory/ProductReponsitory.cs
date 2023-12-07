using System.Linq.Expressions;
using Book.DataAccess.Data;
using Book.DataAccess.Reponsitory.IReponsitory;
using Book.Models;

namespace Book.DataAccess.Reponsitory
{
    public class ProductReponsitory : Reponsitory<Product>, IProductReponsitory
    {
        private ApplicationDbContext _db;
        public ProductReponsitory(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}