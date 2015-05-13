using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Web.Infrastructure.DataLoader;

namespace Advertisements.Web.Controllers
{
    public class HomeController : BaseController
    {
        public const int AdsPageSize = 5;
        
        private readonly IHomeServices homeServices;
        
        public HomeController(IHomeServices homeServices) : 
            base(new AdsData(), new EfDataLoader())
        {
            this.homeServices = homeServices;
        }

        public HomeController(IAdsData data, IDataLoader dataLoader) :
            base(data, dataLoader)
        {
        }
        
        public ActionResult Index(RightSideBarViewModel model)
        {
            IEnumerable<AdsIndexViewModel> ads = null;
            if (this.Request.IsAjaxRequest())
            {
                ads = this.homeServices.GetAllAds(model).ToList();
                return PartialView("_Advertisement", ads);
            }

            ads = this.homeServices.GetAllAds(model).ToList();
            var viewModel = new IndexViewModel
            {
                AdsIndexViewModel = ads,
                RightSideBarViewModel = new RightSideBarViewModel
                {
                    Categories = this.DataLoader.GetCategoriesSelectListItem().ToList(),
                    Towns = this.DataLoader.GetTownsSelectListItem().ToList()
                }
            };
            
            return View(viewModel);
        }
    }
}