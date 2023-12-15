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
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);

            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Author = obj.Author;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Description = obj.Description;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}