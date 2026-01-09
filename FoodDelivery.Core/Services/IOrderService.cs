using FoodDelivery.Core.Entities.Order;

namespace FoodDelivery.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreatOrderAsync(string buyerEmail, int delivryMethodId, string basketId, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
