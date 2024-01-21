using System.ComponentModel.DataAnnotations;

namespace employee_crud.Models.Validations
{
    public class GreaterThanAttribute(int lowerLimit) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not double salary) return new ValidationResult("Salary type must be double");

            return salary >= lowerLimit ? ValidationResult.Success : new ValidationResult($"Salary must be greater than {lowerLimit}");
        }
    }
}
