using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.Caching;

namespace Advertisements.Web.Infrastructure.DataLoader
{
    public class EfDataLoader : IDataLoader
    {
        private readonly IAdsData data;
        private readonly IAspNetCurrentAppCache cache;

        public EfDataLoader()
        {
            this.data = new AdsData();
            this.cache = new AspNetCurrentAppCache();
        }

        public EfDataLoader(IAdsData data, IAspNetCurrentAppCache cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public IQueryable<Town> GetTowns()
        {
            if (this.cache.Get("Towns") != null)
            {
                return ((IEnumerable<Town>)this.cache.Get("Towns")).AsQueryable();
            }
            
            return this.data.Towns.All();
        }

        public IQueryable<SelectListItem> GetTownsSelectListItem()
        {
            var towns = this.GetTowns();
            
            return towns
                .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name,
                        //Selected = (t.Id == 4),
                        //Disabled = (t.Id == 1)
                    });
        }

        public IQueryable<Category> GetCategories()
        {
            if (this.cache.Get("Categories") != null)
            {
                return ((IEnumerable<Category>)this.cache.Get("Categories")).AsQueryable();
            }

            return this.data.Categories.All();
        }

        public IQueryable<SelectListItem> GetCategoriesSelectListItem()
        {
            var categories = this.GetCategories();

            return categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    //Selected = (t.Id == 4),
                    //Disabled = (t.Id == 1)
                });
        }
    }
}