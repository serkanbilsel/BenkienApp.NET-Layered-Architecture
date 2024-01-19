using BenkienApp.Data;
using BenkienApp.Service.Abstract;
using BenkienApp.Service.Concerte;
using BenkienApp.Service.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//builder.Services.AddControllersWithViews();
//builder.Services.AddSession(); // uygulamada session kullanabilmek
//builder.Services.AddDbContext<DatabaseContext>();
//builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
//builder.Services.AddTransient<IProductService, ProductService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IPostService, PostService>();


builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
