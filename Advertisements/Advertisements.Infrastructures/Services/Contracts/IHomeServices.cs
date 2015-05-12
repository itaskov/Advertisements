using System.Linq;
using Advertisements.Infrastructures.ViewModels.Home;

namespace Advertisements.Infrastructures.Services.Contracts
{
    public interface IHomeServices
    {
        IQueryable<AdsIndexViewModel> GetAllAds();

        IQueryable<AdsIndexViewModel> GetAllAds(RightSideBarViewModel model);
    }
}