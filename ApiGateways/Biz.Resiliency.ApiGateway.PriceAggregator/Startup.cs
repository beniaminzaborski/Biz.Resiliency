using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Biz.Resiliency.ApiGateway.PriceAggregator.Configs;
using Biz.Resiliency.ApiGateway.PriceAggregator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Caching;
using Polly.Extensions.Http;
using Swashbuckle.AspNetCore.Swagger;

namespace Biz.Resiliency.ApiGateway.PriceAggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc(Configuration)
                .AddApplicationServices()
                .AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app
                 .UseHttpsRedirection()
                 .UseMvc()
                 .UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "Biz PriceAggregator API v1"));
        }
    }

    public static class PriceAggregatorExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<UrlsConfig>(configuration.GetSection("Urls"));

            services
                .AddOptions()
                .Configure<UrlsConfig>(configuration.GetSection("Urls"))
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Biz.ApiGateway.PriceAggregator.API", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // Register http services
            services.AddHttpClient<IProductApiClient, ProductApiClient>()
                .AddPolicyHandler(GetCachePolicy());

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>()
                .AddPolicyHandler(GetCachePolicy());

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetCachePolicy()
        {
            // In-memory cache
            return Policy
                .CacheAsync<HttpResponseMessage>(memoryCacheProvider, TimeSpan.FromMinutes(2),
                onCacheGet: (c, s) => 
                {
                    Console.WriteLine("Polly: onCacheGet");
                },
                onCacheMiss: (c, s) => 
                {
                    Console.WriteLine("Polly: onCacheMiss");
                },
                onCachePut: (c, s) => 
                {
                    Console.WriteLine("Polly: onCachePut");
                },
                onCacheGetError: (c, s, e) => 
                {
                    Console.WriteLine("Polly: onCacheGetError");
                },
                onCachePutError: (c, s, e) => 
                {
                    Console.WriteLine("Polly: onCachePutError");
                });
        }

        static Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions());
        static Polly.Caching.Memory.MemoryCacheProvider memoryCacheProvider = new Polly.Caching.Memory.MemoryCacheProvider(memoryCache);
    }
}
