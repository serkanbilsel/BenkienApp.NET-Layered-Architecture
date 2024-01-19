using BenkienApp.Data.Configurations;
using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BenkienApp.Data
{
    public class DatabaseContext : DbContext
    {
        
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Contact> Contacts { get; set; }
     
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // OnCinfiguring metodu EntitityFrameWorkCore ile gelir ve veritabanı bağlantı ayarlarını yapmamızı sağlar.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=BENKIENDB; Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);

            //CANLI BİR DATABASE BAĞLANTISI YAPMAK İÇİN AŞAĞIDAKİ ADIMLARI UYGULA

            // optionsBuilder.UseSqlServer(@"Server=CanlıServerAdı; Database=CanlıDatabaseAdı;
            // Username=CanlıVeritabanıKullanıcıAdı; Password=CanlıVeritabanıŞifre);
            //base.OnConfiguring(optionsBuilder);
        }

      


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Role tablosuna bir rol ekleyin
            modelBuilder.Entity<Role>().HasData(
        new Role
        {
            //RoleId = 1,
            Id = 1,
            RoleName = "Admin",
            // Diğer özellikleri doldurun...
        },
        new Role
        {
            //RoleId = 2,
            Id = 2,
            RoleName = "Yönetici",
            // Diğer özellikleri doldurun...
        },
        new Role
        {
            //RoleId = 3,
            Id = 3,
            RoleName = "Kullanıcı",
            // Diğer özellikleri doldurun...
        }


    );

           
           

            base.OnModelCreating(modelBuilder);


            // Users tablosuna bir kullanıcı ekleyin
            modelBuilder.Entity<User>().HasData(new User
            {
                // Id sütununa değer atamayın, veritabanı tarafından otomatik olarak atanacak
                Id = 1,
                Email = "info@Benkien.com",
                Password = "123",
                UserName = "Admin",
                IsActive = true,
                IsAdmin = true,
                Name = "Admin",
                AdressDetail = "Topkapı / Istanbul",
                CreateDate = DateTime.Now,
                Phone = "05077149058",
                RoleId = 1,
                Image = "",
                UserGuid = Guid.NewGuid(),
                // Diğer özellikleri doldurun...
            });


            base.OnModelCreating(modelBuilder);
        }






    }
}



