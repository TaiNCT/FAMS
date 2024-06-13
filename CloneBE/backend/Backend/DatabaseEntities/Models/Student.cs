using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Student")]
[Index("StudentId", Name = "UQ__Student__4D11D63D5926E8B3", IsUnique = true)]
public partial class Student
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string MutatableStudentID { get; set; }

    public Student()
    {
        // Generate the StudentId when creating a new instance
        MutatableStudentID = "STD" + GenerateRandomDigits(5);
    }

    // Method to generate random digits
    private string GenerateRandomDigits(int length)
    {
        Random random = new Random();
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += random.Next(10).ToString();
        }
        return result;
    }

    [Required]
    [Column("studentId")]
    [StringLength(36)]
    public string StudentId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Column("DOB")]
    public DateTime Dob { get; set; }

    public int Gender { get; set; }

    public double Mock { get; set; }
    public string Phone { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(36)]
    public string MajorId { get; set; }

    public DateTime GraduatedDate { get; set; }

    [Column("GPA", TypeName = "double")]
    public double Gpa { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [Column("FAAccount")]
    public string Faaccount { get; set; }

    public int Type { get; set; }

    public bool Status { get; set; }

    public DateTime JoinedDate { get; set; }

    [Required]
    public string Area { get; set; }

    [Column("RECer")]
    [StringLength(100)]
    public string Recer { get; set; }

    public string University { get; set; }


    public bool CertificationStatus { get; set; } // Not yet = False, Done = true

    public DateTime? CertificationDate { get; set; }

    public int Audit { get; set; }


    [InverseProperty("Receiver")]
    public virtual ICollection<EmailSendStudent> EmailSendStudents { get; set; } = new List<EmailSendStudent>();

    [ForeignKey("MajorId")]
    [InverseProperty("Students")]
    public virtual Major Major { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<QuizStudent> QuizStudents { get; set; } = new List<QuizStudent>();

    [InverseProperty("Student")]
    public virtual ICollection<ReservedClass> ReservedClasses { get; set; } = new List<ReservedClass>();

    [InverseProperty("Student")]
    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();
}
