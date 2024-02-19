using TestTask.DAL.Interfaces;
using TestTask.DAL.Repositories;
using TestTask.Entities;
using TestTask.Services.ConcrateServices;
using TestTask.Services.Interfaces;

namespace TestTask
{
    public static class Initializer
    {
        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IProductAndOrderService, ProductAndOrderService>();
        }

        public static void InitializeRepositories(this IServiceCollection services) 
        {
            services.AddScoped<IBaseRepository<Product>, ProductRepository>();

            services.AddScoped<IBaseRepository<Order>, OrderRepository>();

            services.AddScoped<IBaseRepository<ProductAndOrder>, ProductAndOrderRepository>();
        }
    }
}
