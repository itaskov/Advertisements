using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertisements.Models;

namespace Advertisements.Tests.Common
{
    public static class Common
    {
        public static List<SelectListItem> GetTowns()
        {
            var towns = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Sofia"},
                new SelectListItem {Value = "2", Text = "Blagoevgrad"},
                new SelectListItem {Value = "3", Text = "Plovdiv"},
                new SelectListItem {Value = "4", Text = "Varna"},
            };
            return towns;
        }

        public static List<SelectListItem> GetCategories()
        {
            var categories = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Sport"},
                new SelectListItem {Value = "2", Text = "Cars"},
                new SelectListItem {Value = "3", Text = "Electronics"},
                new SelectListItem {Value = "4", Text = "Jobs"},
                new SelectListItem {Value = "5", Text = "Properties"},
            };
            return categories;
        }

        public static void ValidateViewModel<TViewModel, TController>(this TController controller, TViewModel viewModelToValidate)
        where TController : Controller
        {
            var validationContext = new ValidationContext(viewModelToValidate, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModelToValidate, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }
        }

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
                        PhoneNumber = "+359888476924",
                        Id = "16267522-28e1-498d-a3a4-cfea9608531b"
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
                        PhoneNumber = "+359888476924",
                        Id = "16267522-28e1-498d-a3a4-cfea9608531b"
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
                        PhoneNumber = "+359888476924",
                        Id = "16267522-28e1-498d-a3a4-cfea9608531b"
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
                        PhoneNumber = "+359888476924",
                        Id = "16267522-28e1-498d-a3a4-cfea9608531b"
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
                        PhoneNumber = "+359888476924",
                        Id = "16267522-28e1-498d-a3a4-cfea9608531b"
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
                        PhoneNumber = "+359888476924",
                        Id = "16267522-28e1-498d-a3a4-cfea9608531b"
                    }
                },
            };

            return ads;
        }
    }
}
