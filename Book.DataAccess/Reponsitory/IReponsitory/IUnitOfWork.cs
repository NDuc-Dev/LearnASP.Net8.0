namespace Book.DataAccess.Reponsitory.IReponsitory
{
    public interface IUnitOfWork
    {
        ICategoryReponsitory Category { get; }

        void Save();
    }
}