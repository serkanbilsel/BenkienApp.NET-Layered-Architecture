using BenkienApp.Data;
using BenkienApp.Data.Abstract;
using BenkienApp.Data.Concerte;
using BenkienApp.Data.Entity;
using BenkienApp.Service.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BenkienApp.Service.Concrete
{
    public class ProductService : ProductRepository, IProductService, IProductImageService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductImage>> GetImagesByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        public async Task AddImageAsync(ProductImage productImage)
        {
            await AddImageAsync(productImage);
        }

        public async Task SaveImageAsync()
        {
            await SaveAsync();
        }

        public async Task<List<Product>> GetAllWithImagesAsync(Expression<Func<Product, bool>> filter = null)
        {
            var products = await GetAllAsync(filter);
            foreach (var product in products)
            {
                product.ProductImages = await GetImagesByProductIdAsync(product.Id);
            }
            return products;
        }
    }

}
