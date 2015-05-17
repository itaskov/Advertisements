using System;
using System.Collections.Generic;
using Advertisements.Infrastructures.InputModels.Advertisements;
using Advertisements.Models;

namespace Advertisements.Infrastructures.Tests.AllTestsCommon
{
    public static class Common
    {
        public static IEnumerable<Advertisement> GetAds()
        {
            var ads = new List<Advertisement>
            {
               new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 2,
                    CategoryId = 2,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
                new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 3,
                    CategoryId = 3,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
                new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 1,
                    CategoryId = 1,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
                new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 1,
                    CategoryId = 1,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
                new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 1,
                    CategoryId = 1,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
                new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 1,
                    CategoryId = 1,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
                new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 1,
                    CategoryId = 1,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
                new Advertisement
                {
                    Title = "Ski, ski shoes, ski helmet and ski glass",
                    Text = "All things are in a good condition",
                    TownId = 1,
                    CategoryId = 1,
                    ImageDataURL = "skiing.jpg",
                    DateCreated = DateTime.Now,
                    Owner = new ApplicationUser
                    {
                        Name = "Ivan",
                        Email = "itaskov@gmail.com",
                        PhoneNumber = "+359888476924"
                    }
                },
            };

            return ads;
        }
    }
}
