using HomeTask2.DatabaseContext;
using HomeTask2.DataModel;
using HomeTask2.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeTask2.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataBaseContext _dbContext;
        private DbSet<T> _table;
        public Repository(DataBaseContext context)
        {   
            _dbContext = context;
            _table = _dbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await _table.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> FindById(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            _table.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            _table.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
