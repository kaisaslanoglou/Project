using HandmadeShop.Models;

namespace HandmadeShop.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task UpdateAsync(Product product);

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
