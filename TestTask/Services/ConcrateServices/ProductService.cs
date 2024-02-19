using TestTask.DAL.Interfaces;
using TestTask.DAL.Repositories;
using TestTask.Entities;
using TestTask.Services.Interfaces;

namespace TestTask.Services.ConcrateServices
{
    public class ProductService : IProductService
    {
        private IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public bool AbleProductById(Guid id)
        {
            var productToAble = _productRepository.GetAll().Where(p => p.Id == id).FirstOrDefault();

            if (productToAble == null)
                return false;

            productToAble.IsAvailaleToOrder = true;

            return true;
        }

        public async Task AddNewProduct(Product product)
        {
            await _productRepository.CreateAsync(product);
        }

        public bool DisableProduct(ProductInfo product)
        {
            var productToDisable = _productRepository.GetAll()
                                                     .Where(p => p.Name == product.Name && p.Description == product.Description && p.Price == product.Price)
                                                     .FirstOrDefault();

            if (productToDisable == null)
                return false;

            productToDisable.IsAvailaleToOrder = false;

            _productRepository.UpdateAsync(productToDisable);

            return true;
        }

        public IEnumerable<Product> GetAllProducts()
        {
           return _productRepository.GetAll();
        }
    }
}
