using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Advertisements.Infrastructures.ViewModels.Home
{
    public class RightSideBarViewModel
    {
        [Display(Name = "Category")]
        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Town")]
        [UIHint("DropDownList")]
        public int TownId { get; set; }

        public IEnumerable<SelectListItem> Towns { get; set; }
    }
}