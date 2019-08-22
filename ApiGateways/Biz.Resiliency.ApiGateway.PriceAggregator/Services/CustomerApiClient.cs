using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Biz.Resiliency.ApiGateway.PriceAggregator.Configs;
using Biz.Resiliency.ApiGateway.PriceAggregator.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Services
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly UrlsConfig urls;

        public CustomerApiClient(
            HttpClient httpClient,
            IOptions<UrlsConfig> config)
        {
            _apiClient = httpClient;
            urls = config.Value;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var response = await _apiClient.GetAsync(urls.Customer + UrlsConfig.CustomerOperations.GetAll());

            response.EnsureSuccessStatusCode();

            var customerResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(customerResponse);
        }

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var response = await _apiClient.GetAsync(urls.Customer + UrlsConfig.CustomerOperations.Get(id));

            response.EnsureSuccessStatusCode();

            var customerResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerDto>(customerResponse);
        }
    }
}
