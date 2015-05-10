using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Web.Controllers;
using Advertisements.Web.Infrastructure.DataLoader;

namespace Advertisements.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
        public AdminController(IAdsData data, IDataLoader dataLoader)
            : base(data, dataLoader)
        {
        }
    }
}