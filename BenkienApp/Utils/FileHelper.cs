﻿using BenkienApp.Data.Entity;

namespace BenkienApp.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/Img/")
        {
            string fileName = "";

            fileName = formFile.FileName;
            string directory = Directory.GetCurrentDirectory() + "/wwwroot" + filePath + fileName;
            using var stream = new FileStream(directory, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return fileName;
        }


        public static bool FileRemover(string fileName, string filePath = "/wwwroot/Img/")
        {
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;
            if (File.Exists(directory)) // File.Exists metodu .net içinde var olan ve kendisine verilen dizinde dosya var mı yok mu kontrol eden bir metottur
            {
                File.Delete(directory); // File.Delete metodu bir dizinden dosya siler
                return true; // dosya silindikten sonra metot geriye true döner
            }
            return false; // yukardaki silme kodu çalışmazsa metot geriye false döner böylece işlem sonucundan haberdar olabiliriz.
        }

        public class FileHelperNew
        {
            public static async Task<List<ProductImage>> FileLoaderAsync(List<IFormFile> formFiles, string filePath = "/Products/")
            {
                List<ProductImage> productImages = new List<ProductImage>();

                foreach (var formFile in formFiles)
                {
                    string fileName = formFile.FileName;
                    string directory = Directory.GetCurrentDirectory() + "/wwwroot/Products" + filePath + fileName;

                    try
                    {
                        using var stream = new FileStream(directory, FileMode.Create);
                        await formFile.CopyToAsync(stream);

                        // Her bir dosya için yeni bir ProductImage oluşturup listeye ekleyin
                        productImages.Add(new ProductImage { ImageName = fileName });
                    }
                    catch (Exception ex)
                    {
                        // Dosya kopyalama işlemi sırasında bir hata oluştuğunda hata mesajını göster
                        Console.WriteLine($"Dosya kopyalama hatası: {ex.Message}");
                    }
                }

                return productImages;
            }


            public static bool FileRemover(string fileName, string filePath = "/wwwroot/Products/")
            {
                string directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Products", filePath, fileName);

                try
                {
                    if (File.Exists(directory))
                    {
                        File.Delete(directory);
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    // Dosya silme sırasında bir hata oluştuğunda false dönün
                    return false;
                }
            }
        }



    }
}
