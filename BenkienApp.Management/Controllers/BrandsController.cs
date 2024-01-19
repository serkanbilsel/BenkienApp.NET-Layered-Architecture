using BenkienApp.Data.Entity;
using BenkienApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace BenkienApp.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandsController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "https://localhost:7022/api/Brands"; // DEĞİŞECEK
        public BrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAddress);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Brand collection, IFormFile? Logo)
        {
            try
            {
                if (Logo is not null)
                {
                    collection.Logo = await FileHelper.FileLoaderAsync(Logo);
                }
               

                var response = await _httpClient.PostAsJsonAsync(_apiAddress, collection);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "<div class='alert alert-success'>Başarıyla Eklendi</div>";
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hata oluştu : " + e.Message);
            }
            return View(collection);
        }
        // GET: BrandsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Brand collection, IFormFile? Image, bool? resmiSil)
        {
            try
            {
                if (resmiSil is not null && resmiSil == true)
                {
                    FileHelper.FileRemover(collection.Logo);
                    collection.Logo = "";
                }
                if (Image is not null)
                {
                    collection.Logo = await FileHelper.FileLoaderAsync(Image);
                }

            }
            catch
            {
                TempData["Message"] = "<div class='alert alert-success'>Hata!</div>";
            }
            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: BrandController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Brand collection)
        {
            try
            {
                //FileHelper.FileRemover(collection.);
                await _httpClient.DeleteAsync(_apiAddress + "/" + id);
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }


}
