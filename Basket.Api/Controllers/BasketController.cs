// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System;
using Basket.Dto;
using Basket.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPaymentService _paymentService;

        public BasketController(ILogger<BasketController> logger, IConfiguration configuration, IPaymentService paymentService)
        {
            _logger = logger;
            _configuration = configuration;
            _paymentService = paymentService;
        }

        [HttpGet()]
        public ActionResult GetBaskets()
        {
            var response = new BasketDto() {BasketId = Guid.NewGuid(), CreateDate = DateTime.Now};
            var message=_configuration.GetSection("PaymentGateway:Message").Value;
            var payment=_paymentService.GetPayments().Result;
            response.PaymentId = payment.PaymentId;
            //Call Payment Service
            response.Message = message;
            return Ok(response);
        }
    }
}
