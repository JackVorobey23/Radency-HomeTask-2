using HomeTask2.DataModel;
namespace HomeTask2.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindById(int id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Remove(TEntity entity);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);
    }
}
