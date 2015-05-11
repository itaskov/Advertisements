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
        
        public ActionResult Index()
        {
            return View(this.homeServices.GetAllAds().ToList());
        }

        [ChildActionOnly]
        public ActionResult _RightSideBar()
        {
            var model = new RightSideBarViewModel
            {
                Categories = this.DataLoader.GetCategoriesSelectListItem().ToList(),
                Towns = this.DataLoader.GetTownsSelectListItem().ToList()
            };

            return PartialView(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}