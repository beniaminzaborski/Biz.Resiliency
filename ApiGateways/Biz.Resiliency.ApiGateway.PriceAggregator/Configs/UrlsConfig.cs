using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Configs
{
    public class UrlsConfig
    {
        public class ProductOperations
        {
            public static string GetAll() => "/api/product";
        }

        public class CustomerOperations
        {
            public static string GetAll() => "/api/customer";
            public static string Get(Guid id) => $"/api/customer/{id}";
        }

        public string Product { get; set; }
        public string Customer { get; set; }
    }
}
