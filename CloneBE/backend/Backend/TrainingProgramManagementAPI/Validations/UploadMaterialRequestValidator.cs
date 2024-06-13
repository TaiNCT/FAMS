namespace TrainingProgramManagementAPI.Validations;

public class UploadMaterialRequestValidator : AbstractValidator<UploadMaterialRequest>
{
    public UploadMaterialRequestValidator()
    {
        // Check valid properties
        RuleFor(x => x.SyllabusId)
            .Must(code => code.GetType() == typeof(Guid))
            .WithMessage("Syllabus Code wrong Unique Type");
        RuleFor(x => x.DayNo)
            .Must(dayNo => int.TryParse(dayNo.ToString(), out dayNo))
            .WithMessage("Day must be a number");
        RuleFor(x => x.UnitNo)
            .Must(unitNo => int.TryParse(unitNo.ToString(), out unitNo))
            .WithMessage("Unit must be a number");
        RuleFor(x => x.ChapterNo)
            .Must(chapterNo => int.TryParse(chapterNo.ToString(), out chapterNo))
            .WithMessage("Unit Chaper must be a number");
        // RuleFor(x => x.File)
        //     .SetValidator(new ImageFileValidator());
    }
}

public static class UploadMaterialRequestValidatorExtension
{
    public static async Task<ValidationProblemDetails> ValidateAsync(this UploadMaterialRequest reqObj)
    {
        var validator = new UploadMaterialRequestValidator();
        var result = await validator.ValidateAsync(reqObj);
        if(!result.IsValid) return result.ToProblemDetails();
        return null!;
    }
}