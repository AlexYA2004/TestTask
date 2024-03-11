using Microsoft.EntityFrameworkCore;
using TestTask.Entities;
using TestTask.Services;

namespace TestTask.DAL
{
    public class AppDbContext :DbContext 
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductAndOrder> ProductsAndOrders { get; set; }

        public DbSet<User> Users { get; set; }

   

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAndOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId });

            modelBuilder.Entity<ProductAndOrder>()
                .HasOne(po => po.Product)
                .WithMany(p => p.ProductsAndOrder)
            .HasForeignKey(po => po.ProductId);

            modelBuilder.Entity<ProductAndOrder>()
                .HasOne(po => po.Order)
                .WithMany(o => o.ProductsAndOrder)
                .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.User) // У каждого заказа есть один пользователь
            .WithMany(u => u.Orders)   // У каждого пользователя много заказов
            .HasForeignKey(o => o.UserId);
        }
    }
}
