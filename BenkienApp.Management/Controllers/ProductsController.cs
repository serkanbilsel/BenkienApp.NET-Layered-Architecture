using BenkienApp.Admin.Models;
using BenkienApp.Admin.Utils;
using BenkienApp.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using System.Text.Json.Serialization;
using System.Text.Json;
using BenkienApp.Data.Entity.BaseEntities;
using System.Linq;

namespace BenkienApp.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _httpClient;
        private readonly string _apiCategory = "https://localhost:7022/api/Categories";
        private readonly string _apiBrands = "https://localhost:7022/api/Brands";
        private readonly string _apiProduct = "https://localhost:7022/api/Product";
        public ProductsController(HttpClient httpClient, IWebHostEnvironment environment)
        {
            _httpClient = httpClient;
            _environment = environment;
        }
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {

            ViewBag.Products = new SelectList(await _httpClient.GetFromJsonAsync<List<Product>>(_apiProduct), "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiProduct);
            return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<Product>(_apiProduct + "/" + id);

            if (product == null)
            {
                return NotFound();
            }

            // Ürün detayları ve fotoğrafları için ProductViewModel oluştur
            var productViewModel = new ProductViewModel
            {
                Product = product,
                // Diğer özellikleri de gerekirse doldurun
                Categories = await _httpClient.GetFromJsonAsync<List<Category>>(_apiCategory),
                Brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiBrands),
            
            };

            return View(productViewModel);
        }





        // GET: ProductsController/Create
      
        public async Task<ActionResult> Create()
        {
            try
            {

                var viewModel = new ProductViewModel();

                // Kategori ve Marka listelerini ViewBag'e ekleyerek Create view'ını döndür
                var categories = await _httpClient.GetFromJsonAsync<List<Category>>(_apiCategory);
                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                var brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiBrands);
                ViewBag.Brands = new SelectList(brands, "Id", "Name");

                return View();
            }
            catch (Exception ex)
            {
                // Hata durumunda bir şeyler yapabilirsiniz, örneğin loglayabilir veya kullanıcıya bilgi verebilirsiniz
                ModelState.AddModelError("", $"Hata Oluştu: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ProductsController/CreateAsync
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProductViewModel viewModel, IEnumerable<IFormFile> ProductImages)
        {
            try
            {
                // Ürünü ekleyin
                var product = viewModel.Product;

                if (ProductImages != null)
                {
                    List<ProductImage> newImages = await FileHelperMulti.FileLoaderAsync(ProductImages);

                    // Koleksiyonun herbir elemanına image.Product'a product'ı atama işlemi yapılmıştır.
                    newImages.ForEach(image =>
                    {
                        image.Product = product;
                        image.ImageUrl = "/Products/" + image.ImageName; // Örnek bir ImageUrl ataması
                    });

                    product.ProductImages = newImages;
                }

                var response = await _httpClient.PostAsJsonAsync(_apiProduct, product);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "<div class='alert alert-success'>Ürün başarıyla eklendi</div>";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // API isteği başarısız olduysa ModelState'e hata ekleyebilirsiniz
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"API Hata Kodu: {response.StatusCode}. Hata Detayları: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata Oluştu: {ex.Message}");
            }

            // Kategori ve Marka listelerini ViewBag'e ekleyerek Create view'ını döndür
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>(_apiCategory);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiBrands);
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

            return View(nameof(Create));
        }






        // GET: ProductsController/Edit/5
       
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Eğer id null ise, 404 Not Found döndür
            }

            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiCategory), "Id", "Name");
            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiBrands), "Id", "Name");

            var model = await _httpClient.GetFromJsonAsync<Product>(_apiProduct + "/" + id);

            if (model == null)
            {
                return NotFound(); // Eğer model null ise, 404 Not Found döndür
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, ProductViewModel viewModel, IEnumerable<IFormFile> ProductImages, bool? resmiSil)
        {
            try
            {
                // Eğer yeni resimler varsa, bunları yükle
                if (ProductImages != null && ProductImages.Any())
                {
                    List<ProductImage> newImages = await FileHelperMulti.FileLoaderAsync(ProductImages);
                    viewModel.Product.ProductImages.AddRange(newImages);
                }
                if (resmiSil is not null && resmiSil.Value)
                {
                    // Silinecek resimlerin isimlerini al
                    //var imageNamesToDelete = viewModel.Product.ProductImagesToDelete?.Select(pi => pi.ImageName);

                    //if (imageNamesToDelete != null && imageNamesToDelete.Any())
                    //{
                    //    // Silinecek resimleri koleksiyondan kaldır
                    //    viewModel.Product.ProductImages.RemoveAll(pi => imageNamesToDelete.Contains(pi.ImageName));

                    //    // Kaldırılan resimleri fiziksel olarak sil
                    //    FileHelperMulti.FileRemover(imageNamesToDelete);
                    //}
                }



                var response = await _httpClient.PutAsJsonAsync(_apiProduct + "/" + id, viewModel.Product);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "<div class='alert alert-success'>Güncelleme Başarılı</div>";

                  
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // API isteği başarısız olduysa ModelState'e hata ekleyebilirsiniz
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"API Hata Kodu: {response.StatusCode}. Hata Detayları: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata oluştu: {ex.Message}");
            }

            // Kategori ve Marka listelerini ViewBag'e ekleyerek Edit view'ını döndür
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>(_apiCategory);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiBrands);
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

            return View(viewModel.Product);
        }



        // POST: ProductsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditAsync(int id, Product collection, IEnumerable<IFormFile> ProductImages, bool? resmiSil)
        //{
        //    try
        //    {
        //        // Eğer yeni resimler varsa, bunları yükle
        //        if (ProductImages != null && ProductImages.Any())
        //        {
        //            List<ProductImage> newImages = await FileHelperMulti.FileLoaderAsync(ProductImages);
        //            collection.ProductImages.AddRange(newImages);
        //        }

        //        var response = await _httpClient.PutAsJsonAsync(_apiProduct + "/" + id, collection);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["Message"] = "<div class='alert alert-success'>Güncelleme Başarılı</div>";

        //            // Resmi silme işlemleri
        //            if (resmiSil is not null && resmiSil.Value)
        //            {
        //                // Silinecek resimlerin isimlerini al
        //                var imageNamesToDelete = collection.ProductImagesToDelete.Select(pi => pi.ImageName);

        //                // Silinecek resimleri koleksiyondan kaldır
        //                collection.ProductImages.RemoveAll(pi => imageNamesToDelete.Contains(pi.ImageName));

        //                // Kaldırılan resimleri fiziksel olarak sil
        //                FileHelperMulti.FileRemover(imageNamesToDelete);
        //            }




        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Güncelleme işlemi başarısız oldu. Sunucu hatası.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", $"Hata oluştu: {ex.Message}");
        //    }

        //    // Kategori ve Marka listelerini ViewBag'e ekleyerek Edit view'ını döndür
        //    ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiCategory), "Id", "Name");
        //    ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiBrands), "Id", "Name");
        //    TempData["Message"] = "<div class='alert alert-success'>Güncelleme Başarılı</div>";
        //    return View(collection);
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteImage(int productId, List<ProductImageToDelete> imagesToDelete)
        {
            try
            {
                // İlgili ürünü bul
                var product = _httpClient.GetFromJsonAsync<Product>(_apiProduct + "/" + productId).Result;

                if (product == null)
                {
                    return Json(new { success = false, error = "Ürün bulunamadı." });
                }

                // Resmi silme işlemini gerçekleştir
                foreach (var imageToDelete in imagesToDelete)
                {
                    var deletedImage = product.ProductImages.FirstOrDefault(pi => pi.ImageName == imageToDelete.ImageName);
                    if (deletedImage != null)
                    {
                        product.ProductImages.Remove(deletedImage);
                        FileHelperMulti.FileRemover(new List<string> { imageToDelete.ImageName });

                        // Ürünü güncelle
                        var response = _httpClient.PutAsJsonAsync(_apiProduct + "/" + productId, product).Result;

                        if (!response.IsSuccessStatusCode)
                        {
                            return Json(new { success = false, error = "Ürün güncellenirken bir hata oluştu." });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "Silinmek istenen resim bulunamadı." });
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }






        //public async Task<ActionResult> EditAsync(int id, Product collection, List<IFormFile> imageFiles, bool? resmiSil)
        //{
        //    try
        //    {
        //        if (resmiSil is not null && resmiSil.Value)
        //        {
        //            // Resmi silme işlemleri
        //            FileHelperMulti.FileRemover(collection.Image);
        //            collection.Image = "";
        //        }

        //        if (imageFiles != null)
        //        {
        //            // Yeni dosya adlarını kullanarak Images listesini güncelleme
        //            List<ProductImage> newImages = await FileHelperMulti.FileLoaderAsync(imageFiles);


        //            // Yeni dosya adlarını kullanarak ProductImage listesini oluşturma
        //            //collection.Images = newImages;
        //        }

        //        var response = await _httpClient.PutAsJsonAsync(_apiProduct + "/" + id, collection);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["Message"] = "<div class='alert alert-success'>Güncelleme Başarılı</div>";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        if (resmiSil is not null && resmiSil.Value)
        //        {
        //            // Resmi silme işlemleri
        //            FileHelperMulti.FileRemover(collection.Image);
        //            collection.Image = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", $"Hata oluştu: {ex.Message}");
        //    }

        //    // Kategori ve Marka listelerini ViewBag'e ekleyerek Edit view'ını döndür
        //    ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiCategory), "Id", "Name");
        //    ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiBrands), "Id", "Name");

        //    return View(collection);
        //}


        // BURADA KALDIM HALA MULTIPLE UPLOAD KISMINDAYIM

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiProduct + "/" + id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiProduct + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }









    }
}
