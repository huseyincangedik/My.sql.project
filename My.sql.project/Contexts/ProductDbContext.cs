using Microsoft.EntityFrameworkCore;
using My.sql.project.Models;


namespace My.sql.project.Contexts
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product tablosu adını belirleme
            modelBuilder.Entity<Product>().ToTable("products");

            // Order tablosu adını belirleme
            modelBuilder.Entity<Order>().ToTable("orders");

            modelBuilder.Entity<User>().ToTable("users");

            // Product ve Order arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product) // Order'ın bir tane Product'ı vardır.
                .WithMany(p => p.Orders) // Product'ın ise birden fazla Order'ı olabilir.
                .HasForeignKey(o => o.ProductId); // Foreign key olarak OrderId'deki ProductId'yi kullan.

            // Users tablosu
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()                       
                .HasColumnType("enum('user','admin')");
        }
    }
}