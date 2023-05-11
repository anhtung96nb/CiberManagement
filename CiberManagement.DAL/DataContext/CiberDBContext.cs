using CiberManagement.DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CiberManagement.DAL.DataContext
{
    public class CiberDBContext:IdentityDbContext<User>
    {
        public CiberDBContext()
        {
        }

        public CiberDBContext(DbContextOptions<CiberDBContext> options)
            : base(options)
        {
        }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TUNGSOFTWARE\\TUNGDV;Initial Catalog=CiberManagement;Integrated Security=True");
        }
        public DbSet<Category> Categories { get; set; } 

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()
                        .HasMany<Product>(p=>p.Products)
                        .WithOne(c => c.Category)
                        .HasForeignKey(c=>c.CategoryID)
                        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>()
                        .HasOne<Customer>(o => o.Customer)
                        .WithMany(c => c.Orders)
                        .HasForeignKey(o => o.CustomerID)
                        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>()
                        .HasOne<Product>(o => o.Product)
                        .WithMany(p => p.Orders)
                        .HasForeignKey(o => o.ProductID)
                        .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }
        
    }
}
