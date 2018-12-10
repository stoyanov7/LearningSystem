namespace LearningSystem.Utilities.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateAfterAttribute : ValidationAttribute
    {
        private readonly string otherPropertyName;

        public DateAfterAttribute(string otherPropertyName)
        {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentDate = (DateTime?)value;

            if (currentDate == null)
            {
                return new ValidationResult("The current date is not valid!");
            }

            var otherValue = validationContext
                .ObjectType
                .GetProperty(this.otherPropertyName)
                .GetValue(validationContext.ObjectInstance);

            var dateTime = (DateTime?) otherValue;

            if (dateTime == null)
            {
                return new ValidationResult("The other date does not exist or is not valid!");
            }

            if (dateTime >= currentDate)
            {
                return new ValidationResult("The current date is not after the previous date!");
            }

            return ValidationResult.Success;
        }
    }
}