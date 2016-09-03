using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Validations
{
    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please provide FirstName");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("FirstName should not contain @");
                }
            }

            return ValidationResult.Success;
            //return base.IsValid(value, validationContext);
        }
    }
}