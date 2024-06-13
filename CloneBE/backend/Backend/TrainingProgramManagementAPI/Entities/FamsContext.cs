using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainingProgramManagementAPI.Entities;

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

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer(GetConnectionString());

    // private string GetConnectionString()
    // {
    //     IConfiguration config = new ConfigurationBuilder()
    //         .SetBasePath(Directory.GetCurrentDirectory())
    //         // .AddJsonFile("appsettings.Development.json")
    //         .AddJsonFile("appsettings.Docker.json")
    //         .Build();
    //     return config.GetConnectionString("DefaultConnection")!;
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssessmentScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assessme__3213E83FB978B6FB");

            entity.ToTable("AssessmentScheme");

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
                .HasConstraintName("FK__Assessmen__sylla__2EDAF651");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignme__3213E83F7A5E6252");

            entity.ToTable("Assignment");

            entity.HasIndex(e => e.AssignmentId, "UQ__Assignme__52C218214C093618").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Attendee__3213E83F84282634");

            entity.ToTable("AttendeeType");

            entity.HasIndex(e => e.AttendeeTypeId, "UQ__Attendee__114FA69245342566").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Class__3213E83FCA48A773");

            entity.ToTable("Class");

            entity.HasIndex(e => e.ClassId, "UQ__Class__7577347FF6AD2CF4").IsUnique();

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

        modelBuilder.Entity<ClassUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ClassId }).HasName("PK__ClassUse__0B395E30E4C67046");

            entity.ToTable("ClassUser");

            entity.Property(e => e.UserId).HasMaxLength(36);
            entity.Property(e => e.ClassId).HasMaxLength(36);
            entity.Property(e => e.UserType).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__ClassUser__Class__3493CFA7");

            entity.HasOne(d => d.User).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ClassUser__UserI__3587F3E0");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83FC15D3F86");

            entity.ToTable("DeliveryType");

            entity.HasIndex(e => e.Name, "UQ__Delivery__72E12F1B177ADEA7").IsUnique();

            entity.HasIndex(e => e.DeliveryTypeId, "UQ__Delivery__BA19297B6176554A").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83F3F5B0336");

            entity.ToTable("EmailSend");

            entity.HasIndex(e => e.EmailSendId, "UQ__EmailSen__4B3B46D7A46A64B5").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83F8CDE158B");

            entity.ToTable("EmailSendStudent");

            entity.HasIndex(e => e.EmailSendStudentId, "UQ__EmailSen__2D96D8D6C3D961E7").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__EmailTem__3213E83FE2C75866");

            entity.ToTable("EmailTemplate");

            entity.HasIndex(e => e.EmailTemplateId, "UQ__EmailTem__C443B5106EBE293E").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__fsu__3213E83F6BA5BBB1");

            entity.ToTable("fsu");

            entity.HasIndex(e => e.FsuId, "UQ__fsu__E1FCEFCA8A70E292").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FsuId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("fsuId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC07D1938511");

            entity.ToTable("Location");

            entity.HasIndex(e => e.LocationId, "UQ__Location__30646B6F0C1C5BC5").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.LocationId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("locationId");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Major__3213E83F9673AB07");

            entity.ToTable("Major");

            entity.HasIndex(e => e.Name, "FK_MAJOR_NAME").IsUnique();

            entity.HasIndex(e => e.MajorId, "UQ__Major__A5B1B4B5DEA77469").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Module__3213E83F9FA0D766");

            entity.ToTable("Module");

            entity.HasIndex(e => e.ModuleId, "UQ__Module__8EEC8E16A3D5A820").IsUnique();

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

        modelBuilder.Entity<OutputStandard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OutputSt__3213E83F10CD5257");

            entity.ToTable("OutputStandard");

            entity.HasIndex(e => e.OutputStandardId, "UQ__OutputSt__BED5012DA3635214").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Quiz__3213E83FC0A5F013");

            entity.ToTable("Quiz");

            entity.HasIndex(e => e.QuizId, "UQ__Quiz__CFF54C3C00F26B80").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__QuizStud__3213E83FBE688614");

            entity.ToTable("QuizStudent");

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

        modelBuilder.Entity<ReservedClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserved__3213E83F2E8E78D5");

            entity.ToTable("ReservedClass");

            entity.HasIndex(e => e.ReservedClassId, "UQ__Reserved__12EF4C5072FCB98D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId).HasMaxLength(36);
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
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FF479330D");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleId, "UQ__Role__CD98462B6030D1EF").IsUnique();

            entity.HasIndex(e => e.RoleName, "role_name_unique").IsUnique();

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
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("PK__RolePerm__6400A1A89B80BCF9");

            entity.ToTable("RolePermission");

            entity.HasIndex(e => e.RoleId, "UQ__RolePerm__8AFACE1BAC74F8B5").IsUnique();

            entity.HasIndex(e => e.PermissionId, "UQ__RolePerm__EFA6FB2E95AC195C").IsUnique();

            entity.Property(e => e.RoleId).HasMaxLength(36);
            entity.Property(e => e.PermissionId).HasMaxLength(36);

            entity.HasOne(d => d.Permission).WithOne(p => p.RolePermission)
                .HasPrincipalKey<UserPermission>(p => p.UserPermissionId)
                .HasForeignKey<RolePermission>(d => d.PermissionId)
                .HasConstraintName("FK__RolePermi__Permi__3F115E1A");

            entity.HasOne(d => d.Role).WithOne(p => p.RolePermission)
                .HasPrincipalKey<Role>(p => p.RoleId)
                .HasForeignKey<RolePermission>(d => d.RoleId)
                .HasConstraintName("FK__RolePermi__RoleI__40058253");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Score__3213E83F90A87152");

            entity.ToTable("Score");

            entity.HasIndex(e => e.ScoreId, "UQ__Score__B56A0C8CCE7E16FB").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Student__3213E83F37F48545");

            entity.ToTable("Student");

            entity.HasIndex(e => e.StudentId, "UQ__Student__4D11D63D7B074F93").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Faaccount).HasColumnName("FAAccount");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("GPA");
            entity.Property(e => e.MajorId).HasMaxLength(36);
            entity.Property(e => e.Recer)
                .HasMaxLength(100)
                .HasColumnName("RECer");
            entity.Property(e => e.StudentId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("studentId");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasPrincipalKey(p => p.MajorId)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK__Student__MajorId__42E1EEFE");
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentC__3213E83FD2FCBE9E");

            entity.ToTable("StudentClass");

            entity.HasIndex(e => e.StudentClassId, "UQ__StudentC__114B9902D9CFED03").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__StudentM__3213E83FC78C781B");

            entity.ToTable("StudentModule");

            entity.HasIndex(e => e.StudentModuleId, "UQ__StudentM__4A54FA6620B2B562").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83F43057209");

            entity.ToTable("Syllabus");

            entity.HasIndex(e => e.SyllabusId, "UQ__Syllabus__915EDF81276B6197").IsUnique();

            entity.HasIndex(e => e.TopicCode, "topic_code_unique").IsUnique();

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

            entity.HasMany(d => d.TrainingProgramCodes).WithMany(p => p.Syllabi)
                .UsingEntity<Dictionary<string, object>>(
                    "TrainingProgramSyllabus",
                    r => r.HasOne<TrainingProgram>().WithMany()
                        .HasPrincipalKey("TrainingProgramCode")
                        .HasForeignKey("TrainingProgramCode")
                        .HasConstraintName("FK__TrainingP__train__503BEA1C"),
                    l => l.HasOne<Syllabus>().WithMany()
                        .HasPrincipalKey("SyllabusId")
                        .HasForeignKey("SyllabusId")
                        .HasConstraintName("FK__TrainingP__sylla__4F47C5E3"),
                    j =>
                    {
                        j.HasKey("SyllabusId", "TrainingProgramCode").HasName("PK__Training__CF42F2E07A552292");
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
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83F91ED0402");

            entity.ToTable("SyllabusDay");

            entity.HasIndex(e => e.SyllabusDayId, "UQ__Syllabus__6F1A138137C61246").IsUnique();

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
                .HasConstraintName("FK__SyllabusD__sylla__47A6A41B");
        });

        modelBuilder.Entity<SyllabusUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83FA8C36ECD");

            entity.ToTable("SyllabusUnit");

            entity.HasIndex(e => e.SyllabusUnitId, "UQ__Syllabus__D5A44901F079FD64").IsUnique();

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
                .HasConstraintName("FK__SyllabusU__sylla__489AC854");
        });

        modelBuilder.Entity<TechnicalCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83FE26B9A1D");

            entity.ToTable("TechnicalCode");

            entity.HasIndex(e => e.TechnicalCodeId, "UQ__Technica__7E6FA295E09BE7AD").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83FE7BC530C");

            entity.ToTable("TechnicalGroup");

            entity.HasIndex(e => e.TechnicalGroupName, "TechnicalGroupNameUnique").IsUnique();

            entity.HasIndex(e => e.TechnicalGroupId, "UQ__Technica__07542F35CF70F813").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83FF678E92B");

            entity.ToTable("TrainingMaterial");

            entity.HasIndex(e => e.TrainingMaterialId, "UQ__Training__E3CB00D6B202C1D6").IsUnique();

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
                .HasConstraintName("FK__TrainingM__unit___498EEC8D");
        });

        modelBuilder.Entity<TrainingProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83F90EAAA70");

            entity.ToTable("TrainingProgram");

            entity.HasIndex(e => e.TrainingProgramCode, "UQ__Training__8245E6A3B677DBC3").IsUnique();

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
            entity
                .HasNoKey()
                .ToTable("TrainingProgramModule");

            entity.HasIndex(e => e.ProgramId, "UQ__Training__752560593A81E55E").IsUnique();

            entity.Property(e => e.ModuleId).HasMaxLength(36);
            entity.Property(e => e.ProgramId).HasMaxLength(36);

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
            entity.HasKey(e => e.Id).HasName("PK__UnitChap__3213E83FBA27657A");

            entity.ToTable("UnitChapter");

            entity.HasIndex(e => e.UnitChapterId, "UQ__UnitChap__A4B0833C52C67D7D").IsUnique();

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
                .HasConstraintName("FK__UnitChapt__deliv__51300E55");

            entity.HasOne(d => d.OutputStandard).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.OutputStandardId)
                .HasForeignKey(d => d.OutputStandardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UnitChapt__outpu__5224328E");

            entity.HasOne(d => d.SyllabusUnit).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.SyllabusUnitId)
                .HasForeignKey(d => d.SyllabusUnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UnitChapt__sylla__531856C7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F17DCC196");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "EmailUnique").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__User__CB9A1CFE18846C55").IsUnique();

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
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_Role");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3214EC076C80C724");

            entity.ToTable("UserPermission");

            entity.HasIndex(e => e.UserPermissionId, "UQ__UserPerm__0E30AD2E4F65BF14").IsUnique();

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
