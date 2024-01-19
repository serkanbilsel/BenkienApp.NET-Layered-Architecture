using BenkienApp.Data;
using BenkienApp.Data.Concerte;
using BenkienApp.Service.Abstract;

namespace BenkienApp.Service.Concrete
{
    public class CategoryService : CategoryRepository, ICategoryService
    {
        public CategoryService(DatabaseContext context) : base(context)
        {
        }
    }
}
