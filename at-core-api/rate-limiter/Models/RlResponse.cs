using System;
namespace rate_limiter.Models
{
    public class RlResponse
    {
        public bool Allowed { get; set; }
        public string Message { get; set; }
    }
}
