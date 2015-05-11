using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Advertisements.Web.Tests.AllTestsCommon
{
    public static class Common
    {

        public static List<SelectListItem> GetTowns()
        {
            var towns = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Sofia"},
                new SelectListItem {Value = "2", Text = "Blagoevgrad"},
                new SelectListItem {Value = "3", Text = "Plovdiv"},
                new SelectListItem {Value = "4", Text = "Varna"},
            };
            return towns;
        }
    }
}
