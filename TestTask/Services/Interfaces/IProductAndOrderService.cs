using TestTask.Entities;

namespace TestTask.Services.Interfaces
{
    public interface IProductAndOrderService
    {
        IEnumerable<ProductAndOrder> GetProductAndOrder();

        Task SetProductAndOrder(Guid productId, Guid orderId);
    }
}
