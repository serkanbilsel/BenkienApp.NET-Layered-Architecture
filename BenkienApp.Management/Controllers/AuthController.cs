using BenkienApp.Admin.Models;
using BenkienApp.Data.Entity;
using BenkienApp.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BenkienApp.Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly string _apiUserAdress = "https://localhost:7022/api/Users";
        private readonly string _apiRoleAdress = "http://localhost:7022/api/Roles";

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login), "Auth");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {

            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiUserAdress);
            var account = users.Where(x => x.Email == loginModel.Email && x.Password == loginModel.Password).FirstOrDefault();

            if (account == null)
            {

                ModelState.AddModelError("", "Yetkili değilsiniz!");
                TempData["Message"] = "<div class='alert alert-danger'>Giriş Başarısız!</div>";

                return View(loginModel);
            }
            else
            {

                if (account.RoleId == 1)
                {
                    var userAccess = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.Role, "Admin")

                    };

                    var userIdentity = new ClaimsIdentity(userAccess, "Login");


                    ClaimsPrincipal claimsPrincipal = new(userIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);

                    HttpContext.Session.SetInt32("userId", account.Id);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ModelState.AddModelError("", "Yetkiniz Bulunmamaktadır");
                    TempData["Message"] = "<div class='alert alert-danger'>You have not permitted!</div>";
                    return View(loginModel);
                }

            }
        }

        public async Task<IActionResult> UpdateUser()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("userId");

                User? account = await _httpClient.GetFromJsonAsync<User>(_apiUserAdress + "/" + userId);

                return View(account);
            }
            catch (Exception) 
            {
            }
            TempData["Message"] = "<div class='alert alert-danger'>İlk önce giriş yapmalısınız!</div>";
            return RedirectToAction("Index", "Main");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    user.Image = await FileHelper.FileLoaderAsync(Image);
                }

                var userId = HttpContext.Session.GetInt32("userId");

                User? account = await _httpClient.GetFromJsonAsync<User>(_apiUserAdress + "/" + userId);

                if (account != null)
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _httpClient.PutAsJsonAsync(_apiUserAdress + "/" + userId, user);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["Message"] = "<div class='alert alert-success' >Account Updated successfully... </div>";

                            return RedirectToAction("Index", "Main");
                        }

                    }
                }
            }
            catch (Exception e)
            {

            }
            ModelState.AddModelError("", "Update Failed");
            return View("UpdateUser", user);
        }

    }
}
