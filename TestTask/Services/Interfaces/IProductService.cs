using TestTask.Entities;
using TestTask.Entities.Models;

namespace TestTask.Services.Interfaces
{
    public interface IProductService
    {
        Task AddNewProduct(Product product);

        bool DisableProduct(ProductInfo product);

        bool AbleProductById(Guid id);

        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> FindProducts(string searchString);
    }
}
    

