using System;
namespace rate_limiter.Models
{
    public class RequestContext
    {
        private ContextKey _contextKey;
        private string _contextValue;

        public RequestContext(ContextKey key, string value)
        {
            this._contextKey = key;
            this._contextValue = value;
        }

        public string RequestIdentifier => $"{this._contextKey} : {this._contextValue}";
    }
}
