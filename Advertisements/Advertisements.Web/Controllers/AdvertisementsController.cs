using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Advertisements.Common;
using Advertisements.Data;
using Advertisements.Infrastructures.InputModels.Advertisements;
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.Services.Validation;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.Caching;
using Advertisements.Web.Infrastructure.DataLoader;
using AutoMapper;
using Microsoft.AspNet.Identity;

using  AutoMapper.QueryableExtensions;

namespace Advertisements.Web.Controllers
{
    public class AdvertisementsController : BaseController
    {
        private readonly IAdvertisementsService advertisementsService;
        private readonly IAspNetCurrentAppCache cache;

        public AdvertisementsController()
            : base(new AdsData(), new EfDataLoader())
        {
            this.advertisementsService = new AdvertisementsService(new AdsData(), new ModelStateWrapper(this.ModelState));
        }

        public AdvertisementsController(IAdvertisementsService advertisementsService, IAdsData adsData, IDataLoader dataLoader)
            : base(adsData, dataLoader)
        {
            this.advertisementsService = advertisementsService;
        }

        #region Create ad using service layer!
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View(new AdsViewModel());
        //}

        ////[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(AdsViewModel model)
        //{
        //    if (this.advertisementsService.CreateAd(model))
        //    {
        //        return this.RedirectToAction("Index", "Home");
        //    }

        //    return View(model);
        //} 
        #endregion

        public ActionResult Index(RightSideBarViewModel model)
        {
            var currentPage = model.SelectedPage.GetValueOrDefault(1);
            var currentUserId = this.User.Identity.GetUserId();
            var numberOfUserAds = this.Data.Advertisements
                .All()
                .Count(a => a.OwnerId == currentUserId);
            var numberOfPages = (int)Math.Ceiling((double)numberOfUserAds / HomeController.AdsPageSize);

            var viewModel = new IndexViewModel();
            var itemsToSkip = (currentPage - 1) * HomeController.AdsPageSize;
            var adsIndexViewModel = this.Data.Advertisements
                .All()
                .Where(a => a.OwnerId == currentUserId).OrderBy(a => a.Id)
                .Skip(itemsToSkip)
                .Take(HomeController.AdsPageSize);
            viewModel.AdsIndexViewModel = adsIndexViewModel
                .Project()
                .To<AdsIndexViewModel>()
                .ToList();

            viewModel.RightSideBarViewModel = new RightSideBarViewModel
            {
                CategoryId = model.CategoryId,
                TownId = model.TownId,
                Categories = this.DataLoader.GetCategoriesSelectListItem().ToList(),
                Towns = this.DataLoader.GetTownsSelectListItem().ToList(),
                SelectedPage = currentPage,
                NumberOfPages = numberOfPages
            };

            return View("_AllAds", viewModel);
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new AdsCreateViewModel
            {
                Towns = this.DataLoader.GetTownsSelectListItem().ToList(),
                Categories = this.DataLoader.GetCategoriesSelectListItem().ToList()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdsCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var dbAd = Mapper.Map<Advertisement>(model);
                dbAd.Status = AdvertisementStatus.WaitingApproval;
                dbAd.OwnerId = this.User.Identity.GetUserId();
                
                this.Data.Advertisements.Add(dbAd);

                this.SaveImage(model.Image);

                this.Data.SaveChanges();
                
                return this.RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private void SaveImage(HttpPostedFileBase image)
        {
            if (image == null)
            {
                return;
            }

            string fileName = null;
            string fileSavePath = null;
            fileName = Path.GetFileName(image.FileName);
            fileSavePath = Server.MapPath(Constant.VirtualPath + fileName);
            image.SaveAs(fileSavePath); 
        }
    }
}