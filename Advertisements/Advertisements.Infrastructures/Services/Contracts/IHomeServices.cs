using System.Linq;
using Advertisements.Infrastructures.ViewModels.Home;
using Advertisements.Models;

namespace Advertisements.Infrastructures.Services.Contracts
{
    public interface IHomeServices
    {
        IQueryable<AdsIndexViewModel> GetAllAds();

        IQueryable<AdsIndexViewModel> GetAdsByPageSize(RightSideBarViewModel model);

        IQueryable<AdsIndexViewModel> GetAllAds(RightSideBarViewModel model);
    }
}