using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.specifications.Product_Specs
{
    public class ProductSpecParams
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string? Sort { get; set; } = null;
        private const int MaxPageSize = 10;
        private int pageSize = 5;
        public int PageIndex { get; set; } = 1;
        public string Search { get; set; }
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

    }
}
