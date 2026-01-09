using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.specifications.Order_Specs
{
    public class OrderSpecParams
    {
        public OrderSpecParams(string? buyerEmail, int? orderId)
        {
            BuyerEmail = buyerEmail;
            OrderId = orderId;
        }

        public string? BuyerEmail { get; set; }
        public int? OrderId { get; set; }


    }
}
