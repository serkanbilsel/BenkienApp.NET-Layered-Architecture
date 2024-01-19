using BenkienApp.Data.Entity;
using BenkienApp.Data.Entity.BaseEntities;
using Microsoft.AspNetCore.Mvc;

namespace BenkienApp.Admin.Utils
{
    public static class FileHelperMulti
    {
        public static async Task<List<ProductImage>> FileLoaderAsync(IEnumerable<IFormFile> files)
        {
            List<ProductImage> images = new List<ProductImage>();

            foreach (var file in files)
            {
                var fileName = $"File_{Guid.NewGuid()}.jpg";
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Products");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(fileStream);

                images.Add(new ProductImage { ImageName = fileName });
            }

            return images;
        }

        public static bool FileRemover(IEnumerable<string> fileNames, string filePath = "/wwwroot/Products/")
        {
            bool success = true;

            foreach (var fileName in fileNames)
            {
                string directory = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);

                if (File.Exists(directory))
                {
                    try
                    {
                        File.Delete(directory);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file {fileName}: {ex.Message}");
                        success = false;
                    }
                }
                else
                {
                    Console.WriteLine($"File {fileName} not found.");
                    success = false;
                }
            }

            return success;
        }






    }


}



