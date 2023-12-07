namespace Book.DataAccess.Reponsitory.IReponsitory
{
    public interface IUnitOfWork
    {
        ICategoryReponsitory Category { get; }
        IProductReponsitory Product { get; }

        void Save();
    }
}