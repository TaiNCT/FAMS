using System.Globalization;

namespace TrainingProgramManagementAPI.Validations
{
    public class TrainingProgramDtoValidator : AbstractValidator<TrainingProgramDto>
    {
        public TrainingProgramDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Hours)
                .Must(x => x.HasValue && int.TryParse(x.ToString(), out var hours))
                .WithMessage("Total hours must be a number");
            RuleFor(x => x.Days)
                .Must(x => x.HasValue && int.TryParse(x.ToString(), out var days))
                .WithMessage("Total days must be a number");
            // RuleFor(x => x.StartTime)
            //     .Must(x => DateTime.ParseExact(x.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture) > DateTime.Now)
            //     .WithMessage("Start time must greater than  today");
        }
    }

    public static class TrainingProgramDtoValidatorExtension
    {
        public async static Task<ValidationResult?> ValidateAsync(this TrainingProgramDto trainingProgram)
        {
            var validator = new TrainingProgramDtoValidator();
            var result = await validator.ValidateAsync(trainingProgram);
            return !result.IsValid ? result : null;
        }
    }
}