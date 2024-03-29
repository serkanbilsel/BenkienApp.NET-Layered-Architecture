﻿using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenkienApp.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.Image).HasMaxLength(50);
            builder.Property(x => x.ProductCode).HasMaxLength(50);
            // FluentAPI ile class lar arası ilişki kurma
            builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(f => f.BrandId); // Brand Class ı ile Product class ı arasında 1 e çok ilişki vardır.
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(c => c.CategoryId); // Category Class ı ile Products class ı arasında 1 e çok ilişki vardır.
            builder.HasMany(x => x.ProductImages)
          .WithOne(x => x.Product)
          .HasForeignKey(x => x.ProductId);
            // ProductId, ProductImages tablosundaki Product'a referans veren sütun adı

        }
    }
}
