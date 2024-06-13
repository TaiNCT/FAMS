using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime Dob { get; set; }

    public int Gender { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string MajorId { get; set; } = null!;

    public DateTime GraduatedDate { get; set; }

    public decimal Gpa { get; set; }

    public string Address { get; set; } = null!;

    public string Faaccount { get; set; } = null!;

    public int Type { get; set; }

    public bool Status { get; set; }

    public DateTime JoinedDate { get; set; }

    public string Area { get; set; } = null!;

    public string? Recer { get; set; }

    public string? University { get; set; }

    public virtual ICollection<EmailSendStudent> EmailSendStudents { get; set; } = new List<EmailSendStudent>();

    public virtual Major Major { get; set; } = null!;

    public virtual ICollection<QuizStudent> QuizStudents { get; set; } = new List<QuizStudent>();

    public virtual ICollection<ReservedClass> ReservedClasses { get; set; } = new List<ReservedClass>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();
}
