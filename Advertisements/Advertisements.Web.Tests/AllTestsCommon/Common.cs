using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Advertisements.Web.Tests.AllTestsCommon
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
    }
}
