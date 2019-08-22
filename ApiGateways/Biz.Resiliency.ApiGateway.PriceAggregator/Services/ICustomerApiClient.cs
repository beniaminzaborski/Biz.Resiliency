using Biz.Resiliency.ApiGateway.PriceAggregator.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Services
{
    public interface ICustomerApiClient
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto> GetAsync(Guid id);
    }
}
