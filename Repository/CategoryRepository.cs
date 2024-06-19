using HandmadeShop.Data;
using HandmadeShop.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Linq.Expressions;

namespace HandmadeShop.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
