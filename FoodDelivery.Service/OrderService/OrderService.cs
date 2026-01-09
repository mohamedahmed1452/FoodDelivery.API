using FoodDelivery.Core;
using FoodDelivery.Core.Entities.Order;
using FoodDelivery.Core.Entities.Product;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Core.Services;
using FoodDelivery.Core.specifications;
using FoodDelivery.Core.specifications.Order_Specs;
namespace FoodDelivery.Application.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IUnitOfWork unitOfWork;
        public OrderService(IBasketRepository BasketRepo,
            IUnitOfWork unitOfWork)

        {
            basketRepo = BasketRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreatOrderAsync(string buyerEmail, int delivryMethodId, string basketId, Address shippingAddress)
        {
            //Get Basket From basketId
            var basket = await basketRepo.GetBasketAsync(basketId);
            //Create OrderItems =>items in Basket
            var Items = new List<OrderItem>();
            if (basket?.Items.Count > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product>();
                foreach (var item in basket?.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    var productItem = new ProductItemOrdered(productId: item.Id,
                        productName: product.Name, pictureUrl: product.PictureUrl);
                    var orderItem = new OrderItem(product: productItem,
                        price: product.Price, quantity: item.Quantity);
                    Items.Add(orderItem);
                }
            }
            //Get SubTotal
            var subTotal = Items.Sum(o => o.Quantity * o.Price);
            //Get Delivery Method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod>().GetAsync(delivryMethodId);
            //Create Order
            var order = new Order(buyerEmail: buyerEmail,
                status: OrderStatus.Pending,
                shippingAddress: shippingAddress,
                deliveryMethod: deliveryMethod,
                items: Items,
                subTotal: subTotal
               );

            await unitOfWork.GetRepository<Order>().AddAsync(order);
            var res = await unitOfWork.CompleteAsync();
            return res > 0 ? order : null;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await unitOfWork.GetRepository<DeliveryMethod>().GetAllAsync();
        }



        public async Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            //get all orders
            var specParams = new OrderSpecParams(buyerEmail, orderId);
            var spec = new OrderSpecifications(specParams);
            var order =await unitOfWork.GetRepository<Order>().GetWithSpecAsync(spec);
            return order is not null ? order : null;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var specParams = new OrderSpecParams(buyerEmail,null);
            var spec = new OrderSpecifications(specParams);
            var orders =await unitOfWork.GetRepository<Order>().GetAllWithSpecAsync(spec);

            return orders is not null ? orders : null;

        }
    }
}
