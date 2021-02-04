using System;
using rate_limiter.Storage;
using rate_limiter.Models;

namespace rate_limiter.Strategies
{
    public abstract class StrategyBase
    {
        protected readonly IStorage storage;
        public StrategyBase(IStorage storage)
        {
            this.storage = storage;
        }

        public abstract RlResponse ValidateRequest(RlRequest request);
    }
}
