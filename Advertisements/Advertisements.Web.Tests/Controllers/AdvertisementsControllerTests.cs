﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Advertisements.Data;
using Advertisements.Infrastructures.InputModels.Advertisements;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;
using Advertisements.Tests.Common;
using Advertisements.Web.Controllers;
using Advertisements.Web.Infrastructure.DataLoader;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Advertisements.Web.Tests.Controllers
{
    [TestClass]
    public class AdvertisementsControllerTests : BaseControllerTests
    {
        private readonly Mock<IAdvertisementsService> advertisementsServiceMock;
        private AdvertisementsController controller;

        public AdvertisementsControllerTests()
        {
            this.advertisementsServiceMock = new Mock<IAdvertisementsService>(MockBehavior.Strict);
            this.controller = new AdvertisementsController(this.advertisementsServiceMock.Object, this.dataMock.Object,
                this.dataLoaderMock.Object);
        }


        [TestMethod]
        public void IndexMethodShouldReturnAllUserAds()
        {
            var inputModel = new RightSideBarViewModel();
            inputModel.PageSize = 10;

            var mockContext = new Mock<HttpContextBase>(MockBehavior.Strict);

            var userId = "16267522-28e1-498d-a3a4-cfea9608531b";
            var claim = new Claim("Administrator", userId);
            var mockIdentity = new Mock<ClaimsIdentity>();
            mockIdentity.Setup(x => x.FindFirst(It.IsAny<string>())).Returns(claim);
            mockContext.Setup(x => x.User.Identity).Returns(mockIdentity.Object);

            var adsRepoMock = new Mock<IRepository<Advertisement>>();
            adsRepoMock.Setup(x => x.All()).Returns(Common.GetAds().AsQueryable());

            var uowMock = new Mock<IAdsData>();
            uowMock.Setup(x => x.Advertisements).Returns(adsRepoMock.Object);

            this.controller = new AdvertisementsController(uowMock.Object);
            this.controller.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), controller);
            var viewResult = this.controller.Index(inputModel) as ViewResult;
            Assert.IsNotNull(viewResult, "Index action returns null.");

            var model = viewResult.Model as IndexViewModel;
            Assert.IsNotNull(model, "Index action view model is null.");

            var sideBarViewModel = model.RightSideBarViewModel;
            Assert.IsNotNull(sideBarViewModel, "Index action RightSideBarViewModel is null.");

            var adsViewModel = model.AdsIndexViewModel;
            Assert.IsNotNull(adsViewModel, "Index action AdsIndexViewModel is null.");
            Assert.AreEqual(Common.GetAds().Count(a => a.Owner.Id == userId), adsViewModel.Count(), "Index action returns different number of ads than expected.");
        }
        
        [TestMethod]
        public void CreateMethodGetShouldReturnCreateAdView()
        {
            this.dataLoaderMock.Setup(x => x.GetTownsSelectListItem()).Returns(Common.GetTowns().AsQueryable());
            this.dataLoaderMock.Setup(x => x.GetCategoriesSelectListItem())
                .Returns(Common.GetCategories().AsQueryable());

            var result = this.controller.Create() as ViewResult;

            Assert.IsNotNull(result, "Create action returns null.");

            var model = result.Model as AdsCreateViewModel;
            Assert.IsNotNull(model, "The model is null.");

            var towns = Common.GetTowns().Count;
            Assert.AreEqual(towns, model.Towns.Count(), "Different number of towns.");

            var categories = Common.GetCategories().Count;
            Assert.AreEqual(categories, model.Categories.Count(), "Different number of towns.");
        }

        [TestMethod]
        public void CreateMethodPostShouldCreateAd()
        {
            bool isItemAdded = false;
            var adsRepoMock = new Mock<IRepository<Advertisement>>(MockBehavior.Strict);
            adsRepoMock.Setup(x => x.Add(It.IsAny<Advertisement>())).Callback(() => isItemAdded = true);

            this.dataMock.Setup(x => x.Advertisements).Returns(adsRepoMock.Object);

            bool isSaveChangesExecuted = false;
            this.dataMock.Setup(x => x.SaveChanges())
                .Returns(It.IsAny<int>)
                .Callback(() => isSaveChangesExecuted = true);

            var model = new AdsCreateViewModel
            {
                Title = "Ski, ski shoes, ski helmet and ski glass",
                Text = "All things are in a good condition",
                TownId = 1,
                CategotyId = 1,
                //Image = 
            };

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);

            var fakeIdentity = new GenericIdentity("UnitTestUser");
            var principal = new GenericPrincipal(fakeIdentity, null);
            context.SetupGet(x => x.User).Returns(principal);


            this.controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var result = controller.Create(model) as RedirectToRouteResult;

            Assert.AreEqual(true, isItemAdded);
            Assert.AreEqual(true, isSaveChangesExecuted);

            var action = result.RouteValues["Action"];
            var ctrl = result.RouteValues["Controller"];

            Assert.AreEqual("Index", action);
            Assert.AreEqual("Home", ctrl);
        }

        [TestMethod]
        public void CreateMethodPostShouldNotCreateAd()
        {
            var inputModel = new AdsCreateViewModel();

            var validationContext = new ValidationContext(inputModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, validationContext, validationResults, true);
            Common.ValidateViewModel(this.controller, inputModel);
            var result = this.controller.Create(inputModel) as ViewResult;

            Assert.IsNotNull(result, "Create action returns null.");

            var model = result.Model as AdsCreateViewModel;
            Assert.IsNotNull(model, "The model is null.");
            Assert.AreEqual(this.controller.ModelState.Count, validationResults.Count, "Model state errors are not e");
        }
    }
}
