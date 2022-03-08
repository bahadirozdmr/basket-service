// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this
using System.Threading.Tasks;
using Basket.Dto;

namespace Basket.Service
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetPayments();
    }
}
    