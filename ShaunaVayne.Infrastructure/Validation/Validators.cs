using System.ComponentModel.DataAnnotations;

namespace ShaunaVayne.Infrastructure.Validation
{
    public class GreaterThanAttribute : ValidationAttribute
    {
        public GreaterThanAttribute(int number)
        {
            Number = number;
        }

        public int Number { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.TryParse(value.ToString(), out int i))
            {
                if (i <= Number)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
