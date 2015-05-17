using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Infrastructures.InputModels.Advertisements;
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.Services.Validation;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.Caching;
using Advertisements.Web.Infrastructure.DataLoader;
using AutoMapper;
using Microsoft.AspNet.Identity;

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

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new AdsCreateViewModel
            {
                Towns = this.DataLoader.GetTownsSelectListItem().ToList()
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
                this.Data.SaveChanges();
                
                return this.RedirectToAction("Index", "Home");
            }

            return View(model);
        } 
    }
}