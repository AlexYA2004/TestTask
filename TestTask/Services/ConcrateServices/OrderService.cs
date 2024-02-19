using TestTask.DAL.Interfaces;
using TestTask.Entities;
using TestTask.Services.Interfaces;

namespace TestTask.Services.ConcrateServices
{
    public class OrderService : IOrderService
    {
        private IBaseRepository<Order> _orderRepository;

        private IBaseRepository<Product> _productRepository;
         
        public OrderService(IBaseRepository<Order> orderRepository, IBaseRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            
            _productRepository = productRepository;
        }

        public bool CancelOrderById(Guid orderToCancelId)
        {
            var orderToCancel = _orderRepository.GetAll().Where(o => o.Id == orderToCancelId).FirstOrDefault();

            if (orderToCancel == null)
                return false;

            _orderRepository.DeleteAsync(orderToCancel);

            return true;
        }

        public async Task DeleteOrder(Order order)
        {
            await _orderRepository.DeleteAsync(order);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }


        public async Task SaveOrder(Order order)
        {
            await _orderRepository.CreateAsync(order);
        }
    }
}
