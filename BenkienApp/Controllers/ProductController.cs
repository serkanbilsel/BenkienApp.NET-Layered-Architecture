using BenkienApp.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using BenkienApp.Models;
using BenkienApp.Data.Entity;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;



namespace BenkienApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _serviceProduct;

        public ProductController(IProductService serviceProduct)
        {
            _serviceProduct = serviceProduct;

        }
        //[Route("Tum-Urunler")] // adres çubuğundan tum-urunlerimiz yazınca bu action çalışsın
        public async Task<IActionResult> IndexAsync()
        {
            var products = await _serviceProduct.GetAllAsync(p => p.IsActive == true, include => include.Include(p => p.ProductImages));
            var model = products.Select(p => new ProductDetailViewModel
            {
                Product = p,
                ProductImages = p.ProductImages ?? new List<ProductImage>() // Eğer null ise boş bir liste oluştur
            }).ToList();

            return View(model);
        }












        public async Task<IActionResult> Search(string q) // adres çubuğunda query string ile 
        {
            var model = await _serviceProduct.GetProductsByIncludeAsync(p => p.IsActive && p.Name.Contains(q) || p.Description.Contains(q) || p.Brand.Name.Contains(q) || p.Category.Name.Contains(q));
            return View(model);
        }


        public async Task<IActionResult> DetailAsync(int id)
        {
            var model = new ProductDetailViewModel();
            var product = await _serviceProduct.GetProductByIncludeAsync(id);
            model.Product = product;
            model.RelatedProducts = await _serviceProduct.GetAllAsync(p => p.CategoryId == product.CategoryId && p.Id != id);
            if (model is null)
            {
                return NotFound();
            }
            return View(model);
        }


    }
}
