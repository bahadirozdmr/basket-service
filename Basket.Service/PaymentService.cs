// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System.Net.Http;
using System.Threading.Tasks;
using Basket.Dto;

namespace Basket.Service
{
    public class PaymentService :IPaymentService
    {
        private readonly IConsulHttpClient _consulHttpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentService(IConsulHttpClient consulHttpClient, IHttpClientFactory httpClientFactory)
        {
            _consulHttpClient = consulHttpClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaymentDto> GetPayments()
        {
            var response=await _consulHttpClient.GetAsync<PaymentDto>("payment-service", "payment");
            return response;
        }
    }
}
