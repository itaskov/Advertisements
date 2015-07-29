using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Advertisements.Infrastructures.ViewModels.Home
{
    public class RightSideBarViewModel
    {
        [Display(Name = "Category")]
        [UIHint("_RightSidebarDropDownList")]
        public int? CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Town")]
        [UIHint("_RightSidebarDropDownList")]
        public int? TownId { get; set; }

        public IEnumerable<SelectListItem> Towns { get; set; }

        public int? SelectedPage { get; set; }
        public int NumberOfPages { get; set; }

        public int PageSize { get; set; }
    }
}