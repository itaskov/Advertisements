using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Infrastructures.Services.Contracts;

namespace Advertisements.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeServices homeServices;
        
        public HomeController(IHomeServices homeServices)
        {
            this.homeServices = homeServices;
        }
        
        public ActionResult Index()
        {
            return View(this.homeServices.GetAllAds().ToList());
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