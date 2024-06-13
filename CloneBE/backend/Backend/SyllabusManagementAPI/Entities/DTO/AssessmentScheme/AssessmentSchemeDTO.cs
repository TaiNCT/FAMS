using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public partial class AssessmentSchemeDTO
{
    public string AssesmentSchemeId { get; set; } = null!;

    public int Id { get; set; }

    public double? Assignment { get; set; }

    public double? FinalPractice { get; set; }

    public double? Final { get; set; }

    public double? FinalTheory { get; set; }

    public double? Gpa { get; set; }

    public double? Quiz { get; set; }

    public string? SyllabusId { get; set; }

    public virtual Syllabus? Syllabus { get; set; }
}
