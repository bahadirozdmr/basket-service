// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System;
using Basket.Dto;
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

        public BasketController(ILogger<BasketController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet()]
        public ActionResult GetPayments()
        {
            var response = new BasketDto() {BasketId = Guid.NewGuid(), CreateDate = DateTime.Now};
            var message=_configuration.GetSection("PaymentGateway:Message").Value;
            response.Message = message;
            return Ok(response);
        }
    }
}
