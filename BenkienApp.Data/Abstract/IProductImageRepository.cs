using BenkienApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Abstract
{
    public interface IProductImageRepository
    {
        Task<List<ProductImage>> GetProductImagesAsync(int productId);
        // Diğer CRUD operasyonları buraya ekleyebilirsiniz.
    }
}
