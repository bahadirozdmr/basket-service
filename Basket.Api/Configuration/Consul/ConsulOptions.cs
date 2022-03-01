// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace Basket.Api.Configuration.Consul
{
    public class ConsulOptions
    {
        public bool Enabled { get; set; }
        public string Host { get; set; }
        public string Service { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public bool PingEnabled { get; set; }
    }
}
