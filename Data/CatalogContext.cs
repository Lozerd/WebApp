#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories");  // Create Category Table
            modelBuilder.Entity<Product>().ToTable("Products");  // Create Product table

            // Configuring primary keys
            modelBuilder.Entity<Category>().HasKey(c => c.Id).HasName("PK_Categories");
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId).HasName("PK_Products");

            // Configuring columns
            // Entity<Category>
            modelBuilder.Entity<Category>().Property(c => c.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Title).HasColumnType("varchar(255)").IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Description).HasColumnType("longtext").IsRequired(false);

            // Entity<Category>
            modelBuilder.Entity<Product>().Property(p => p.ProductId).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ProductName).HasColumnType("varchar(255)").IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ProductDescription).HasColumnType("text").IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).HasColumnType("int").IsRequired();

            // Configuring relations
            modelBuilder.Entity<Product>().HasOne<Category>().WithMany()
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Products_Categories");

        }

        internal List<Product> GetCategoryRelatedProducts(int category_id = 0)
        {
            List<Product> products = Products.Where(p => p.CategoryId == category_id).ToList();
            if (products.Count == 0)
                return null;
            return products;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = Categories.ToList();
            if (categories.Count == 0)
                return null;
            return categories;
        }
    }
}
