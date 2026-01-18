using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Models;

namespace MovieApp.DataAccess.Repository
{
    public class Repository<T>(MovieAppDbContext context) : IRepository<T> where T : BaseEntity
    {
        //crud operations
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
        public T GetById(int id)
        {
            return context.Set<T>().FirstOrDefault(e => e.Id == id);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
