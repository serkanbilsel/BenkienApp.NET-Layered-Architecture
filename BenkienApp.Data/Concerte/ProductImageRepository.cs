using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Concerte
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly DatabaseContext _context;

        public ProductImageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<ProductImage>> GetProductImagesAsync(int productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        // Diğer CRUD operasyonlarını buraya ekleyebilirsiniz.
    }
}
