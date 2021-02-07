using System;
using rate_limiter.Models;
using rate_limiter.Storage;
using rate_limiter.Strategies;
using Xunit;

namespace rate_limiter_tests
{
    public class FixedWindowStrategyTest
    {
        [Fact]
        public void ShouldAllowIncomingRequest()
        {
            var strategy = new FixedWindowStrategy(new MemoryStorage(), 2, 5);
            var context = new RequestContext(ContextKey.IpAddress, "1.2.3.4");
            var result = strategy.ValidateRequest(new RlRequest(context, DateTime.UtcNow));
            Assert.True(result.Allowed, "Request should not be rejected");
        }

        [Fact]
        public void ShouldNotAllowIncomingRequest()
        {
            var strategy = new FixedWindowStrategy(new MemoryStorage(), 0, 5);
            var context = new RequestContext(ContextKey.IpAddress, "1.2.3.4");
            var result = strategy.ValidateRequest(new RlRequest(context, DateTime.UtcNow));
            Assert.False(result.Allowed, "Request should be rejected");
        }
    }
}
