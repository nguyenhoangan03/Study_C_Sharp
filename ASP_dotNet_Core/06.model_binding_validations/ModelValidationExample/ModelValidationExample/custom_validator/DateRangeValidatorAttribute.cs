﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelValidationExample.custom_validator {
    public class DateRangeValidatorAttribute : ValidationAttribute {

        public string OtherPropertyName {
            get; set;
        }
        public DateRangeValidatorAttribute(string otherPropertyName) {
            OtherPropertyName = otherPropertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            if (value == null) {
                DateTime from_date = Convert.ToDateTime(value);
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
                DateTime to_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

                if (from_date > to_date) {
                    return new ValidationResult(ErrorMessage, new string[] { OtherPropertyName, validationContext.MemberName });
                } else {
                    return ValidationResult.Success;
                }
            }
            return null;

        }
    }
}
