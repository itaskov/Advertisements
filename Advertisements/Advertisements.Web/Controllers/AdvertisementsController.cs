using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Advertisements.Common;
using Advertisements.Data;
using Advertisements.Infrastructures.InputModels.Advertisements;
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.Services.Validation;
using Advertisements.Infrastructures.ViewModels.Advertisements;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.Caching;
using Advertisements.Web.Infrastructure.DataLoader;
using AutoMapper;
using Microsoft.AspNet.Identity;

using AutoMapper.QueryableExtensions;
using Microsoft.Owin.Security;

namespace Advertisements.Web.Controllers
{
    public class AdvertisementsController : AuthorizationController
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

        public AdvertisementsController(IAdsData adsData)
            : base(adsData, new EfDataLoader())
        {

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
            var adsPerPage = model.PageSize == 0 ? HomeController.AdsPageSize : model.PageSize;

            var adsIndexViewModel = this.Data.Advertisements
                .All()
                .Where(a => a.Owner.Id == this.CurrentUserId);
                
            var numberOfUserAds = adsIndexViewModel.Count();
           
            var numberOfPages = (int)Math.Ceiling((double)numberOfUserAds / adsPerPage);

            var viewModel = new IndexViewModel();
            var itemsToSkip = (currentPage - 1) * adsPerPage;
            
            adsIndexViewModel = adsIndexViewModel
                .OrderBy(a => a.Id)
                .Skip(itemsToSkip)
                .Take(adsPerPage);
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

            ViewBag.Title = "Ads - My Ads";

            return View(viewModel);
        }

        public ActionResult Pagination(RightSideBarViewModel model, AdvertisementStatus? status)
        {
            var currentPage = model.SelectedPage.GetValueOrDefault(1);
            var adsPerPage = model.PageSize == 0 ? HomeController.AdsPageSize : model.PageSize;

            var adsIndexViewModel = this.Data.Advertisements
                .All()
                .Where(a => a.Owner.Id == this.CurrentUserId)
                .Where(a => status == null || a.Status == status);

            var numberOfUserAds = adsIndexViewModel.Count();

            var numberOfPages = (int)Math.Ceiling((double)numberOfUserAds / adsPerPage);

            var viewModel = new IndexViewModel();
            var itemsToSkip = (currentPage - 1) * adsPerPage;

            adsIndexViewModel = adsIndexViewModel
                .OrderBy(a => a.Id)
                .Skip(itemsToSkip)
                .Take(adsPerPage);
            viewModel.AdsIndexViewModel = adsIndexViewModel
                .Project()
                .To<AdsIndexViewModel>()
                .ToList();

            viewModel.RightSideBarViewModel = new RightSideBarViewModel
            {
                SelectedPage = currentPage,
                NumberOfPages = numberOfPages
            };

            ViewBag.Title = "Ads - My Ads";

            return View("Index", viewModel);
        }

        public ActionResult DeactivateAd(int id)
        {
            var ad = this.Data.Advertisements.GetById(id);
            if (ad == null)
            {
                return HttpNotFound();
            }

            ad.Status = AdvertisementStatus.Inactive;
            this.Data.Advertisements.Update(ad);
            this.Data.SaveChanges();

            return Content(ad.Status.ToString());
        }

        //public ActionResult MyAds(AdvertisementStatus? status)
        //{
        //    var myAds = this.Data.Advertisements
        //        .All()
        //        .Where(a => a.Owner.Id == this.CurrentUserId)
        //        .Where(a => status == null || a.Status == status);

        //    var viewModel = new MyAdViewModel();
        //    //viewModel.MyAds = myAds
        //    //    .Project()
        //    //    .To<MyAdViewModel>()
        //    //    .ToList();

        //    viewModel.MyAds = myAds.Select(MyAdViewModel.FromAdvertisement).ToList();
        //    ViewBag.Title = "Ads - My Ads";

        //    return View(viewModel);
        //}

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AdsCreateViewModel
            {
                Towns = this.DataLoader.GetTownsSelectListItem().ToList(),
                Categories = this.DataLoader.GetCategoriesSelectListItem().ToList()
            };

            return View(model);
        }

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

                return this.RedirectToAction("Index");
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

        // GET: UserProfile
        public ActionResult IndexUserProfile()
        {
            var applicationUsers = Data.ApplicationUsers.All().Include(a => a.Town).Where(a => a.Id == this.CurrentUserId);
            return View(applicationUsers.ToList());
        }

        // GET: UserProfile/Edit/5
        public ActionResult EditUserProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = this.Data.ApplicationUsers.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.TownId = new SelectList(this.Data.Towns.All(), "Id", "Name", applicationUser.TownId);
            return View(applicationUser);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile([Bind(Include = "Id,Name,TownId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (this.CurrentUserId != applicationUser.Id)
            {
                this.ModelState.AddModelError("Id", "Unexpected user id!");
            }

            if (ModelState.IsValid)
            {
                this.Data.ApplicationUsers.Update(applicationUser);
                this.Data.SaveChanges();
                return RedirectToAction("IndexUserProfile");
            }
            ViewBag.TownId = new SelectList(this.Data.Towns.All(), "Id", "Name", applicationUser.TownId);
            return View(applicationUser);
        }
    }
}