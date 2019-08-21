using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biz.Product.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biz.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private List<ProductDto> list = new List<ProductDto>();

        private void LoadFakeData()
        {
            list.AddRange(
                new List<ProductDto>
                {
                    new ProductDto { Id = new Guid("61FC205D-53F3-4AE6-9695-4AEAB47E021E"), Name = "Product 01", Price = 12.99m },
                    new ProductDto { Id = new Guid("E41D31F2-0935-4D98-A2BF-F70E155C9E80"), Name = "Product 02", Price = 7.50m },
                    new ProductDto { Id = new Guid("1AC9D352-6128-438E-9BF2-2AF87C571BD9"), Name = "Product 03", Price = 120.00m },
                    new ProductDto { Id = new Guid("2E8F5D4D-4782-4687-92F2-8B84896686A2"), Name = "Product 04", Price = 27.99m },
                    new ProductDto { Id = new Guid("C7862846-7248-40FB-BD49-E3EA615A2559"), Name = "Product 05", Price = 13.79m }
                });
        }

        public ProductController()
        {
            LoadFakeData();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return list;
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductDto>> GetAsync(Guid id)
        {
            var product = list.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return product;
        }
    }
}