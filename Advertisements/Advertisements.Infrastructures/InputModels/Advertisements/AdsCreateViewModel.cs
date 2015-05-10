using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertisements.Models;
using TicketSystem.Web.Infrastructure.Mapping;

namespace Advertisements.Infrastructures.InputModels.Advertisements
{
    public class AdsCreateViewModel : IMapFrom<Advertisement>
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Text { get; set; }

        public string ImageDataURL { get; set; }

        [Required]
        [Display(Name = "Town")]
        [UIHint("DropDownList")]
        public int TownId { get; set; }

        public IEnumerable<SelectListItem> Towns { get; set; }
    }
}
