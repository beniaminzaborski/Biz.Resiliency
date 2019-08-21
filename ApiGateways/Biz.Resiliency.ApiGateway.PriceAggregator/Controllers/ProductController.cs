using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biz.Resiliency.ApiGateway.PriceAggregator.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{customerId}:Guid")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<ProductDto>> GetAllByCustomerIdAsync(Guid customerId)
        {
            // TODO: Fetch products with regular prices
            // TODO: Fetch customer with discount
            // TODO: Calculate new prices
            return new List<ProductDto>();
        }
    }
}