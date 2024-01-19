using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BenkienApp.Data.Concerte
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Product> GetProductByIncludeAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByIncludeAsync()
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByIncludeAsync(Expression<Func<Product, bool>> expression)
        {
            return await _context.Products
                .Where(expression)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }

        public async Task<List<ProductImage>> GetProductImagesByIncludeAsync(Expression<Func<ProductImage, bool>> expression)
        {
            return await _context.ProductImages
                .Where(expression)
                .Include(pi => pi.Product)
                .ToListAsync();
        }




    }


}

