using System.Linq;
using Advertisements.Models;

namespace Advertisements.Web.Infrastructure.DataLoader
{
    public interface IDataLoader
    {
        IQueryable<Town> GetTowns();
    }
}