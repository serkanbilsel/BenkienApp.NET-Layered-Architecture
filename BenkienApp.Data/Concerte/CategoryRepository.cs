using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BenkienApp.Data.Concerte
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Category> GetCategoryByIncludeAsync(int id)
        {
            return await _dbSet.Where(c => c.Id == id).AsNoTracking().Include(c => c.Products).FirstOrDefaultAsync();
        }
    }
}
