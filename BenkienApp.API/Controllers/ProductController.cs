using BenkienApp.Admin.Models;
using BenkienApp.Data.Entity;
using BenkienApp.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BenkienApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAsync()
        {
            var products = await _service.GetProductsByIncludeAsync();

            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetAsync(int id)
        {
            var product = await _service.GetProductByIncludeAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }




        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product viewModel)
        {
            try
            {
                // Eğer ModelState geçerli değilse, yani gelen verilerde eksik veya hatalı alanlar varsa,
                // BadRequest ile birlikte ModelState hatalarını döndürebilirsiniz.
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Eğer gelen verilerde eksik alanlar yoksa, işlemi devam ettir
                await _service.AddAsync(viewModel);
                await _service.SaveAsync();

                // Oluşturulan ürünü Ok (200) ile döndür
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir hata mesajıyla birlikte InternalServerError (500) döndür
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }





        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Product value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest(new { Message = "Geçersiz veya boş ürün bilgisi.", StatusCode = 400 });
                }

                // Güncelleme işlemi
                var AddProduct = await _service.GetProductByIncludeAsync(id);

                if (AddProduct == null)
                {
                    return NotFound(new { Message = $"ID'si {id} olan ürün bulunamadı.", StatusCode = 404 });
                }

                // Güncelleme işlemleri
                AddProduct.Name = value.Name;
                AddProduct.Description = value.Description;
                AddProduct.Brand = value.Brand;
                AddProduct.BrandId = value.BrandId;
                AddProduct.ProductImages = value.ProductImages;
                AddProduct.CategoryId = value.CategoryId;
                AddProduct.Description = value.Description;
                AddProduct.Price = value.Price;
                AddProduct.Category = value.Category;
                AddProduct.IsActive = value.IsActive;
                AddProduct.IsHome = value.IsHome;
                AddProduct.ProductCode = value.ProductCode;
                //AddProduct.ProductImagesToDelete = value.ProductImagesToDelete;
                AddProduct.IsAvailable = value.IsAvailable;
                AddProduct.UpdatedAt = value.UpdatedAt;

                // Diğer özelliklerin güncellenmesi

                _service.Update(AddProduct);
                var sonuc = await _service.SaveAsync();

                if (sonuc > 0)
                {
                    return Ok(AddProduct);
                }

                return Problem("Ürün güncellenirken bir hata oluştu.", statusCode: 500);
            }
            catch (Exception ex)
            {
                // Hata durumunu handle et
                return BadRequest(new { Message = "Ürün güncellenirken bir hata oluştu.", ExceptionMessage = ex.Message, StatusCode = 500 });
            }
        }




        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var data = await _service.FindAsync(id);

                if (data == null)
                {
                    return NotFound();
                }

                _service.Delete(data);
                var sonuc = await _service.SaveAsync();

                if (sonuc > 0)
                {
                    return Ok(data);
                }

                return Problem("Ürün silinirken bir hata oluştu.", statusCode: 500);
            }
            catch (Exception ex)
            {
                // Hata durumunu handle et
                return BadRequest(new { Message = "Ürün silinirken bir hata oluştu.", ExceptionMessage = ex.Message });
            }
        }
    }
}
