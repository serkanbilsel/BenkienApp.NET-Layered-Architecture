using BenkienApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByIncludeAsync(int id);

        Task<List<Product>> GetProductsByIncludeAsync();

        Task<List<Product>> GetProductsByIncludeAsync(Expression<Func<Product, bool>> expression);

        Task<List<ProductImage>> GetProductImagesByIncludeAsync(Expression<Func<ProductImage, bool>> expression);
    }

}
