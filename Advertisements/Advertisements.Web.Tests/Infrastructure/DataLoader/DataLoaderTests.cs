using System;
using System.Linq;
using Advertisements.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advertisements.Web.Infrastructure.DataLoader;
using System.Collections.Generic;

namespace Advertisements.Web.Tests.Infrastructure.DataLoader
{
    [TestClass]
    public class DataLoaderTests
    {
        private readonly IAdsData data;
        private readonly EfDataLoader dataLoader;
        
        public DataLoaderTests()
        {
            this.data = new AdsData();
            this.dataLoader = new EfDataLoader(this.data);
        }

        [TestMethod]
        public void GetTownsShouldReturnAllTowns()
        {
            // TODO: Implement mocking.
            var actual = this.data.Towns.All().ToList();
            
            var expected = this.dataLoader.GetTowns().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
