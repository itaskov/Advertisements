﻿using System.Data.Entity.Validation;

namespace Advertisements.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Advertisements.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class AdsData : IAdsData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public AdsData()
            : this(new ApplicationDbContext())
        {
        }

        public AdsData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> ApplicationUsers
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<IdentityRole> UserRoles
        {
            get
            {
                return this.GetRepository<IdentityRole>();
            }
        }

        public IRepository<Advertisement> Advertisements
        {
            get
            {
                return this.GetRepository<Advertisement>();
            }
        }

        public IRepository<Town> Towns
        {
            get
            {
                return this.GetRepository<Town>();
            }
        }


        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public int SaveChanges()
        {
            try
            {
                return this.context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var ex = e;
                throw;
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
