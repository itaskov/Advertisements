using System;
using System.Linq;
using Advertisements.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advertisements.Web.Infrastructure.DataLoader;
using System.Collections.Generic;
using System.Web.Mvc;
using Advertisements.Web.Infrastructure.Caching;

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
            this.dataLoader = new EfDataLoader(this.data, new AspNetCurrentAppCache());
        }

        [TestMethod]
        public void GetTownsShouldReturnAllTowns()
        {
            // TODO: Implement mocking.
            var actual = this.data.Towns.All().ToList();
            
            var expected = this.dataLoader.GetTowns().ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCategoriesSelectListItemShouldReturnAllCategories()
        {
            // TODO: Implement mocking.
            var actual = this.data.Categories.All()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                })
                .ToList();

            var expected = this.dataLoader.GetCategoriesSelectListItem().ToList();

            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void GetCategoriesShouldReturnAllCategories()
        {
            // TODO: Implement mocking.
            var actual = this.data.Categories.All().ToList();

            var expected = this.dataLoader.GetCategories().ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
