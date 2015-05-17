using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Advertisements.Infrastructures.InputModels.Advertisements;
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advertisements.Web;
using Advertisements.Web.Controllers;
using Moq;
using AutoMapper.QueryableExtensions;

namespace Advertisements.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest : BaseControllerTests
    {
        private readonly Mock<IHomeServices> homeServiceMock;
        private readonly HomeController controller;

        public HomeControllerTest()
        {
            this.homeServiceMock = new Mock<IHomeServices>(MockBehavior.Strict);
            this.controller = new HomeController(this.homeServiceMock.Object, this.dataMock.Object, this.dataLoaderMock.Object);
        }

        [TestMethod]
        public void IndexMethodShouldReturnAllAds()
        {
            var inputModel = new RightSideBarViewModel { PageSize = 10 };
            this.homeServiceMock.Setup(x => x.GetAdsByPageSize(It.IsAny<RightSideBarViewModel>()))
                .Returns(Common.GetAds().AsQueryable().Project().To<AdsIndexViewModel>());
            this.homeServiceMock.Setup(x => x.GetAllAds(It.IsAny<RightSideBarViewModel>()))
                .Returns(Common.GetAds().AsQueryable().Project().To<AdsIndexViewModel>());
            this.dataLoaderMock.Setup(x => x.GetCategoriesSelectListItem())
                .Returns(Common.GetCategories().AsQueryable());
            this.dataLoaderMock.Setup(x => x.GetTownsSelectListItem())
                .Returns(Common.GetTowns().AsQueryable());

            var result = this.controller.Index(inputModel) as ViewResult;

            Assert.IsNotNull(result, "Index action returns null.");
            var model = result.Model as IndexViewModel;
            Assert.IsNotNull(model, "The model is null.");
            Assert.AreEqual(Common.GetAds().Count(), model.AdsIndexViewModel.Count(), "Index action returns different number of ads.");
            Assert.AreEqual(Common.GetCategories().Count, model.RightSideBarViewModel.Categories.Count(), "Index action returns different number of categories.");
            Assert.AreEqual(Common.GetTowns().Count, model.RightSideBarViewModel.Towns.Count(), "Index action returns different number of towns.");
        }
    }
}
