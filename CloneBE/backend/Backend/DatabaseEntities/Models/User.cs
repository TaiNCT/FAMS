using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("User")]
[Index("Email", Name = "EmailUnique", IsUnique = true)]
[Index("UserId", Name = "UQ__User__CB9A1CFE03CF03B4", IsUnique = true)]
[Index("Username", Name = "UsernameUnique", IsUnique = true)]
public partial class User
{
    [Required]
    [Column("userId")]
    [StringLength(36)]
    public string UserId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Column("DOB")]
    public DateTime Dob { get; set; }

    [Required]
    [StringLength(255)]
    public string Address { get; set; }

    [Required]
    [StringLength(255)]
    public string Gender { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    [StringLength(255)]
    public string Username { get; set; }

    [Required]
    [StringLength(255)]
    public string Password { get; set; }

    [StringLength(36)]
    public string RoleId { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(36)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(255)]
    public string Avatar { get; set; }

    public bool Status { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ClassUser> ClassUsers { get; set; } = new List<ClassUser>();

    [InverseProperty("Sender")]
    public virtual ICollection<EmailSend> EmailSends { get; set; } = new List<EmailSend>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
