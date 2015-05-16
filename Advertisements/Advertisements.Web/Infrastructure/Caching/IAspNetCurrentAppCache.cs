using System.Collections.Generic;

namespace Advertisements.Web.Infrastructure.Caching
{
    public interface IAspNetCurrentAppCache
    {
        void Insert(string cacheId, object cacheValue);
        object Get(string cacheId);
        object Remove(string cacheId);
    }
}