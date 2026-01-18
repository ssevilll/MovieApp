using MovieApp.DataAccess.Models;

namespace MovieApp.DataAccess.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        void Delete(T entity);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task SaveChangesAsync();
        void Update(T entity);
    }
}