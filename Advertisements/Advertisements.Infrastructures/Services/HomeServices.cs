using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;
using AutoMapper.QueryableExtensions;

namespace Advertisements.Infrastructures.Services
{
    using System.Linq;
    using Data;
    using Contracts;


    public class HomeServices : BaseServices, IHomeServices
    {
        public HomeServices()
            : this(new AdsData())
        {

        }

        public HomeServices(IAdsData adsData)
            : base(adsData)
        {

        }

        public IQueryable<AdsIndexViewModel> GetAllAds()
        {
            var result = this.AllAds().Project().To<AdsIndexViewModel>();
            return result;
        }

        private IQueryable<Advertisement> AllAds()
        {
            return this.Data.Advertisements.All();
        }

        public IQueryable<AdsIndexViewModel> GetAdsByPageSize(RightSideBarViewModel model)
        {

            var itemsToSkip = (model.SelectedPage.GetValueOrDefault(1) - 1) * model.PageSize;

            return this.AllAds(model)
                .OrderBy(a => a.Id)
                .Skip(itemsToSkip)
                .Take(model.PageSize)
                .Project().To<AdsIndexViewModel>();
        }

        public IQueryable<AdsIndexViewModel> GetAllAds(RightSideBarViewModel model)
        {
            return AllAds(model).Project().To<AdsIndexViewModel>();
        }

        /// <summary>
        /// Get all ads filtered by model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IQueryable<Advertisement> AllAds(RightSideBarViewModel model)
        {
            return this.AllAds()
                .Where(a => !model.CategoryId.HasValue || a.CategoryId == model.CategoryId)
                .Where(a => !model.TownId.HasValue || a.TownId == model.TownId);
        }
    }
}
