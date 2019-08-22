using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Discount { get; set; }
    }
}
