using System;
using System.Linq;
using Advertisements.Data;
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.Tests.AllTestsCommon;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Advertisements.Infrastructures.Tests.Services
{
    [TestClass]
    public class HomeServicesTests : BaseServicesTests
    {
        private readonly IHomeServices homeServices;
        
        public HomeServicesTests()
        {
            var adsRepoMock = new Mock<IRepository<Advertisement>>(MockBehavior.Strict);
            adsRepoMock.Setup(x => x.All()).Returns(Common.GetAds().AsQueryable());

            this.dataMock.Setup(x => x.Advertisements).Returns(adsRepoMock.Object);
            
            this.homeServices = new HomeServices(this.dataMock.Object);
        }
        
        [TestMethod]
        public void GetAllAdsMethodShouldReturnAllAds()
        {
            var result = this.homeServices.GetAllAds();

            Assert.IsNotNull(result, "GetAllAds method returns null.");
            Assert.IsInstanceOfType(result, typeof(IQueryable<AdsIndexViewModel>));
            Assert.AreEqual(Common.GetAds().Count(), result.Count(), "GetAllAds method returns different number of ads.");
        }

        [TestMethod]
        public void GetAllAdsMethodShouldReturnAllAdsByRightSidebarViewModel()
        {
            var model = new RightSideBarViewModel
            {
                CategoryId = 1,
                TownId = 1
            };
            
            var result = this.homeServices.GetAllAds(model);

            Assert.IsNotNull(result, "GetAllAds method returns null.");
            Assert.IsInstanceOfType(result, typeof(IQueryable<AdsIndexViewModel>));
            Assert.AreEqual(Common.GetAds().AsQueryable().Count(a => a.TownId == 1 && a.CategoryId == 1), result.Count(), "GetAllAds method returns different number of ads.");
        }

        [TestMethod]
        public void GetAdsByPageSizeMethodShouldReturnAllAdsForFirstPage()
        {
            var model = new RightSideBarViewModel
            {
                CategoryId = 1,
                TownId = 1,
                PageSize = 5
            };

            var result = this.homeServices.GetAdsByPageSize(model);

            Assert.IsNotNull(result, "GetAllAds method returns null.");
            Assert.IsInstanceOfType(result, typeof(IQueryable<AdsIndexViewModel>));
            Assert.AreEqual(Common.GetAds().AsQueryable().Take(model.PageSize).Count(), result.Count(), "GetAllAds method returns different number of ads.");
        }

        [TestMethod]
        public void GetAdsByPageSizeMethodShouldReturnAllAdsForSecondPage()
        {
            var model = new RightSideBarViewModel
            {
                CategoryId = 1,
                TownId = 1,
                PageSize = 5,
                
                // Expected number of ads to return 1.
                SelectedPage = 2
            };

            var result = this.homeServices.GetAdsByPageSize(model);

            Assert.IsNotNull(result, "GetAllAds method returns null.");
            Assert.IsInstanceOfType(result, typeof(IQueryable<AdsIndexViewModel>));
            Assert.AreEqual(Common.GetAds().AsQueryable().Take(1).Count(), result.Count(), "GetAllAds method returns different number of ads.");
        }
    }
}
