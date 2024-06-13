using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;

namespace IdentityAPI.Validations;

public class IdentityRegisterValidation : AbstractValidator<IdentityRegisterRequest>
{
    private const string DateTimePattern = "yyyy-MM-dd";
    public IdentityRegisterValidation()
    {
        // Adding validation here

        RuleFor(x => x.Dob)
            .Must(dob => FormatDateTime(dob, DateTimePattern) < FormatDateTime(DateTime.Now, DateTimePattern))
            .WithMessage("Date of birth is not valid");
        RuleFor(x => x.Password)
            // Minimum eight characters, at least one letter, one number and one special
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$")
            .WithMessage("Password must contains at least 8 characters, including at least one special character, one uppercase letter, and on digit");
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Wrong email format");
        RuleFor(x => x.Phone)
            .Matches(@"^(03|05|07|08|09|01[2|6|7|8|9])+([0-9]{8})$")
            .WithMessage("Wrong Vietnamese phone format");
        RuleFor(x => x.Phone)
            .Length(10, 12)
            .WithMessage("Phone length must be 10-12 characters");
    }

    private DateTime FormatDateTime(DateTime datetime, string pattern)
    {
        return DateTime.ParseExact(datetime.ToString(pattern),
            pattern, CultureInfo.InvariantCulture);
    }
}

public static class IdentityRegsiterValidationExtension
{
    public static async Task<ValidationProblemDetails> ValidateAsync(this IdentityRegisterRequest user)
    {
        var validator = new IdentityRegisterValidation();
        var result = await validator.ValidateAsync(user);
        if (!result.IsValid)
        {
            return result.ToProblemDetails();
        }

        return null!;
    }
}