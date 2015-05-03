using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Advertisements.Models;
using AutoMapper;
using TicketSystem.Web.Infrastructure.Mapping;

namespace Advertisements.Web.ViewModels.Home
{
    public class AdsViewModel : IMapFrom<Advertisement>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageDataURL { get; set; }

        public string OwnerName { get; set; }

        public string OwnerEmail { get; set; }

        public string OwnerPhone { get; set; }

        public DateTime Date { get; set; }

        #region IHaveCustomMappings Members

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Advertisement, AdsViewModel>()
                .ForMember(d => d.OwnerName, opt => opt.MapFrom(s => s.Owner.Name))
                .ForMember(d => d.OwnerEmail, opt => opt.MapFrom(s => s.Owner.Email))
                .ForMember(d => d.OwnerPhone, opt => opt.MapFrom(s => s.Owner.PhoneNumber))
                .ReverseMap();
        }

        #endregion
    }
}