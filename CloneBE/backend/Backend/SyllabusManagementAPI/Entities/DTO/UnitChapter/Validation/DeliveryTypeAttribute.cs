using System.ComponentModel.DataAnnotations;
using SyllabusManagementAPI.Contracts;

namespace SyllabusManagementAPI.Entities.DTO.UnitChapter.Validation
{
    public class DeliveryTypeAttribute : ValidationAttribute
    {
        public DeliveryTypeAttribute() { }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage);

            var selection = value as string;
            if (selection == null)
                return new ValidationResult(ErrorMessage);

            var repository = (IRepositoryWrapper)validationContext.GetService(typeof(IRepositoryWrapper));

            // Get the list of valid options from the database
            var validOptions = repository.DeliveryType.GetAllDeliveryTypeAsync().Result;

            // Check if the selected option exists in the list
            var isValid = validOptions.Exists(option => option.DeliveryTypeId == selection);
            if (!isValid)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}