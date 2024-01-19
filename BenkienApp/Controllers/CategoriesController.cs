using BenkienApp.Data.Entity;
using BenkienApp.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BenkienApp.Controllers
{
    public class CategoriesController : Controller

    {
        private readonly IService<Category> _service;
        private readonly IProductService _serviceProduct;
        public CategoriesController(IService<Category> service, IProductService serviceProduct)
        {
            _service = service;
            _serviceProduct = serviceProduct;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _service.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            model.Products = await _serviceProduct.GetAllAsync(p=>p.CategoryId == id);
            return View(model);
        }
    }
}
