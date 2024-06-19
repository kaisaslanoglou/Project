using HandmadeShop.Models;

namespace HandmadeShop.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task UpdateAsync(Category category);
           
    }
}
