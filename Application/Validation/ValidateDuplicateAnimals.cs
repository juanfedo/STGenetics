using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Validation
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class ValidateDuplicateAnimals : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var order = (List<OrderDetailRequest>)value;

                var duplicateAnimals = order?.GroupBy(c => new { c.AnimalId })
                                       .Where(g => g.Count() > 1)
                                       .ToList();

                if (duplicateAnimals != null && duplicateAnimals.Any())
                {
                    return new ValidationResult("It's not allowed to duplicate the animal in the Order");
                }
                
                return ValidationResult.Success!;
            }

            return new ValidationResult("List null");
        }
    }
}
