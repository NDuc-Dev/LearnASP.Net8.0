using System;
using Book.Models;


namespace Book.DataAccess.Reponsitory.IReponsitory
{
    public interface ICategoryReponsitory : IReponsitory<Category>
    {
        void Update(Category obj);
        void Save();
    }
}