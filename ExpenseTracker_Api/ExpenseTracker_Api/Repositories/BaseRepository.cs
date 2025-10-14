using ExpenseTracker_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker_Api.Repositories
{
    public class BaseRepository<T> where T : class
    {
        protected readonly ExpenseTrackerDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual int Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            return id;
        }

        protected IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
