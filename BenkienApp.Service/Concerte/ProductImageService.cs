// BenkienApp.Service/Concerte/ProductImageService.cs
using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;
using BenkienApp.Service.Abstract;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;

    public ProductImageService(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<List<ProductImage>> GetProductImagesAsync(int productId)
    {
        return await _productImageRepository.GetProductImagesAsync(productId);
    }

    public async Task<List<ProductImage>> GetImagesByProductIdAsync(int productId)
    {
        // Burada ilgili işlemleri gerçekleştirin.
        // Örneğin:
        // return await _productImageRepository.GetImagesByProductIdAsync(productId);

        // Eğer bu metotta bir dönüş değeri sağlanamıyorsa, örneğin bir hata durumunda null dönebilirsiniz.
        return null;
    }

    // Diğer servis metotlarını buraya ekleyebilirsiniz.
}
