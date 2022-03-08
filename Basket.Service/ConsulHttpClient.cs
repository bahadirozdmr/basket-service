// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Consul;
using Newtonsoft.Json;

namespace Basket.Service
{
    public class ConsulHttpClient :IConsulHttpClient
    {
        private readonly IConsulClient _consulClient;

        private readonly IHttpClientFactory _httpClientFactory;

        public ConsulHttpClient(IConsulClient consulClient, IHttpClientFactory httpClientFactory)
        {
            _consulClient = consulClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string serviceName,string path)
        {
            var uri = await GetRequestUri(serviceName, path);

            using (var client=_httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(UrlValidator(uri,path));

                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);
            }
           
        }

        private string UrlValidator(string url, string path)
        {
            if (url.EndsWith("/"))
            {
                return string.Concat(url, path);
            }

            return string.Concat(url + "/" + path);
        }

        public  async Task<string> GetRequestUri(string serviceName,string path)
        {
            var allRegisteredServices =  await _consulClient.Agent.Services();
            
            var registeredServices = allRegisteredServices.Response?.Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToList();
            
            var service = GetRandomInstance(registeredServices, serviceName);

            if (service == null)
            {
                throw new ConsulConfigurationException($"Service Not Found {serviceName}");
            }
            
           

            var uriBuilder = new UriBuilder()
            {
                Host = service.Address,
                Port = service.Port
            };

            return uriBuilder.Uri.ToString();

        }
        
        private AgentService GetRandomInstance(IList<AgentService> services, string serviceName)
        {
            Random _random = new Random();

            AgentService servToUse = null;

            servToUse = services[_random.Next(0, services.Count)];

            return servToUse;
        }
    }
}
