using BenkienApp.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BenkienApp.Admin.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Categories = new List<Category>();
            Brands = new List<Brand>();
            ProductImages = new List<ProductImage>();
        }

        public Product? Product { get; set; }

        [Display(Name = "Kategoriler")]
        public IEnumerable<Category> Categories { get; set; }

        [Display(Name = "Markalar")]
        public List<Brand> Brands { get; set; }

        [Display(Name = "Ürün Resimleri")]
        public List<ProductImage> ProductImages { get; set; }
        public IEnumerable<IFormFile> ProductImagesToDelete { get; set; }


    }



}

