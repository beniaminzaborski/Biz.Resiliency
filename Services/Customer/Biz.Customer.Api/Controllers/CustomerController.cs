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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return new List<CustomerDto>();
        }
    }
}