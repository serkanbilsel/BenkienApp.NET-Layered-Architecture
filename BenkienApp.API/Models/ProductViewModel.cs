using BenkienApp.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BenkienApp.API.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductImages = new List<IFormFile>();
            Categories = new List<Category>();  // Kategorileri başlat
            Brands = new List<Brand>();
        }

        [Display(Name = "Ürün")]
        public Product Product { get; set; }

        [Display(Name = "Kategoriler")]
        public IEnumerable<Category> Categories { get; set; }

        [Display(Name = "Markalar")]
        public List<Brand> Brands { get; set; }

        [Display(Name = "Ürün Resimleri")]
        public List<IFormFile> ProductImages { get; set; }
    }

}
