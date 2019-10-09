using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biz.Resiliency.ApiGateway.PriceAggregator.Dtos;
using Biz.Resiliency.ApiGateway.PriceAggregator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApiClient _productClient;
        private readonly ICustomerApiClient _customerClient;

        public ProductController(
            IProductApiClient productClient,
            ICustomerApiClient customerClient)
        {
            _productClient = productClient;
            _customerClient = customerClient;
        }

        [HttpGet("{customerId}:Guid")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<ProductDto>> GetAllByCustomerIdAsync(Guid customerId)
        {
            // Fetch products with regular prices
            var products = await _productClient.GetAllAsync();

            // Fetch customer with discount
            var customer = await _customerClient.GetAsync(customerId);

            decimal discount = customer?.Discount ?? 0m;

            // Calculate new prices
            products.ToList().ForEach(p => CalculatePrice(p, discount));

            return products;
        }

        private void CalculatePrice(ProductDto product, decimal discount)
        {
            product.Price = Math.Round(product.Price - (product.Price * discount / 100), 2);
        }
    }
}