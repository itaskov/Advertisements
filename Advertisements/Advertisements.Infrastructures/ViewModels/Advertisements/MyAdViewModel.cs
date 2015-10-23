using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertisements.Common;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;
using AutoMapper;
using TicketSystem.Web.Infrastructure.Mapping;

namespace Advertisements.Infrastructures.ViewModels.Advertisements
{
    public class MyAdViewModel : IMapFrom<Advertisement>, IHaveCustomMappings
    {
        public static Expression<Func<Advertisement, MyAdViewModel>> FromAdvertisement
        {
            get
            {
                return advertisement => new MyAdViewModel
                {
                    Title = advertisement.Title,
                    Text = advertisement.Text,
                    ImageDataURL = advertisement.ImageDataURL,
                    DateCreated = advertisement.DateCreated,
                    Category = advertisement.Category.Name,
                    Town = advertisement.Town.Name
                };
            }
        }
        
        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageDataURL { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public string Category { get; set; }

        public string Town { get; set; }

        public IList<MyAdViewModel> MyAds { get; set; }

        #region IHaveCustomMappings Members

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Advertisement, MyAdViewModel>()
                .ForMember(d => d.ImageDataURL, opt => opt.MapFrom(s => s.ImageDataURL == null || s.ImageDataURL.StartsWith("data:") ? s.ImageDataURL : Constant.VirtualPath + s.ImageDataURL));
            //.ReverseMap();
        }

        #endregion

    }
}
