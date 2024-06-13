using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class User
{
    public string UserId { get; set; }

    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public DateTime Dob { get; set; }

    public string Address { get; set; }

    public string Gender { get; set; }

    public string Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string RoleId { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Avatar { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<ClassUser> ClassUsers { get; set; } = new List<ClassUser>();

    public virtual ICollection<EmailSend> EmailSends { get; set; } = new List<EmailSend>();

    public virtual Role Role { get; set; }

    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
