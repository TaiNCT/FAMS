using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? RoleId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Avatar { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<ClassUser> ClassUsers { get; set; } = new List<ClassUser>();

    public virtual ICollection<EmailSend> EmailSends { get; set; } = new List<EmailSend>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
