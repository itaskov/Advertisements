using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace Advertisements.Web.Infrastructure.Caching
{
    public class AspNetCurrentAppCache : IAspNetCurrentAppCache
    {
        public void Insert(string cacheId, object cacheValue)
        {
            if (HttpRuntime.Cache.Get(cacheId) == null)
            {

                HttpRuntime.Cache.Insert(cacheId, cacheValue, null);
            }
        }

        public object Get(string cacheId)
        {
            var result = HttpRuntime.Cache.Get(cacheId);
            return result;
        }

        public object Remove(string cacheId)
        {
            return HttpRuntime.Cache.Remove(cacheId);
        }
    }
}