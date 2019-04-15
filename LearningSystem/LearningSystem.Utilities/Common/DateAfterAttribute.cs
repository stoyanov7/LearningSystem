namespace LearningSystem.Utilities.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateAfterAttribute : ValidationAttribute
    {
        private const string CurrentDateIsNotValid = "The current date is not valid!";
        private const string OtherDateDoesNotExist = "The other date does not exist or is not valid!";
        private const string CurrentDateExceptionMessage = "The current date is not after the previous date!";

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
                return new ValidationResult(CurrentDateIsNotValid);
            }

            var otherValue = validationContext
                .ObjectType
                .GetProperty(this.otherPropertyName)
                .GetValue(validationContext.ObjectInstance);

            var dateTime = (DateTime?) otherValue;

            if (dateTime == null)
            {
                return new ValidationResult(OtherDateDoesNotExist);
            }

            if (dateTime >= currentDate)
            {
                return new ValidationResult(CurrentDateExceptionMessage);
            }

            return ValidationResult.Success;
        }
    }
}