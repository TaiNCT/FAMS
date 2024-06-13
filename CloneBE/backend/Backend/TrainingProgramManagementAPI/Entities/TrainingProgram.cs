using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace TrainingProgramManagementAPI.Entities;

public partial class TrainingProgram
{
    public string TrainingProgramCode { get; set; } = null!;

    public int Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? Days { get; set; }

    public int? Hours { get; set; }

    public TimeOnly StartTime { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? UserId { get; set; }

    public string? TechnicalCodeId { get; set; }

    public string? TechnicalGroupId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual TechnicalCode? TechnicalCode { get; set; }

    public virtual TechnicalGroup? TechnicalGroup { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Syllabus> Syllabi { get; set; } = new List<Syllabus>();


    /// Testing Environment only
    public TrainingProgram()
    {
        #if DEBUG
        if (IsRunningInTestingEnvironment())
        {
            TrainingProgramCode = Guid.NewGuid().ToString();
        }
        #endif
    }

    private bool IsRunningInTestingEnvironment()
    {
        var testingAssemblyName = "TrainingProgramManagementAPI.Tests";
        var callingAssembly = Assembly.GetCallingAssembly();
        var callingAssemblyLocation = callingAssembly.Location;
        return callingAssemblyLocation != null && callingAssemblyLocation.Contains(testingAssemblyName);
    }
}
