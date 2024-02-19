using TestTask.Entities;

namespace TestTask.Services.Interfaces
{
    public interface IOrderService
    {
        bool CancelOrderById(Guid orderToCancelId);

        IEnumerable<Order> GetAllOrders();

        Task SaveOrder(Order order);

        Task DeleteOrder(Order order);
    }



}
