using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrainingProgramManagementAPI.Extensions;

namespace UserManagementAPI.Models.DTO.ValidationDTO;

public class RecoverPasswordValidationDTO : AbstractValidator<RecoverPasswordDTO>
{
    //The validation rules themselves should be defined in the validator class’s constructor.
    public RecoverPasswordValidationDTO()
    {
        //To specify a validation rule for a particular property, call the RuleFor

        // Password 
        RuleFor(x => x.Password)
            // Is available
            .NotNull()
            // Minimum eight characters, at least one letter, one number and one special
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$")
            .WithMessage("Password must contains at least 8 characters, including at least one special character, one uppercase letter, and on digit");

        // Confirm Password
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Password comfirm not match");

        // Email
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email wrong format");
    }
}

public static class RecoverPasswordValidationDTOExtension
{
    public static async Task<ValidationProblemDetails> ValidateAsync(this RecoverPasswordDTO recoverPasswordDTO)
    {
        // Initiate Obj Validaotr
        var validator = new RecoverPasswordValidationDTO();
        // Validate properties
        var result = await validator.ValidateAsync(recoverPasswordDTO);
        // Is not valid
        if (!result.IsValid)
        {
            // Này nó gọi class extension bên folder extension á
            return result.ToProblemDetails();
        }
        
        // Is valid
        return null!;
    }
}