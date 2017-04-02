using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Bonbonniere.UnitTests.Controllers
{
    public static class MvcTestExtension
    {
        public static void ValidateModel(this Controller controller, object model)
        {
            //TODO: when using Compare annotation, the result is not expected.
            controller.ModelState.Clear();
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }
    }
}
