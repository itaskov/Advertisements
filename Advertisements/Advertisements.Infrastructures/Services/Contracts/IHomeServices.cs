using System.Linq;
using Advertisements.Web.ViewModels.Home;

namespace Advertisements.Infrastructures.Services.Contracts
{
    public interface IHomeServices
    {
        IQueryable<AdsIndexViewModel> GetAllAds();
    }
}