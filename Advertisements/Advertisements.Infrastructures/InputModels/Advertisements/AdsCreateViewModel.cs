using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Advertisements.Models;
using AutoMapper;
using TicketSystem.Web.Infrastructure.Mapping;

namespace Advertisements.Infrastructures.InputModels.Advertisements
{
    public class AdsCreateViewModel : IHaveCustomMappings
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        //[Display(Name = "Image")]
        //public string ImageDataURL { get; set; }

        [Required]
        [Display(Name = "Town")]
        [UIHint("DropDownList")]
        public int TownId { get; set; }

        public IEnumerable<SelectListItem> Towns { get; set; }

        [Required]
        [Display(Name = "Category")]
        [UIHint("DropDownList")]
        public int CategotyId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }

        #region IHaveCustomMappings Members

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AdsCreateViewModel, Advertisement>()
                .ForMember(d => d.ImageDataURL, opt => opt.MapFrom(s => s.Image.FileName));
        }

        #endregion
    }
}
