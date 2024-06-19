namespace HandmadeShop.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository {  get; }
        IProductRepository ProductRepository {  get; }

        Task SaveAsync();
    }
}
