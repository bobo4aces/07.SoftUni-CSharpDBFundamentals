using System;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {
        private string xorTargetAttribute;

        public XorAttribute(string xorTargetAttribute)
        {
            this.xorTargetAttribute = xorTargetAttribute;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object targetAttribute = validationContext.ObjectType
                .GetProperty(xorTargetAttribute)
                .GetValue(validationContext.ObjectInstance);
            if ((targetAttribute == null) && (value != null) ||
                (targetAttribute != null) && (value == null))
            {
                return ValidationResult.Success;
            }
            string errorMessage = "The two properties must have opposite values!";
            return new ValidationResult(errorMessage);
        }
    }
}
