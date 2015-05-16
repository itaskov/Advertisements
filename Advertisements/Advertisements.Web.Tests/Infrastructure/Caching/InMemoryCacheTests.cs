using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advertisements.Web.Tests.Infrastructure.Caching
{
    [TestClass]
    public class InMemoryCacheTests
    {
        private readonly AspNetCurrentAppCache cache;

        public InMemoryCacheTests()
        {
            this.cache = new AspNetCurrentAppCache();
        }
        
        [TestMethod]
        public void InsertMethodShouldInsertDataToCache()
        {
            var cacheID = "Categories";
            object cacheValue = "1234";
            this.cache.Insert(cacheID, cacheValue);

            var result = HttpRuntime.Cache.Get(cacheID);

            Assert.AreEqual(cacheValue, result);
        }

        [TestMethod]
        public void GetMethodShouldGetDataFromCache()
        {
            var cacheID = "Object";
            object cacheValue = "1234";
            HttpRuntime.Cache.Insert(cacheID, cacheValue);

            var result = this.cache.Get(cacheID);

            Assert.AreEqual(cacheValue, result);
        }

        [TestMethod]
        public void RemoveMethodShouldClearDataFromCache()
        {
            var cacheID = "Categories";
            object cacheValue = "1234";
            HttpRuntime.Cache.Insert(cacheID, cacheValue);
            this.cache.Remove(cacheID);

            var result = HttpRuntime.Cache.Get(cacheID);

            Assert.AreEqual(null, result);
        }
    }
}
