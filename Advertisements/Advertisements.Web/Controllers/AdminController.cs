using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Advertisements.Data;
using Advertisements.Web.Infrastructure.DataLoader;

namespace Advertisements.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : AuthorizeController
    {
        public AdminController(IAdsData data, IDataLoader dataLoader)
            : base(data, dataLoader)
        {
        }
    }
}