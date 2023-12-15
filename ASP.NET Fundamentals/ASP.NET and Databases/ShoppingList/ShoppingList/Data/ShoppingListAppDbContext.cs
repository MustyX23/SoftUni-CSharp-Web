using Microsoft.EntityFrameworkCore;
using ShoppingList.Data.Models;

namespace ShoppingList.Data
{
    public class ShoppingListAppDbContext : DbContext
    {

        public ShoppingListAppDbContext(DbContextOptions<ShoppingListAppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductNote> ProductNotes { get; set; } = null!;   

        protected ShoppingListAppDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           //modelBuilder.Entity<Product>()
           //    .HasMany(p => p.ProductNotes)
           //    .WithOne(r => r.Product);

        }
    }
}
