using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;
using System.Linq.Expressions;

namespace BenkienApp.Service.Abstract
{
    public interface IProductService : IProductRepository
    {
        Task AddImageAsync(ProductImage image);
        Task SaveImageAsync();
        Task<List<Product>> GetAllWithImagesAsync(Expression<Func<Product, bool>> filter = null);

    }
}
