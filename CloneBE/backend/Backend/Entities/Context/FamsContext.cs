using System;
using System.Collections.Generic;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Context;

public partial class FamsContext : DbContext
{
    public FamsContext()
    {
    }

    public FamsContext(DbContextOptions<FamsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AssessmentScheme> AssessmentSchemes { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AttendeeType> AttendeeTypes { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassTrainerLocation> ClassTrainerLocations { get; set; }

    public virtual DbSet<ClassUser> ClassUsers { get; set; }

    public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }

    public virtual DbSet<EmailSend> EmailSends { get; set; }

    public virtual DbSet<EmailSendStudent> EmailSendStudents { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Fsu> Fsus { get; set; }

    public virtual DbSet<InboxState> InboxStates { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<OutboxMessage> OutboxMessages { get; set; }

    public virtual DbSet<OutboxState> OutboxStates { get; set; }

    public virtual DbSet<OutputStandard> OutputStandards { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizStudent> QuizStudents { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<ReservedClass> ReservedClasses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    public virtual DbSet<StudentModule> StudentModules { get; set; }

    public virtual DbSet<Syllabus> Syllabi { get; set; }

    public virtual DbSet<SyllabusClass> SyllabusClasses { get; set; }

    public virtual DbSet<SyllabusDay> SyllabusDays { get; set; }

    public virtual DbSet<SyllabusUnit> SyllabusUnits { get; set; }

    public virtual DbSet<TechnicalCode> TechnicalCodes { get; set; }

    public virtual DbSet<TechnicalGroup> TechnicalGroups { get; set; }

    public virtual DbSet<TrainingMaterial> TrainingMaterials { get; set; }

    public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }

    public virtual DbSet<TrainingProgramModule> TrainingProgramModules { get; set; }

    public virtual DbSet<TrainingProgramSyllabus> TrainingProgramSyllabi { get; set; }

    public virtual DbSet<UnitChapter> UnitChapters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:famsdatabase.database.windows.net,1433;Initial Catalog=FamsDB;Persist Security Info=False;User ID=fams;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentClass> ()
           .ToTable(tb => tb.HasTrigger("UpdateStudentStatus"));

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AssessmentScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assessme__3213E83FCC60AC6C");

            entity.ToTable("AssessmentScheme");

            entity.HasIndex(e => e.SyllabusId, "IX_AssessmentScheme_syllabus_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssesmentSchemeId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("assesmentSchemeId");
            entity.Property(e => e.Assignment).HasColumnName("assignment");
            entity.Property(e => e.Final).HasColumnName("final");
            entity.Property(e => e.FinalPractice).HasColumnName("final_practice");
            entity.Property(e => e.FinalTheory).HasColumnName("final_theory");
            entity.Property(e => e.Gpa).HasColumnName("gpa");
            entity.Property(e => e.Quiz).HasColumnName("quiz");
            entity.Property(e => e.SyllabusId)
                .HasMaxLength(36)
                .HasColumnName("syllabus_id");

            entity.HasOne(d => d.Syllabus).WithMany(p => p.AssessmentSchemes)
                .HasPrincipalKey(p => p.SyllabusId)
                .HasForeignKey(d => d.SyllabusId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Assessmen__sylla__45BE5BA9");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignme__3213E83FAFB0A0F5");

            entity.ToTable("Assignment");

            entity.HasIndex(e => e.AssignmentId, "AK_Assignment_assignmentId").IsUnique();

            entity.HasIndex(e => e.ModuleId, "IX_Assignment_ModuleId");

            entity.HasIndex(e => e.AssignmentId, "UQ__Assignme__52C21821D9283E9E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignmentId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("assignmentId");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModuleId).HasMaxLength(36);
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Module).WithMany(p => p.Assignments)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_Assignment_Module");
        });

        modelBuilder.Entity<AttendeeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendee__3213E83F9919DF08");

            entity.ToTable("AttendeeType");

            entity.HasIndex(e => e.AttendeeTypeId, "AK_AttendeeType_attendeeTypeId").IsUnique();

            entity.HasIndex(e => e.AttendeeTypeId, "UQ__Attendee__114FA692CC3579EB").IsUnique();

            entity.HasIndex(e => e.AttendeeTypeName, "attendee_type_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendeeTypeId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("attendeeTypeId");
            entity.Property(e => e.AttendeeTypeName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3213E83F5E231634");

            entity.ToTable("Class");

            entity.HasIndex(e => e.ClassId, "AK_Class_classId").IsUnique();

            entity.HasIndex(e => e.AttendeeLevelId, "IX_Class_AttendeeLevelId");

            entity.HasIndex(e => e.LocationId, "IX_Class_LocationId");

            entity.HasIndex(e => e.TrainingProgramCode, "IX_Class_TrainingProgramCode");

            entity.HasIndex(e => e.FsuId, "IX_Class_fsu_id");

            entity.HasIndex(e => e.ClassId, "UQ__Class__7577347F0E09220E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovedBy).HasMaxLength(36);
            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.AttendeeLevelId).HasMaxLength(36);
            entity.Property(e => e.ClassCode).HasMaxLength(255);
            entity.Property(e => e.ClassId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("classId");
            entity.Property(e => e.ClassName).HasMaxLength(255);
            entity.Property(e => e.ClassStatus).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FsuId)
                .HasMaxLength(36)
                .HasColumnName("fsu_id");
            entity.Property(e => e.LocationId).HasMaxLength(36);
            entity.Property(e => e.ReviewBy).HasMaxLength(36);
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.SlotTime).HasMaxLength(30);
            entity.Property(e => e.TrainingProgramCode).HasMaxLength(36);
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.AttendeeLevel).WithMany(p => p.Classes)
                .HasPrincipalKey(p => p.AttendeeTypeId)
                .HasForeignKey(d => d.AttendeeLevelId)
                .HasConstraintName("FK_Class_AttendeeType");

            entity.HasOne(d => d.Fsu).WithMany(p => p.Classes)
                .HasPrincipalKey(p => p.FsuId)
                .HasForeignKey(d => d.FsuId)
                .HasConstraintName("FK_Class_FSU");

            entity.HasOne(d => d.Location).WithMany(p => p.Classes)
                .HasPrincipalKey(p => p.LocationId)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Class_Location");

            entity.HasOne(d => d.TrainingProgramCodeNavigation).WithMany(p => p.Classes)
                .HasPrincipalKey(p => p.TrainingProgramCode)
                .HasForeignKey(d => d.TrainingProgramCode)
                .HasConstraintName("FK_Class_TrainingProgram");
        });

        modelBuilder.Entity<ClassTrainerLocation>(entity =>
        {
            entity.ToTable("ClassTrainerLocation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId)
                .HasMaxLength(36)
                .HasColumnName("classId");
            entity.Property(e => e.LocationId)
                .HasMaxLength(36)
                .HasColumnName("locationId");
            entity.Property(e => e.SyllabusUnitId)
                .HasMaxLength(36)
                .HasColumnName("syllabusUnitId");
            entity.Property(e => e.TrainerId)
                .HasMaxLength(36)
                .HasColumnName("trainerId");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassTrainerLocations)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassTrainerLocations_Class");

            entity.HasOne(d => d.Location).WithMany(p => p.ClassTrainerLocations)
                .HasPrincipalKey(p => p.LocationId)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_ClassTrainerLocations_Location");

            entity.HasOne(d => d.SyllabusUnit).WithMany(p => p.ClassTrainerLocations)
                .HasPrincipalKey(p => p.SyllabusUnitId)
                .HasForeignKey(d => d.SyllabusUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassTrainerLocations_SyllabusUnit");

            entity.HasOne(d => d.Trainer).WithMany(p => p.ClassTrainerLocations)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK_ClassTrainerLocations_User");
        });

        modelBuilder.Entity<ClassUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ClassId }).HasName("PK__ClassUse__0B395E30283D2B0E");

            entity.ToTable("ClassUser");

            entity.HasIndex(e => e.ClassId, "IX_ClassUser_ClassId");

            entity.Property(e => e.UserId).HasMaxLength(36);
            entity.Property(e => e.ClassId).HasMaxLength(36);
            entity.Property(e => e.UserType).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__ClassUser__Class__4B7734FF");

            entity.HasOne(d => d.User).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ClassUser__UserI__3587F3E0");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F97C1CCC9");

            entity.ToTable("DeliveryType");

            entity.HasIndex(e => e.DeliveryTypeId, "AK_DeliveryType_deliveryTypeId").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Delivery__72E12F1BCAE909DF").IsUnique();

            entity.HasIndex(e => e.DeliveryTypeId, "UQ__Delivery__BA19297BE49A3755").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryTypeId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("deliveryTypeId");
            entity.Property(e => e.Descriptions)
                .HasMaxLength(255)
                .HasColumnName("descriptions");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<EmailSend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83F3E3F0F74");

            entity.ToTable("EmailSend");

            entity.HasIndex(e => e.EmailSendId, "AK_EmailSend_emailSendId").IsUnique();

            entity.HasIndex(e => e.SenderId, "IX_EmailSend_SenderId");

            entity.HasIndex(e => e.TemplateId, "IX_EmailSend_TemplateId");

            entity.HasIndex(e => e.EmailSendId, "UQ__EmailSen__4B3B46D7354B2CBB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailSendId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailSendId");
            entity.Property(e => e.SenderId).HasMaxLength(36);
            entity.Property(e => e.TemplateId).HasMaxLength(36);

            entity.HasOne(d => d.Sender).WithMany(p => p.EmailSends)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_EmailSend_Sender");

            entity.HasOne(d => d.Template).WithMany(p => p.EmailSends)
                .HasPrincipalKey(p => p.EmailTemplateId)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK_EmailSend_Template");
        });

        modelBuilder.Entity<EmailSendStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83F60B657A4");

            entity.ToTable("EmailSendStudent");

            entity.HasIndex(e => e.EmailId, "IX_EmailSendStudent_EmailId");

            entity.HasIndex(e => e.ReceiverId, "IX_EmailSendStudent_ReceiverId");

            entity.HasIndex(e => e.EmailSendStudentId, "UQ__EmailSen__2D96D8D684A06733").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailId).HasMaxLength(36);
            entity.Property(e => e.EmailSendStudentId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailSendStudentId");
            entity.Property(e => e.ReceiverId).HasMaxLength(36);

            entity.HasOne(d => d.Email).WithMany(p => p.EmailSendStudents)
                .HasPrincipalKey(p => p.EmailSendId)
                .HasForeignKey(d => d.EmailId)
                .HasConstraintName("FK_EmailSendStudent_Email");

            entity.HasOne(d => d.Receiver).WithMany(p => p.EmailSendStudents)
                .HasPrincipalKey(p => p.StudentId)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK_EmailSendStudent_Receiver");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailTem__3213E83FC51A311E");

            entity.ToTable("EmailTemplate");

            entity.HasIndex(e => e.EmailTemplateId, "AK_EmailTemplate_emailTemplateId").IsUnique();

            entity.HasIndex(e => e.EmailTemplateId, "UQ__EmailTem__C443B510273F0DA5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EmailTemplateId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailTemplateId");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Fsu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fsu__3213E83F64A66A7E");

            entity.ToTable("fsu");

            entity.HasIndex(e => e.FsuId, "AK_fsu_fsuId").IsUnique();

            entity.HasIndex(e => e.FsuId, "UQ__fsu__E1FCEFCA7EF29731").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FsuId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("fsuId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<InboxState>(entity =>
        {
            entity.ToTable("InboxState");

            entity.HasIndex(e => new { e.MessageId, e.ConsumerId }, "AK_InboxState_MessageId_ConsumerId").IsUnique();

            entity.HasIndex(e => e.Delivered, "IX_InboxState_Delivered");

            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC0778E516FB");

            entity.ToTable("Location");

            entity.HasIndex(e => e.LocationId, "AK_Location_locationId").IsUnique();

            entity.HasIndex(e => e.LocationId, "UQ__Location__30646B6F32B77C0A").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.LocationId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("locationId");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Major__3213E83F613E5602");

            entity.ToTable("Major");

            entity.HasIndex(e => e.MajorId, "AK_Major_majorId").IsUnique();

            entity.HasIndex(e => e.Name, "FK_MAJOR_NAME").IsUnique();

            entity.HasIndex(e => e.MajorId, "UQ__Major__A5B1B4B55E10EFFF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MajorId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("majorId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Module__3213E83F19463C25");

            entity.ToTable("Module");

            entity.HasIndex(e => e.ModuleId, "AK_Module_moduleId").IsUnique();

            entity.HasIndex(e => e.ModuleId, "UQ__Module__8EEC8E166C0A30EB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModuleId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("moduleId");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.HasKey(e => e.SequenceNumber);

            entity.ToTable("OutboxMessage");

            entity.HasIndex(e => e.EnqueueTime, "IX_OutboxMessage_EnqueueTime");

            entity.HasIndex(e => e.ExpirationTime, "IX_OutboxMessage_ExpirationTime");

            entity.HasIndex(e => new { e.InboxMessageId, e.InboxConsumerId, e.SequenceNumber }, "IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber")
                .IsUnique()
                .HasFilter("([InboxMessageId] IS NOT NULL AND [InboxConsumerId] IS NOT NULL)");

            entity.HasIndex(e => new { e.OutboxId, e.SequenceNumber }, "IX_OutboxMessage_OutboxId_SequenceNumber")
                .IsUnique()
                .HasFilter("([OutboxId] IS NOT NULL)");

            entity.Property(e => e.ContentType).HasMaxLength(256);
            entity.Property(e => e.DestinationAddress).HasMaxLength(256);
            entity.Property(e => e.FaultAddress).HasMaxLength(256);
            entity.Property(e => e.ResponseAddress).HasMaxLength(256);
            entity.Property(e => e.SourceAddress).HasMaxLength(256);
        });

        modelBuilder.Entity<OutboxState>(entity =>
        {
            entity.HasKey(e => e.OutboxId);

            entity.ToTable("OutboxState");

            entity.HasIndex(e => e.Created, "IX_OutboxState_Created");

            entity.Property(e => e.OutboxId).ValueGeneratedNever();
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<OutputStandard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OutputSt__3213E83FFE1AA419");

            entity.ToTable("OutputStandard");

            entity.HasIndex(e => e.OutputStandardId, "AK_OutputStandard_outputStandardId").IsUnique();

            entity.HasIndex(e => e.OutputStandardId, "UQ__OutputSt__BED5012D7A551E0B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.Descriptions)
                .HasMaxLength(255)
                .HasColumnName("descriptions");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OutputStandardId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("outputStandardId");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quiz__3213E83FA0E025B4");

            entity.ToTable("Quiz");

            entity.HasIndex(e => e.QuizId, "AK_Quiz_quizId").IsUnique();

            entity.HasIndex(e => e.ModuleId, "IX_Quiz_ModuleId");

            entity.HasIndex(e => e.QuizId, "UQ__Quiz__CFF54C3C832E8C81").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.ModuleId).HasMaxLength(36);
            entity.Property(e => e.QuizId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("quizId");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);

            entity.HasOne(d => d.Module).WithMany(p => p.Quizzes)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Quiz_Module");
        });

        modelBuilder.Entity<QuizStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuizStud__3213E83F47D940D4");

            entity.ToTable("QuizStudent");

            entity.HasIndex(e => e.QuizId, "IX_QuizStudent_QuizId");

            entity.HasIndex(e => e.StudentId, "IX_QuizStudent_StudentId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuizId).HasMaxLength(36);
            entity.Property(e => e.QuizStudentId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("quizStudentId");
            entity.Property(e => e.Score).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StudentId).HasMaxLength(36);

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizStudents)
                .HasPrincipalKey(p => p.QuizId)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_QuizStudent_Quiz");

            entity.HasOne(d => d.Student).WithMany(p => p.QuizStudents)
                .HasPrincipalKey(p => p.StudentId)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_QuizStudent_Student");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.HasIndex(e => e.UserId, "IX_RefreshToken_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ReservedClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserved__3213E83F27E8B567");

            entity.ToTable("ReservedClass");

            entity.HasIndex(e => e.ClassId, "IX_ReservedClass_ClassId");

            entity.HasIndex(e => e.StudentId, "IX_ReservedClass_StudentId");

            entity.HasIndex(e => e.ReservedClassId, "UQ__Reserved__12EF4C50142F7E21").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId).HasMaxLength(36);
            entity.Property(e => e.Reason).HasMaxLength(200);
            entity.Property(e => e.ReservedClassId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("reservedClassId");
            entity.Property(e => e.StudentId).HasMaxLength(36);

            entity.HasOne(d => d.Class).WithMany(p => p.ReservedClasses)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_ReservedClass_Class");

            entity.HasOne(d => d.Student).WithMany(p => p.ReservedClasses)
                .HasPrincipalKey(p => p.StudentId)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ReservedClass_Student");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FC8774946");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleId, "AK_Role_roleId").IsUnique();

            entity.HasIndex(e => e.RoleId, "UQ__Role__CD98462B408B2C9E").IsUnique();

            entity.HasIndex(e => e.RoleName, "role_name_unique")
                .IsUnique()
                .HasFilter("([RoleName] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(36);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("roleId");
            entity.Property(e => e.RoleName).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId);

            entity.ToTable("RolePermission");

            entity.HasIndex(e => e.RoleId, "UQ__RolePerm__8AFACE1B5732A972").IsUnique();

            entity.Property(e => e.PermissionId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.Class).HasMaxLength(36);
            entity.Property(e => e.LearningMaterial).HasMaxLength(36);
            entity.Property(e => e.RoleId).HasMaxLength(36);
            entity.Property(e => e.Syllabus).HasMaxLength(36);
            entity.Property(e => e.TrainingProgram).HasMaxLength(36);
            entity.Property(e => e.UserManagement).HasMaxLength(36);

            entity.HasOne(d => d.Role).WithOne(p => p.RolePermission)
                .HasPrincipalKey<Role>(p => p.RoleId)
                .HasForeignKey<RolePermission>(d => d.RoleId)
                .HasConstraintName("FK__RolePermi__RoleI__40058253");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Score__3213E83F09BAE30F");

            entity.ToTable("Score");

            entity.HasIndex(e => e.AssignmentId, "IX_Score_AssignmentId");

            entity.HasIndex(e => e.StudentId, "IX_Score_StudentId");

            entity.HasIndex(e => e.ScoreId, "UQ__Score__B56A0C8C4A13EA97").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignmentId).HasMaxLength(36);
            entity.Property(e => e.Score1)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Score");
            entity.Property(e => e.ScoreId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("scoreId");
            entity.Property(e => e.StudentId).HasMaxLength(36);

            entity.HasOne(d => d.Assignment).WithMany(p => p.Scores)
                .HasPrincipalKey(p => p.AssignmentId)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK_Score_Assignment");

            entity.HasOne(d => d.Student).WithMany(p => p.Scores)
                .HasPrincipalKey(p => p.StudentId)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Score_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3213E83F166127A4");

            entity.ToTable("Student");

            entity.HasIndex(e => e.StudentId, "AK_Student_studentId").IsUnique();

            entity.HasIndex(e => e.MajorId, "IX_Student_MajorId");

            entity.HasIndex(e => e.StudentId, "UQ__Student__4D11D63DBF618A0B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Faaccount).HasColumnName("FAAccount");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("GPA");
            entity.Property(e => e.MajorId).HasMaxLength(36);
            entity.Property(e => e.MutatableStudentId).HasColumnName("MutatableStudentID");
            entity.Property(e => e.Recer)
                .HasMaxLength(100)
                .HasColumnName("RECer");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StudentId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("studentId");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasPrincipalKey(p => p.MajorId)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK__Student__MajorId__43D61337");
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentC__3213E83F82F467D8");

            entity.ToTable("StudentClass");

            entity.HasIndex(e => e.ClassId, "IX_StudentClass_ClassId");

            entity.HasIndex(e => e.StudentId, "IX_StudentClass_StudentId");

            entity.HasIndex(e => e.StudentClassId, "UQ__StudentC__114B9902500C79F3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendingStatus).HasMaxLength(50);
            entity.Property(e => e.ClassId).HasMaxLength(36);
            entity.Property(e => e.FinalScore).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Gpalevel).HasColumnName("GPALevel");
            entity.Property(e => e.StudentClassId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("studentClassId");
            entity.Property(e => e.StudentId).HasMaxLength(36);

            entity.HasOne(d => d.Class).WithMany(p => p.StudentClasses)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_StudentClass_Class");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentClasses)
                .HasPrincipalKey(p => p.StudentId)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_StudentClass_Student");
        });

        modelBuilder.Entity<StudentModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentM__3213E83F1922A532");

            entity.ToTable("StudentModule");

            entity.HasIndex(e => e.ModuleId, "IX_StudentModule_ModuleId");

            entity.HasIndex(e => e.StudentId, "IX_StudentModule_StudentId");

            entity.HasIndex(e => e.StudentModuleId, "UQ__StudentM__4A54FA669301BCB6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ModuleId).HasMaxLength(36);
            entity.Property(e => e.ModuleScore).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StudentId).HasMaxLength(36);
            entity.Property(e => e.StudentModuleId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("studentModuleId");

            entity.HasOne(d => d.Module).WithMany(p => p.StudentModules)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_StudentModule_Module");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentModules)
                .HasPrincipalKey(p => p.StudentId)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_StudentModule_Student");
        });

        modelBuilder.Entity<Syllabus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83FE0AED0E4");

            entity.ToTable("Syllabus");

            entity.HasIndex(e => e.SyllabusId, "AK_Syllabus_syllabusId").IsUnique();

            entity.HasIndex(e => e.SyllabusId, "UQ__Syllabus__915EDF81F530350C").IsUnique();

            entity.HasIndex(e => e.TopicCode, "topic_code_unique")
                .IsUnique()
                .HasFilter("([topic_code] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendeeNumber).HasColumnName("attendee_number");
            entity.Property(e => e.CourseObjective).HasColumnName("course_objective");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Days).HasColumnName("days");
            entity.Property(e => e.DeliveryPrinciple).HasColumnName("delivery_principle");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.Level)
                .HasMaxLength(50)
                .HasColumnName("level");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(36)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SyllabusId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("syllabusId");
            entity.Property(e => e.TechnicalRequirement).HasColumnName("technical_requirement");
            entity.Property(e => e.TopicCode)
                .HasMaxLength(20)
                .HasColumnName("topic_code");
            entity.Property(e => e.TopicName)
                .HasMaxLength(255)
                .HasColumnName("topic_name");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<SyllabusClass>(entity =>
        {
            entity.ToTable("SyllabusClass");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId)
                .HasMaxLength(36)
                .HasColumnName("classId");
            entity.Property(e => e.SyllabusId)
                .HasMaxLength(36)
                .HasColumnName("syllabusId");

            entity.HasOne(d => d.Class).WithMany(p => p.SyllabusClasses)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SyllabusClass_Class");

            entity.HasOne(d => d.Syllabus).WithMany(p => p.SyllabusClasses)
                .HasPrincipalKey(p => p.SyllabusId)
                .HasForeignKey(d => d.SyllabusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SyllabusClass_Syllabus");
        });

        modelBuilder.Entity<SyllabusDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83F96DB42A0");

            entity.ToTable("SyllabusDay");

            entity.HasIndex(e => e.SyllabusDayId, "AK_SyllabusDay_syllabusDayId").IsUnique();

            entity.HasIndex(e => e.SyllabusId, "IX_SyllabusDay_syllabus_id");

            entity.HasIndex(e => e.SyllabusDayId, "UQ__Syllabus__6F1A1381F1101CC8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.DayNo).HasColumnName("day_no");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(36)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SyllabusDayId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("syllabusDayId");
            entity.Property(e => e.SyllabusId)
                .HasMaxLength(36)
                .HasColumnName("syllabus_id");

            entity.HasOne(d => d.Syllabus).WithMany(p => p.SyllabusDays)
                .HasPrincipalKey(p => p.SyllabusId)
                .HasForeignKey(d => d.SyllabusId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SyllabusD__sylla__5D95E53A");
        });

        modelBuilder.Entity<SyllabusUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83F976FDEE6");

            entity.ToTable("SyllabusUnit");

            entity.HasIndex(e => e.SyllabusUnitId, "AK_SyllabusUnit_syllabusUnitId").IsUnique();

            entity.HasIndex(e => e.SyllabusDayId, "IX_SyllabusUnit_syllabus_day_id");

            entity.HasIndex(e => e.SyllabusUnitId, "UQ__Syllabus__D5A449019B1027EC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(36)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SyllabusDayId)
                .HasMaxLength(36)
                .HasColumnName("syllabus_day_id");
            entity.Property(e => e.SyllabusUnitId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("syllabusUnitId");
            entity.Property(e => e.UnitNo).HasColumnName("unit_no");

            entity.HasOne(d => d.SyllabusDay).WithMany(p => p.SyllabusUnits)
                .HasPrincipalKey(p => p.SyllabusDayId)
                .HasForeignKey(d => d.SyllabusDayId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SyllabusU__sylla__5E8A0973");
        });

        modelBuilder.Entity<TechnicalCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83F5D376B44");

            entity.ToTable("TechnicalCode");

            entity.HasIndex(e => e.TechnicalCodeId, "AK_TechnicalCode_technicalCodeId").IsUnique();

            entity.HasIndex(e => e.TechnicalCodeId, "UQ__Technica__7E6FA295E18ED5C5").IsUnique();

            entity.HasIndex(e => e.TechnicalCodeName, "technical_code_unique_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.TechnicalCodeId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("technicalCodeId");
            entity.Property(e => e.TechnicalCodeName).HasMaxLength(255);
        });

        modelBuilder.Entity<TechnicalGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83F78E9E1EF");

            entity.ToTable("TechnicalGroup");

            entity.HasIndex(e => e.TechnicalGroupId, "AK_TechnicalGroup_technicalGroupId").IsUnique();

            entity.HasIndex(e => e.TechnicalGroupName, "TechnicalGroupNameUnique").IsUnique();

            entity.HasIndex(e => e.TechnicalGroupId, "UQ__Technica__07542F35E754278E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.TechnicalGroupId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("technicalGroupId");
            entity.Property(e => e.TechnicalGroupName).HasMaxLength(255);
        });

        modelBuilder.Entity<TrainingMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83F393D468D");

            entity.ToTable("TrainingMaterial");

            entity.HasIndex(e => e.UnitChapterId, "IX_TrainingMaterial_unit_chapter_id");

            entity.HasIndex(e => e.TrainingMaterialId, "UQ__Training__E3CB00D61261AAEC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsFile).HasColumnName("is_file");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(36)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TrainingMaterialId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("trainingMaterialId");
            entity.Property(e => e.UnitChapterId)
                .HasMaxLength(36)
                .HasColumnName("unit_chapter_id");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            entity.HasOne(d => d.UnitChapter).WithMany(p => p.TrainingMaterials)
                .HasPrincipalKey(p => p.UnitChapterId)
                .HasForeignKey(d => d.UnitChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TrainingM__unit___5F7E2DAC");
        });

        modelBuilder.Entity<TrainingProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83FAE168522");

            entity.ToTable("TrainingProgram");

            entity.HasIndex(e => e.TrainingProgramCode, "AK_TrainingProgram_TrainingProgramCode").IsUnique();

            entity.HasIndex(e => e.TechnicalCodeId, "IX_TrainingProgram_TechnicalCodeId");

            entity.HasIndex(e => e.TechnicalGroupId, "IX_TrainingProgram_TechnicalGroupId");

            entity.HasIndex(e => e.UserId, "IX_TrainingProgram_UserId");

            entity.HasIndex(e => e.TrainingProgramCode, "UQ__Training__8245E6A31E34736B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TechnicalCodeId).HasMaxLength(36);
            entity.Property(e => e.TechnicalGroupId).HasMaxLength(36);
            entity.Property(e => e.TrainingProgramCode)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(36);

            entity.HasOne(d => d.TechnicalCode).WithMany(p => p.TrainingPrograms)
                .HasPrincipalKey(p => p.TechnicalCodeId)
                .HasForeignKey(d => d.TechnicalCodeId)
                .HasConstraintName("FK_TrainingProgram_TechnicalCode");

            entity.HasOne(d => d.TechnicalGroup).WithMany(p => p.TrainingPrograms)
                .HasPrincipalKey(p => p.TechnicalGroupId)
                .HasForeignKey(d => d.TechnicalGroupId)
                .HasConstraintName("FK_TrainingProgram_TechnicalGroup");

            entity.HasOne(d => d.User).WithMany(p => p.TrainingPrograms)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TrainingProgram_User");
        });

        modelBuilder.Entity<TrainingProgramModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83F47060668");

            entity.ToTable("TrainingProgramModule");

            entity.HasIndex(e => e.ModuleId, "IX_TrainingProgramModule_ModuleId");

            entity.HasIndex(e => new { e.ProgramId, e.ModuleId }, "UQ__Training__179227236E66ECA7").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ModuleId).HasMaxLength(36);
            entity.Property(e => e.ProgramId).HasMaxLength(36);

            entity.HasOne(d => d.Module).WithMany(p => p.TrainingProgramModules)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_TrainingProgramModule_Module");

            entity.HasOne(d => d.Program).WithMany(p => p.TrainingProgramModules)
                .HasPrincipalKey(p => p.TrainingProgramCode)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("FK_TrainingProgramModule_Program");
        });

        modelBuilder.Entity<TrainingProgramSyllabus>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SyllabusId, e.TrainingProgramCode }).HasName("PK__Training__2EE7C7116A349350");

            entity.ToTable("TrainingProgramSyllabus");

            entity.HasIndex(e => e.SyllabusId, "IX_TrainingProgramSyllabus_syllabus_id");

            entity.HasIndex(e => e.TrainingProgramCode, "IX_TrainingProgramSyllabus_training_program_code");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.SyllabusId)
                .HasMaxLength(36)
                .HasColumnName("syllabus_id");
            entity.Property(e => e.TrainingProgramCode)
                .HasMaxLength(36)
                .HasColumnName("training_program_code");

            entity.HasOne(d => d.Syllabus).WithMany(p => p.TrainingProgramSyllabi)
                .HasPrincipalKey(p => p.SyllabusId)
                .HasForeignKey(d => d.SyllabusId)
                .HasConstraintName("FK__TrainingP__sylla__65370702");

            entity.HasOne(d => d.TrainingProgramCodeNavigation).WithMany(p => p.TrainingProgramSyllabi)
                .HasPrincipalKey(p => p.TrainingProgramCode)
                .HasForeignKey(d => d.TrainingProgramCode)
                .HasConstraintName("FK__TrainingP__train__662B2B3B");
        });

        modelBuilder.Entity<UnitChapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UnitChap__3213E83FEE1207B2");

            entity.ToTable("UnitChapter");

            entity.HasIndex(e => e.UnitChapterId, "AK_UnitChapter_unitChapterId").IsUnique();

            entity.HasIndex(e => e.DeliveryTypeId, "IX_UnitChapter_delivery_type_id");

            entity.HasIndex(e => e.OutputStandardId, "IX_UnitChapter_output_standard_id");

            entity.HasIndex(e => e.SyllabusUnitId, "IX_UnitChapter_syllabus_unit_id");

            entity.HasIndex(e => e.UnitChapterId, "UQ__UnitChap__A4B0833C1DDD0FF2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChapterNo).HasColumnName("chapter_no");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.DeliveryTypeId)
                .HasMaxLength(36)
                .HasColumnName("delivery_type_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsOnline).HasColumnName("is_online");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(36)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OutputStandardId)
                .HasMaxLength(36)
                .HasColumnName("output_standard_id");
            entity.Property(e => e.SyllabusUnitId)
                .HasMaxLength(36)
                .HasColumnName("syllabus_unit_id");
            entity.Property(e => e.UnitChapterId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("unitChapterId");

            entity.HasOne(d => d.DeliveryType).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.DeliveryTypeId)
                .HasForeignKey(d => d.DeliveryTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UnitChapt__deliv__671F4F74");

            entity.HasOne(d => d.OutputStandard).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.OutputStandardId)
                .HasForeignKey(d => d.OutputStandardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UnitChapt__outpu__690797E6");

            entity.HasOne(d => d.SyllabusUnit).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.SyllabusUnitId)
                .HasForeignKey(d => d.SyllabusUnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UnitChapt__sylla__69FBBC1F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83FD7603D84");

            entity.ToTable("User");

            entity.HasIndex(e => e.UserId, "AK_User_userId").IsUnique();

            entity.HasIndex(e => e.Email, "EmailUnique").IsUnique();

            entity.HasIndex(e => e.RoleId, "IX_User_RoleId");

            entity.HasIndex(e => e.UserId, "UQ__User__CB9A1CFE05390903").IsUnique();

            entity.HasIndex(e => e.Username, "UsernameUnique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(36);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasMaxLength(36);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("userId");
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasPrincipalKey(p => p.RoleId)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3214EC07C0E79A0B");

            entity.ToTable("UserPermission");

            entity.HasIndex(e => e.UserPermissionId, "UQ__UserPerm__0E30AD2E5F3873EC").IsUnique();

            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.UserPermissionId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("userPermissionId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
