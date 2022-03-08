// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System.Threading.Tasks;

namespace Basket.Service
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string serviceName, string path);
    }
}
