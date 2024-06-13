using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ScoreManagementAPI.Models;

namespace ScoreManagementAPI.Context;

public partial class FamsContext : DbContext
{
    public FamsContext()
    {
    }

    public FamsContext(DbContextOptions<FamsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssessmentScheme> AssessmentSchemes { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AttendeeType> AttendeeTypes { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassUser> ClassUsers { get; set; }

    public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }

    public virtual DbSet<EmailSend> EmailSends { get; set; }

    public virtual DbSet<EmailSendStudent> EmailSendStudents { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Fsu> Fsus { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<OutputStandard> OutputStandards { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizStudent> QuizStudents { get; set; }

    public virtual DbSet<ReservedClass> ReservedClasses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    public virtual DbSet<StudentModule> StudentModules { get; set; }

    public virtual DbSet<Syllabus> Syllabi { get; set; }

    public virtual DbSet<SyllabusDay> SyllabusDays { get; set; }

    public virtual DbSet<SyllabusUnit> SyllabusUnits { get; set; }

    public virtual DbSet<TechnicalCode> TechnicalCodes { get; set; }

    public virtual DbSet<TechnicalGroup> TechnicalGroups { get; set; }

    public virtual DbSet<TrainingMaterial> TrainingMaterials { get; set; }

    public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }

    public virtual DbSet<TrainingProgramModule> TrainingProgramModules { get; set; }

    public virtual DbSet<UnitChapter> UnitChapters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Phase2Database");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssessmentScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assessme__3213E83F99431233");

            entity.Property(e => e.AssesmentSchemeId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.Syllabus).WithMany(p => p.AssessmentSchemes)
                .HasPrincipalKey(p => p.SyllabusId)
                .HasForeignKey(d => d.SyllabusId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Assessmen__sylla__2EDAF651");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignme__3213E83F072D0988");

            entity.Property(e => e.AssignmentId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Module).WithMany(p => p.Assignments)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_Assignment_Module");
        });

        modelBuilder.Entity<AttendeeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendee__3213E83F9AB621E1");

            entity.Property(e => e.AttendeeTypeId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3213E83F66E39C14");

            entity.Property(e => e.ClassId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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

        modelBuilder.Entity<ClassUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ClassId }).HasName("PK__ClassUse__0B395E3033F02159");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__ClassUser__Class__367C1819");

            entity.HasOne(d => d.User).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ClassUser__UserI__37703C52");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F8D2D4FDA");

            entity.Property(e => e.DeliveryTypeId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<EmailSend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83FE40BAAF8");

            entity.Property(e => e.EmailSendId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83FB6DC4824");

            entity.Property(e => e.EmailSendStudentId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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
            entity.HasKey(e => e.Id).HasName("PK__EmailTem__3213E83FB94D42D0");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EmailTemplateId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Fsu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fsu__3213E83F34FB54CF");

            entity.Property(e => e.FsuId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC0705D631CF");

            entity.Property(e => e.LocationId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Major__3213E83FD57C066F");

            entity.Property(e => e.MajorId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Module__3213E83FA0A7ABD7");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModuleId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<OutputStandard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OutputSt__3213E83FFA1203B2");

            entity.Property(e => e.OutputStandardId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quiz__3213E83F7A961CEF");

            entity.Property(e => e.QuizId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.Module).WithMany(p => p.Quizzes)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Quiz_Module");
        });

        modelBuilder.Entity<QuizStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuizStud__3213E83F94DDA8EE");

            entity.Property(e => e.QuizStudentId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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

        modelBuilder.Entity<ReservedClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserved__3213E83FA15BAD05");

            entity.Property(e => e.ReservedClassId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FBC4F9F42");

            entity.Property(e => e.RoleId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("PK__RolePerm__6400A1A826748756");

            entity.HasOne(d => d.Permission).WithOne(p => p.RolePermission)
                .HasPrincipalKey<UserPermission>(p => p.UserPermissionId)
                .HasForeignKey<RolePermission>(d => d.PermissionId)
                .HasConstraintName("FK__RolePermi__Permi__40F9A68C");

            entity.HasOne(d => d.Role).WithOne(p => p.RolePermission)
                .HasPrincipalKey<Role>(p => p.RoleId)
                .HasForeignKey<RolePermission>(d => d.RoleId)
                .HasConstraintName("FK__RolePermi__RoleI__41EDCAC5");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Score__3213E83F7F89EC7A");

            entity.Property(e => e.ScoreId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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
            entity.HasIndex(e => e.MutatableStudentID).IsUnique();

            entity.HasKey(e => e.Id).HasName("PK__Student__3213E83F3485C51E");

            entity.Property(e => e.StudentId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasPrincipalKey(p => p.MajorId)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK__Student__MajorId__44CA3770");
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentC__3213E83F0B5F7B86");

            entity.Property(e => e.StudentClassId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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
            entity.HasKey(e => e.Id).HasName("PK__StudentM__3213E83F5FFC8BAB");

            entity.Property(e => e.StudentModuleId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83FEE2C76F8");

            entity.Property(e => e.SyllabusId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasMany(d => d.TrainingProgramCodes).WithMany(p => p.Syllabi)
                .UsingEntity<Dictionary<string, object>>(
                    "TrainingProgramSyllabus",
                    r => r.HasOne<TrainingProgram>().WithMany()
                        .HasPrincipalKey("TrainingProgramCode")
                        .HasForeignKey("TrainingProgramCode")
                        .HasConstraintName("FK__TrainingP__train__5224328E"),
                    l => l.HasOne<Syllabus>().WithMany()
                        .HasPrincipalKey("SyllabusId")
                        .HasForeignKey("SyllabusId")
                        .HasConstraintName("FK__TrainingP__sylla__51300E55"),
                    j =>
                    {
                        j.HasKey("SyllabusId", "TrainingProgramCode").HasName("PK__Training__CF42F2E0267126E8");
                        j.ToTable("TrainingProgramSyllabus");
                        j.IndexerProperty<string>("SyllabusId")
                            .HasMaxLength(36)
                            .HasColumnName("syllabus_id");
                        j.IndexerProperty<string>("TrainingProgramCode")
                            .HasMaxLength(36)
                            .HasColumnName("training_program_code");
                    });
        });

        modelBuilder.Entity<SyllabusDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83F0C4B4C1A");

            entity.Property(e => e.SyllabusDayId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.Syllabus).WithMany(p => p.SyllabusDays)
                .HasPrincipalKey(p => p.SyllabusId)
                .HasForeignKey(d => d.SyllabusId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SyllabusD__sylla__498EEC8D");
        });

        modelBuilder.Entity<SyllabusUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83FFC642C5B");

            entity.Property(e => e.SyllabusUnitId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.SyllabusDay).WithMany(p => p.SyllabusUnits)
                .HasPrincipalKey(p => p.SyllabusDayId)
                .HasForeignKey(d => d.SyllabusDayId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SyllabusU__sylla__4A8310C6");
        });

        modelBuilder.Entity<TechnicalCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83FB58F0454");

            entity.Property(e => e.TechnicalCodeId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<TechnicalGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83FC70FAB2F");

            entity.Property(e => e.TechnicalGroupId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<TrainingMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83F67DDB0F7");

            entity.Property(e => e.TrainingMaterialId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.UnitChapter).WithMany(p => p.TrainingMaterials)
                .HasPrincipalKey(p => p.UnitChapterId)
                .HasForeignKey(d => d.UnitChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TrainingM__unit___4B7734FF");
        });

        modelBuilder.Entity<TrainingProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83F0C3B8760");

            entity.Property(e => e.TrainingProgramCode).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

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
            entity.HasOne(d => d.Module).WithMany()
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_TrainingProgramModule_Module");

            entity.HasOne(d => d.Program).WithOne()
                .HasPrincipalKey<TrainingProgram>(p => p.TrainingProgramCode)
                .HasForeignKey<TrainingProgramModule>(d => d.ProgramId)
                .HasConstraintName("FK_TrainingProgramModule_Program");
        });

        modelBuilder.Entity<UnitChapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UnitChap__3213E83F0C4B84C3");

            entity.Property(e => e.UnitChapterId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.DeliveryType).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.DeliveryTypeId)
                .HasForeignKey(d => d.DeliveryTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UnitChapt__deliv__531856C7");

            entity.HasOne(d => d.OutputStandard).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.OutputStandardId)
                .HasForeignKey(d => d.OutputStandardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UnitChapt__outpu__540C7B00");

            entity.HasOne(d => d.SyllabusUnit).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.SyllabusUnitId)
                .HasForeignKey(d => d.SyllabusUnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UnitChapt__sylla__55009F39");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F01BD641D");

            entity.Property(e => e.UserId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasPrincipalKey(p => p.RoleId)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_Role");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3214EC071230FE7D");

            entity.Property(e => e.UserPermissionId).HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });


		// Faker data

		(new Faker()).fake(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
