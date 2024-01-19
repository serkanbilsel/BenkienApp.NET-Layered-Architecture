using BenkienApp.Data.Entity;


namespace BenkienApp.Models
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }



        public List<Product>? RelatedProducts { get; set; }

        //public string Image => Product?.Image;
        //public string Name => Product?.Name;
        //public string Description => Product?.Description;
        //public string Price => Product?.Price.ToString() ?? "N/A";

        //public string ImageUrl => Product.ImageUrl;
        //public int Id { get; set; } // Set edilebilir hale getirildi


    }


}


