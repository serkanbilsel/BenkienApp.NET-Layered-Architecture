using BenkienApp.Data;
using BenkienApp.Data.Abstract;
using BenkienApp.Data.Concerte;
using BenkienApp.Data.Entity;
using BenkienApp.Service.Abstract;
using BenkienApp.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddSession(); // uygulamada session kullanabilmek
//builder.Services.AddDbContext<DatabaseContext>();
//builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
//builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<DatabaseContext>();

// IService<> ve Service<>'yi ekleyin (ihtiyaca bağlı olarak)
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

// Sadece bir kez ekleyin
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
// Startup.cs
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();






builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>// oturum işlemleri için
{
    x.LoginPath = "/Admin/Login"; // oturum açmayan kullanıcıların giriş için göndereliceği adres
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccesDenied"; // yetkilendirme ile ekrana erişim hakkı olmayan kullanıcıların gönderileceği sayfa
    x.Cookie.Name = "Administrator"; //  Oluşacak cookie ismi
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // Oluşacak cooki'nin yaşam süresi 1 gün.
});
//Uygulama admin paneli için admin yetkilendirme aarları

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // admin paneline giriş yapma yetkisine sahip olanları bu  kuralla kontrol edeceğiz.
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin dışında yetkilendirme kullanıırsak bu kuralı kullanabiliriz(siteye üye girişi yapanları ön yüzde bir paneldepanele eriştirme gibi)
});



var app = builder.Build();



//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    string? connStr = builder.Configuration.GetConnectionString("DBConStr"); // Builder konfigürasyonu içerisinde "DBConStr" appsettings.json deðerini oku.

//    options.UseSqlServer(connStr);
//});






// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // session için 
app.UseAuthentication(); // Dİkkat! önce UseAuthentication satırı gelmeli sonra Use Authorization

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Uygulama ayaða kalktýðýnda, belirtilen Database'i getir.

//    var db = dbContext.Database;

//    if (!await db.CanConnectAsync()) // Eðer ilgili database'yi bulamýyorsan 
//    {
//        await db.EnsureCreatedAsync();

//    }
//}


app.Run();
