using Microsoft.EntityFrameworkCore;


namespace My.sql.project.Contexts
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Docker'daki MySQL'e bağlantı
        //    optionsBuilder.UseMySql(
        //        "Server=localhost;Port=3306;Database=productdb;User=root;Password=123456;",
        //        new MySqlServerVersion(new Version(8, 0, 43))
        //    );
        //}
protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product tablosu adını belirleme
            modelBuilder.Entity<Product>().ToTable("products");

            // Order tablosu adını belirleme
            modelBuilder.Entity<Order>().ToTable("orders");

            // Product ve Order arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product) // Order'ın bir tane Product'ı vardır
                .WithMany(p => p.Orders) // Product'ın ise birden fazla Order'ı olabilir
                .HasForeignKey(o => o.ProductId); // Foreign key olarak OrderId'deki ProductId'yi kullan
        }
    }
}