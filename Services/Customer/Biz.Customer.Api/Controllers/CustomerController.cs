using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biz.Customer.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biz.Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private List<CustomerDto> list = new List<CustomerDto>();

        private void LoadFakeData()
        {
            list.AddRange(
                new List<CustomerDto>
                {
                    new CustomerDto { Id = new Guid("195ADBA9-61AE-411C-BE02-7B497E55376C"), Name = "Customer 01", Discount = 10 },
                    new CustomerDto { Id = new Guid("4646BAF7-9823-4427-B742-568E51BD6445"), Name = "Customer 02", Discount = 15 }
                });
        }

        public CustomerController()
        {
            LoadFakeData();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return list;
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CustomerDto>> GetAsync(Guid id)
        {
            return NotFound();

            //var customer = list.FirstOrDefault(p => p.Id == id);
            //if (customer == null)
            //    return NotFound();

            //return customer;
        }
    }
}