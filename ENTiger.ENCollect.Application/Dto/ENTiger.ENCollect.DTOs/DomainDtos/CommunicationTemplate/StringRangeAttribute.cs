using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public string AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] allowableValuesArray = AllowableValues?.Split(',') ?? Array.Empty<string>();

            string valueString = value?.ToString().ToLower();
            if (string.IsNullOrEmpty(valueString) || allowableValuesArray.Contains(valueString))
            {
                return ValidationResult.Success;
            }

            string msg = string.IsNullOrEmpty(ErrorMessage) ? "Input is not valid" : ErrorMessage;
            return new ValidationResult(msg);

        }
    }
}