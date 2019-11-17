using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Louis.Models
{
    public class PriceValidationAttribute: ValidationAttribute
    {
    
        private readonly string _other;
        public PriceValidationAttribute(string other)
        {
            _other = other;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_other);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _other)
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance);

            bool isConfirmed = (bool)otherValue;
            decimal price = (decimal)value;
            if ( price > 999 && !isConfirmed )
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}
