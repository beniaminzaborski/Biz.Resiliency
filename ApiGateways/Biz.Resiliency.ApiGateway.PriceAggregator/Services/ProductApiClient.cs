using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Biz.Resiliency.ApiGateway.PriceAggregator.Configs;
using Biz.Resiliency.ApiGateway.PriceAggregator.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;

namespace Biz.Resiliency.ApiGateway.PriceAggregator.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly UrlsConfig urls;

        public ProductApiClient(
            HttpClient httpClient,
            IOptions<UrlsConfig> config)
        {
            _apiClient = httpClient;
            urls = config.Value;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var context = new Polly.Context("Products"); // Create cache context and set cache key

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urls.Product + UrlsConfig.ProductOperations.GetAll());
            request.SetPolicyExecutionContext(context); // Set cache context on HttpRequestMessage

            var response = await _apiClient.SendAsync(request);
            //var response = await _apiClient.GetAsync(urls.Product + UrlsConfig.ProductOperations.GetAll());

            response.EnsureSuccessStatusCode();
            
            var productResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(productResponse);
        }
    }
}
