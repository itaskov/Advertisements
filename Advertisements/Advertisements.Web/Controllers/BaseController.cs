using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.DataLoader;

namespace Advertisements.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IAdsData Data { get; private set; }
        
        //protected ApplicationUser UserProfile;

        public IDataLoader DataLoader;

        protected BaseController(IAdsData data, IDataLoader dataLoader)
        {
            this.Data = data;
            this.DataLoader = dataLoader;
        }

        //protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        //{
        //    this.UserProfile = this.Data.Users.All().FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            
        //    return base.BeginExecute(requestContext, callback, state);
        //}
    }
}