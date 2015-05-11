﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Advertisements.Data;
using Advertisements.Infrastructures.InputModels.Advertisements;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Models;
using Advertisements.Web.Tests.AllTestsCommon;
using Advertisements.Web.Controllers;
using Advertisements.Web.Infrastructure.DataLoader;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Advertisements.Web.Tests.Controllers
{
    [TestClass]
    public class AdvertisementsControllerTests : BaseControllerTests
    {
        private readonly AdvertisementsController controller;
        
        
        public AdvertisementsControllerTests() 
        {
            this.controller = new AdvertisementsController(this.advertisementsServiceMock.Object, this.dataMock.Object, this.dataLoaderMock.Object);
        }
        
        [TestMethod]
        public void CreateMethodGetShouldReturnCreateAdView()
        {
            this.dataLoaderMock.Setup(x => x.GetTownsSelectListItem()).Returns(Common.GetTowns().AsQueryable());
            
            var result = this.controller.Create() as ViewResult;
            
            Assert.IsNotNull(result, "Create action returns null.");

            var model = result.Model as AdsCreateViewModel;
            Assert.IsNotNull(model, "The model is null.");
            Assert.IsInstanceOfType(model, typeof(AdsCreateViewModel));

            var towns = Common.GetTowns().Count;
            Assert.AreEqual(towns, model.Towns.Count(), "Different number of towns.");
        }

        [TestMethod]
        public void CreateMethodPostShouldCreateAd()
        {
            bool isItemAdded = false;
            var adsRepoMock = new Mock<IRepository<Advertisement>>(MockBehavior.Strict);
            adsRepoMock.Setup(x => x.Add(It.IsAny<Advertisement>())).Callback(() => isItemAdded = true);

            this.dataMock.Setup(x => x.Advertisements).Returns(adsRepoMock.Object);
            
            bool isSaveChangesExecuted = false;
            this.dataMock.Setup(x => x.SaveChanges()).Returns(It.IsAny<int>).Callback(() => isSaveChangesExecuted = true);
            
            var model = new AdsCreateViewModel
            {
                Title = "Ski, ski shoes, ski helmet and ski glass",
                Text = "All things are in a good condition",
                TownId = 1,
                ImageDataURL = "skiing.jpg"
            };

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);

            var fakeIdentity = new GenericIdentity("UnitTestUser");
            var principal = new GenericPrincipal(fakeIdentity, null);
            context.SetupGet(x => x.User).Returns(principal);


            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var result = controller.Create(model) as RedirectToRouteResult;
            
            Assert.AreEqual(true, isItemAdded);
            Assert.AreEqual(true, isSaveChangesExecuted);

            var action = result.RouteValues["Action"];
            var ctrl = result.RouteValues["Controller"];

            Assert.AreEqual("Index", action);
            Assert.AreEqual("Home", ctrl);
        }
    }
}