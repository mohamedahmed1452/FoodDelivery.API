using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Entities.Order
{
    public enum OrderStatus
    {
        Pending,
        PaymentReceived,
        PaymentFailed
    }
}
