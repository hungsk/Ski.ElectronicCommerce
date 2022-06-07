using Microsoft.EntityFrameworkCore;
using Ski.Demo1.Domain;

namespace Ski.Demo1.Data
{
    public class Demo1DbContext : DbContext
    {
        public Demo1DbContext()
        {
        }

        public Demo1DbContext(DbContextOptions<Demo1DbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"data source=(localdb)\MSSQLLocalDB;database=Demo1");  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImagesUrl>()
                 .HasKey(c => new { c.url, c.productid });

            //modelBuilder.Entity<OrderItem>()
            //     .HasKey(c => new { c.orderid, c.productid });

            //modelBuilder.Entity<Order>()
            //    .HasOne(b => b.user)
            //    .WithMany(c => c.);

            //modelBuilder.Entity<Order>()
            //    .HasMany(p => p.product);
        }

        //Product
        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<ImagesUrl> ImagesUrl { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}