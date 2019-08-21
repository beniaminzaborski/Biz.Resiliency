using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
