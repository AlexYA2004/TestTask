using TestTask.DAL;
using TestTask.DAL.Interfaces;
using TestTask.Entities;
using TestTask.Services.Interfaces;

namespace TestTask.Services.ConcrateServices
{

    public class ProductAndOrderService : IProductAndOrderService
    {
        private IBaseRepository<ProductAndOrder> _productAndOrderRepository;

        public ProductAndOrderService(IBaseRepository<ProductAndOrder> productAndOrderRepository)
        {
            _productAndOrderRepository = productAndOrderRepository;   
        }

        public IEnumerable<ProductAndOrder> GetProductAndOrder()
        {
            return _productAndOrderRepository.GetAll();
        }

        public async Task SetProductAndOrder(Guid productId, Guid orderId)
        {
            var productAndOrder = new ProductAndOrder { OrderId = orderId, ProductId = productId };

            await _productAndOrderRepository.CreateAsync(productAndOrder);
        }
    }
}
