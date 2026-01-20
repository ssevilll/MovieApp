using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Interfaces;
using MovieApp.DataAccess.Models;
using System.Linq.Expressions;

namespace MovieApp.DataAccess.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MovieAppDbContext _context;
        private readonly DbSet<T> table;
        public Repository(MovieAppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await table.AddAsync(entity);
        }
        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            var query=table.AsQueryable();
            if (filter != null)
                query = query.Where(filter);
            return query;
        }

        public IQueryable<T> GetAll(bool isTracking = false, int page = 1, int take = 2, params string[] includes)
        {
            var query=table.AsQueryable();
            if (!isTracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                    query = query.Include(includeProperty);
            }
            query = query.Skip((page - 1) * take).Take(take);
            return query;
        }

        public IQueryable<T> GetAll(bool isTracking = false, Expression<Func<T, bool>> filter = null, params string[] includes)
        {
            var query=table.AsQueryable();
            if (!isTracking)
                query = query.AsNoTracking();
            if (filter != null)
                query = query.Where(filter);
            if (includes != null)
            {
                foreach (var includesProperty in includes)
                    query = query.Include(includesProperty);
            }
            return query;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id, bool isTracking = false, params string[] includes)
        {
            var query= table.AsQueryable();
            if (!isTracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var  includedProperty in includes)
                    query = query.Include(includedProperty);
            }
            return await query.FirstOrDefaultAsync(e=>e.Id==id);
        }

        public Task<bool> isExistAsync(Expression<Func<T, bool>> filter)
        {
            return table.AnyAsync(filter);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            table.Update(entity);
        }
    }
}
