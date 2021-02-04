using System;
using rate_limiter.Storage;
using rate_limiter.Strategies;

namespace rate_limiter.Factory
{
    public class RateLimiterFactory
    {
        public static StrategyBase GetFixedWindowRateLimiterStrategy(int maximumRequests, int windowMinutes)
        {
            return new FixedWindowStrategy(new MemoryStorage(), maximumRequests, windowMinutes);
        }
    }
}
