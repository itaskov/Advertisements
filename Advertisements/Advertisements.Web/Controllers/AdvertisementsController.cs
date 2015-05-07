using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.Services.Validation;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.Caching;
using Advertisements.Web.ViewModels.Home;

namespace Advertisements.Web.Controllers
{
    public class AdvertisementsController : BaseController
    {
        private readonly IAdvertisementsService advertisementsService;
        private readonly IAspNetCurrentAppCache cache;

        public AdvertisementsController()
            : base(new AdsData())
        {
            var service = new AdvertisementsService(new AdsData(), new ModelStateWrapper(this.ModelState));
            this.advertisementsService = service;
        }

        public AdvertisementsController(IAdvertisementsService advertisementsService, IAdsData adsData)
            : base(adsData)
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

        [HttpGet]
        public ActionResult Create()
        {
            return View(new AdsViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdsViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var dbAd = AutoMapper.Mapper.Map<Advertisement>(model);
                dbAd.Status = AdvertisementStatus.WaitingApproval;
                dbAd.OwnerId = this.UserProfile.Id;
                this.Data.Advertisements.Add(dbAd);
                this.Data.SaveChanges();
                
                return this.RedirectToAction("Index", "Home");
            }

            return View(model);
        } 
    }
}