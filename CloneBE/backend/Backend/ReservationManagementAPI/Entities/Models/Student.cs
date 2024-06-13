using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Student
{
    public string StudentId { get; set; }

    public int Id { get; set; }

    public string FullName { get; set; }

    public DateTime Dob { get; set; }

    public string Gender { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string MajorId { get; set; }

    public DateTime GraduatedDate { get; set; }

    public decimal Gpa { get; set; }

    public string Address { get; set; }

    public string Faaccount { get; set; }

    public int Type { get; set; }

    public string Status { get; set; }

    public DateTime JoinedDate { get; set; }

    public string Area { get; set; }

    public string Recer { get; set; }

    public string University { get; set; }

    public int? Audit { get; set; }

    public int? Mock { get; set; }

    public virtual ICollection<EmailSendStudent> EmailSendStudents { get; set; } = new List<EmailSendStudent>();

    public virtual Major Major { get; set; }

    public virtual ICollection<QuizStudent> QuizStudents { get; set; } = new List<QuizStudent>();

    public virtual ICollection<ReservedClass> ReservedClasses { get; set; } = new List<ReservedClass>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();
}
