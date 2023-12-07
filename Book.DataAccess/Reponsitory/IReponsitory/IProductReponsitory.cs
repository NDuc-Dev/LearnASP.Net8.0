using System;
using Book.Models;


namespace Book.DataAccess.Reponsitory.IReponsitory
{
    public interface IProductReponsitory : IReponsitory<Product>
    {
        void Update(Product obj);
    }
}