using System;
namespace rate_limiter.Models
{
    public enum ContextKey
    {
        IpAddress = 0,
        User = 1,
        Tenant = 2
    }
}
