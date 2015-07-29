using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Infrastructures.Services;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Web.Infrastructure.DataLoader;
using AutoMapper.QueryableExtensions;

namespace Advertisements.Web.Controllers
{
    public class HomeController : BaseController
    {
        public const int AdsPageSize = 5;
        
        private readonly IHomeServices homeServices;
        
        public HomeController() : 
            base(new AdsData(), new EfDataLoader())
        {
            this.homeServices = new HomeServices();
        }

        public HomeController(IHomeServices homeServices, IAdsData data, IDataLoader dataLoader) :
            base(data, dataLoader)
        {
            this.homeServices = homeServices;
        }
        
        public ActionResult Index(RightSideBarViewModel model)
        {
            model.PageSize = AdsPageSize;
            var ads = this.homeServices.GetAdsByPageSize(model).ToList();
            
            var viewModel = new IndexViewModel
            {
                AdsIndexViewModel = ads,
                RightSideBarViewModel = new RightSideBarViewModel
                {
                    Categories = this.DataLoader.GetCategoriesSelectListItem().ToList(),
                    Towns = this.DataLoader.GetTownsSelectListItem().ToList(),
                    NumberOfPages = (int)Math.Ceiling((double)this.homeServices.GetAllAds(model).Count() / AdsPageSize),
                    SelectedPage = model.SelectedPage.GetValueOrDefault(1),
                    CategoryId = model.CategoryId,
                    TownId = model.TownId
                }
            };
            
            return View("_AllAds", viewModel);
        }
    }
}