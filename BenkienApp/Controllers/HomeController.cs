using BenkienApp.Data.Entity;
using BenkienApp.Models;
using BenkienApp.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BenkienApp.Controllers
{
   

    public class HomeController : Controller
    {
        private readonly IService<Slider> _serviceSlider;
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Contact> _serviceContact;
        private readonly IService<News> _serviceNews;
        private readonly IService<Brand> _serviceBrand;
        private readonly IProductImageService _productImageService;
        private readonly IService<Setting> _serviceSetting;

        public HomeController(IService<Slider> serviceSlider, IService<Product> serviceProduct, IService<Contact> serviceContact, IService<News> serviceNews, IService<Brand> serviceBrand, IService<Setting> serviceSetting, IProductImageService productImageService)
        {
            _serviceSlider = serviceSlider;
            _serviceProduct = serviceProduct;
            _serviceContact = serviceContact;
            _serviceNews = serviceNews;
            _serviceBrand = serviceBrand;

            _serviceSetting = serviceSetting;
            _productImageService = productImageService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _serviceSlider.GetAllAsync();
            var products = await _serviceProduct.GetAllAsync(p => p.IsActive && p.IsHome);

            // Asenkron olarak ProductImages al
            var productTasks = products.Select(async p => new ProductDetailViewModel
            {
                Product = p,
                ProductImages = await _productImageService.GetImagesByProductIdAsync(p.Id)
            });

            // Tüm asenkron görevleri tamamla
            var productViewModels = await Task.WhenAll(productTasks);

            var model = new HomePageViewModel()
            {
                Sliders = sliders,
                Products = productViewModels.ToList(),
                Brands = await _serviceBrand.GetAllAsync(b => b.IsActive),
                News = await _serviceNews.GetAllAsync(n => n.IsActive && n.IsHome),
                Contacts = await _serviceContact.GetAllAsync(),
                Settings = await _serviceSetting.GetAllAsync()
            };

            return View(model);
        }









        [HttpGet("iletisim")]
        public IActionResult ContactUs()
        {
            return View();
        }


        [HttpPost("iletisim")]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceContact.AddAsync(contact);
                    var sonuc = await _serviceContact.SaveAsync();
                  
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            TempData["Message"] = "<div class='alert alert-success'>Mesajınız Gönderildi! Teşekkürler..</div>";
            return RedirectToAction("ContactUs");
        }


        [Route("AccessDenied"),HttpPost]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
