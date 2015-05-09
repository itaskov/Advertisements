using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}