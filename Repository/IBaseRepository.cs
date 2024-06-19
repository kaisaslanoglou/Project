using System.Linq.Expressions;
namespace HandmadeShop.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        //T - Category
        

        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);


        /*IEnumerable<T> GetAll(string? includeProperties=null);
         T Get(Expression<Func<T, bool>> filter, string? includeProperties=null);
         void Add(T entity);
         void Delete(T entity);
         void DeleteRange(IEnumerable<T> entities);*/

    }
}
