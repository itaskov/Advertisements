using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Advertisements.Data;
using Advertisements.Web.Controllers;
using Advertisements.Web.Infrastructure.DataLoader;
using Microsoft.AspNet.Identity;

namespace Advertisements.Web.Controllers
{
    [Authorize]
    public class AuthorizationController : BaseController
    {
        protected string CurrentUserId { get; private set; }

        public AuthorizationController(IAdsData data, IDataLoader dataLoader)
            : base(data, dataLoader)
        {
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            this.CurrentUserId = this.User.Identity.GetUserId();
            
            base.OnAuthentication(filterContext);
        }
    }
}