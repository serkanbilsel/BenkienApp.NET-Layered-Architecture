using BenkienApp.Data.Entity;
using BenkienApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BenkienApp.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUser = "https://localhost:7022/api/Users";
        private readonly string _apiRoleAddress = "https://localhost:7022/api/Roles";
        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       


        public async Task<ActionResult> Index()
        { 
               
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUser), "Id", "RoleName");

            var model = await _httpClient.GetFromJsonAsync<List<User>>(_apiUser);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create()
        {
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoleAddress), "Id", "RoleName");

            return View();
        }
        public async Task<ActionResult> Create(User collection, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    // Eğer resim yüklendiyse, resmi kaydet ve dosya adını collection.Image'e ata
                    collection.Image = await FileHelper.FileLoaderAsync(Image, "/img/");
                }
                var response = await _httpClient.PostAsJsonAsync(_apiUser, collection);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "<div class='alert alert-success'>Görev Başarılı</div>";
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hata oluştu : " + e.Message);
            }
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoleAddress), "Id", "RoleName");

            return View(collection);
        }
        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoleAddress), "Id", "RoleName");

            var model = await _httpClient.GetFromJsonAsync<User>(_apiUser + "/" + id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(int id, User collection, IFormFile? Image)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiUser + "/" + id, collection);
            //if (Image is not null)
            //{
            //    // Eğer resim yüklendiyse, resmi kaydet ve dosya adını collection.Image'e ata
            //    collection.Image = await FileHelper.FileLoaderAsync(Image, "/img/");
            //}
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>Güncelleme Başarılı</div>";
            }
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoleAddress), "Id", "RoleName");

            return RedirectToAction(nameof(Index));
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<User>(_apiUser + "/" + id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, User collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiUser + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
