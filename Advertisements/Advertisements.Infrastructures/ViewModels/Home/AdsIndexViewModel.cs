using System;
using System.ComponentModel.DataAnnotations;
using Advertisements.Models;
using AutoMapper;
using TicketSystem.Web.Infrastructure.Mapping;

namespace Advertisements.Infrastructures.ViewModels.Home
{
    public class AdsIndexViewModel : IMapFrom<Advertisement>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageDataURL { get; set; }

        public string OwnerName { get; set; }

        public string OwnerEmail { get; set; }

        public string OwnerPhone { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        #region IHaveCustomMappings Members

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Advertisement, AdsIndexViewModel>()
                .ForMember(d => d.OwnerName, opt => opt.MapFrom(s => s.Owner.Name))
                .ForMember(d => d.OwnerEmail, opt => opt.MapFrom(s => s.Owner.Email))
                .ForMember(d => d.OwnerPhone, opt => opt.MapFrom(s => s.Owner.PhoneNumber));
            //.ReverseMap();
        }

        #endregion
    }
}