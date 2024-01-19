using BenkienApp.Data;
using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenkienApp.Service.Abstract
{
    public interface IProductImageService
    {

        Task<List<ProductImage>> GetImagesByProductIdAsync(int productId);
    }
    public class ProductImageService : IProductImageService
    {
        private readonly DatabaseContext _dbContext; // DbContext'inizi uygun bir şekilde değiştirin.

        public ProductImageService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductImage>> GetImagesByProductIdAsync(int productId)
        {
            // Burada belirli bir ürün ID'sine ait resimleri veritabanından çekin.
            return await _dbContext.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        // Diğer metotlar...
    }
}
