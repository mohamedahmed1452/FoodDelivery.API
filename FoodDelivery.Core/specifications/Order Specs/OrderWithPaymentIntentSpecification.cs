using FoodDelivery.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.specifications.Order_Specs
{
    public class OrderWithPaymentIntentSpecification:Specifications<Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
