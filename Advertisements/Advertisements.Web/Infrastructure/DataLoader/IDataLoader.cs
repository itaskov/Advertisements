using System.Linq;
using System.Web.Mvc;
using Advertisements.Models;

namespace Advertisements.Web.Infrastructure.DataLoader
{
    public interface IDataLoader
    {
        IQueryable<Town> GetTowns();
        IQueryable<SelectListItem> GetTownsSelectListItem();

        IQueryable<Category> GetCategories();
        IQueryable<SelectListItem> GetCategoriesSelectListItem();
    }
}