using Advertisements.Infrastructures.ViewModels.Home;

namespace Advertisements.Infrastructures.Services.Contracts
{
    public interface IAdvertisementsService
    {
        bool CreateAd(AdsIndexViewModel model);
    }
}