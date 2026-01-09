using FoodDelivery.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FoodDelivery.Core.specifications.Order_Specs
{
    public class OrderSpecifications:Specifications<Order>
    {
        public OrderSpecifications(OrderSpecParams orderSpec):base(
            o=>
          (string.IsNullOrEmpty(orderSpec.BuyerEmail) || o.BuyerEmail.ToLower().Equals(orderSpec.BuyerEmail.ToLower())) &&
         (!orderSpec.OrderId.HasValue || o.Id==orderSpec.OrderId))
        {


            #region Inner Join =>eager loading
            Includes.Add(o => o.Items);
            Includes.Add(o => o.DeliveryMethod);

            #endregion




        }
        //public OrderSpecifications(string BuyerEmail) :base(
        //    o=>
        //    string.IsNullOrEmpty(BuyerEmail) || o.BuyerEmail.ToLower().Equals(BuyerEmail.ToLower())
        //    )
        //{

        //    #region Inner Join =>eager loading
        //    Includes.Add(o => o.Items);
        //    #endregion

        //}


    }
}
