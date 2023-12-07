using Book.DataAccess.Data;
using Book.DataAccess.Reponsitory.IReponsitory;

namespace Book.DataAccess.Reponsitory
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _db;
        public ICategoryReponsitory Category { get; private set; }
        public IProductReponsitory Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryReponsitory(_db);
            Product = new ProductReponsitory(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}