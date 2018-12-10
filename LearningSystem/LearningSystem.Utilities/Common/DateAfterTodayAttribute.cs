namespace LearningSystem.Utilities.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateAfterTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dateTimeValue = (DateTime) value;

            if (dateTimeValue == null)
            {
                return false;
            }

            return dateTimeValue.Date >= DateTime.Today;
        }
    }
}