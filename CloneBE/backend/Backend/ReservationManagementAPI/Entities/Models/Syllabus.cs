using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Syllabus
{
    public string SyllabusId { get; set; }

    public int Id { get; set; }

    public string TopicCode { get; set; }

    public string TopicName { get; set; }

    public string Version { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? AttendeeNumber { get; set; }

    public string Level { get; set; }

    public string TechnicalRequirement { get; set; }

    public string CourseObjective { get; set; }

    public byte[] DeliveryPrinciple { get; set; }

    public int? Days { get; set; }

    public double? Hours { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<AssessmentScheme> AssessmentSchemes { get; set; } = new List<AssessmentScheme>();

    public virtual ICollection<SyllabusDay> SyllabusDays { get; set; } = new List<SyllabusDay>();

    public virtual ICollection<TrainingProgramSyllabus> TrainingProgramSyllabi { get; set; } = new List<TrainingProgramSyllabus>();
}
