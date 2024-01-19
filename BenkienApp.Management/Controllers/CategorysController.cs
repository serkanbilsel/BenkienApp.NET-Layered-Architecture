using BenkienApp.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenkienApp.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategorysController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdress = "https://localhost:7022/api/Categories";

        public CategorysController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiAdress, category);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "<div class='alert alert-success'>Başarıyla Eklendi</div>";
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hata Oluştu : " + ex.Message);
            }
            return View(category);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdress + "/" + id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category collection)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiAdress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>Görev Başarıyla Tamamlandı!</div>";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdress + "/" + id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Category collection)
        {
            try
            {
                //FileHelper.FileRemover(collection.);
                await _httpClient.DeleteAsync(_apiAdress + "/" + id);
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
