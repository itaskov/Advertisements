using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Models;

namespace Advertisements.Web.Infrastructure.DataLoader
{
    public class EfDataLoader : IDataLoader
    {
        private readonly IAdsData data;

        public EfDataLoader()
        {
            this.data = new AdsData();
        }

        public EfDataLoader(IAdsData data)
        {
            this.data = data;
        }

        public IQueryable<Town> GetTowns()
        {
            return this.data.Towns.All();
        }

        public IQueryable<SelectListItem> GetTownsSelectListItem()
        {
            return this.data.Towns.All()
                .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name,
                        //Selected = (t.Id == 4),
                        //Disabled = (t.Id == 1)
                    });
        }
    }
}