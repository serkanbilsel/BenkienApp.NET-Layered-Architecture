using BenkienApp.Data;
using BenkienApp.Data.Entity;
using BenkienApp.Models;
using BenkienApp.Service.Abstract;
using BenkienApp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BenkienApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService<User> _service;

        public AccountController(IService<User> service)
        {
            _service = service;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId is null)
            {
                TempData["Message"] = "<div class='alert alert-danger'>Lütfen Giriş Yapınız!</div>";
                return RedirectToAction("Login");
            }
            else
            {
                var user = await _service.GetAsync(u => u.Id == userId);
                return View(user);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(User appUser, IFormFile? Image, bool? resmiSil)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("userId");
                var user = await _service.GetAsync(u => u.Id == userId);

                if (Image is not null)
                {
                    appUser.Image = await FileHelper.FileLoaderAsync(Image);
                }
                

                if (user != null)
                {
                    // Diğer alanları güncelle
                    user.Name = appUser.Name;
                    user.Surname = appUser.Surname;
                    user.Email = appUser.Email;
                    user.Phone = appUser.Phone;
                    user.AdressDetail = appUser.AdressDetail;
                    user.Image = appUser.Image;
                    // Şifre değişmişse yeni şifreyi ata
                    if (!string.IsNullOrEmpty(appUser.Password))
                    {
                        user.Password = appUser.Password;
                    }
                    if (resmiSil is not null && resmiSil == true)
                    {
                        FileHelper.FileRemover(appUser.Image);
                        appUser.Image = "";
                    }

                    _service.Update(user);
                    await _service.SaveAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }

            // Başarılı kayıt mesajı burada değil, başarı durumunda return RedirectToAction içinde olmalı
            ModelState.AddModelError("", "Başarıyla Güncellendi!");
            return View("Index", appUser);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginViewModel viewModel)
        {
            var user = await _service.GetAsync(x => x.Email == viewModel.Email && x.Password == viewModel.Password && x.IsActive);
            if (user == null)
            {
                ModelState.AddModelError("", "Giriş Başarısız!");
            }
            else
            {
                HttpContext.Session.SetString("userName", user.Name);
                HttpContext.Session.SetInt32("userId", user.Id);
                HttpContext.Session.SetString("userGuid", user.UserGuid.ToString());
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("userId");
                HttpContext.Session.Remove("userGuid");
            }
            catch
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(User appUser)
        {
            try
            {
                var kullanici = await _service.GetAsync(x => x.Email == appUser.Email);
                if (kullanici != null)
                {
                    ModelState.AddModelError("", "Bu Mail İle Daha Önce Kayıt Olunmuş!");
                    return View();
                }
                else
                {
                    appUser.RoleId = 3;
                    appUser.UserGuid = Guid.NewGuid();
                    appUser.IsActive = true;
                    appUser.IsAdmin = false;
                    await _service.AddAsync(appUser);
                    await _service.SaveAsync();
                    TempData["Message"] = "<div class='alert alert-success'>Kayıt Başarılı! Giriş Yapabilirsiniz..</div>";
                    return RedirectToAction(nameof(Login));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Kayıt olunurken bilinmeyen bir hata oluştu!");
            }
            return View(appUser);
        }
        public IActionResult NewPassword()
        {
            return View();
        }
    }
}
