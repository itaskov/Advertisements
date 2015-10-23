using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Infrastructures.Services.Validation;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;

namespace Advertisements.Infrastructures.Services
{
    public class AdvertisementsService : BaseServices, IAdvertisementsService
    {
        private readonly IValidationDictionary validationDictionary;

        public AdvertisementsService()
            : this(new AdsData(), new ModelStateWrapper(new ModelStateDictionary()))
        {
            
        }
        
        public AdvertisementsService(IAdsData adsData, IValidationDictionary validationDictionary)
            : base(adsData)
        {
            this.validationDictionary = validationDictionary;
        }

        public bool CreateAd(AdsIndexViewModel model)
        {
            var result = false;

            if (this.validationDictionary.IsValid)
            {
                var dbAd = AutoMapper.Mapper.Map<Advertisement>(model);
                dbAd.Status = AdvertisementStatus.WaitingApproval;
                dbAd.OwnerId = this.Data.ApplicationUsers.All().Select(u => u.Id).FirstOrDefault();
                this.Data.Advertisements.Add(dbAd);
                this.Data.SaveChanges();

                result = true;
            }

            return result;
        }
    }
}
