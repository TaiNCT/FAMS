using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReservationManagementAPI.Entities.Models;

namespace ReservationManagementAPI.Entities;

public partial class RepositoryContext : DbContext
{
    public RepositoryContext()
    {
    }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
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

    public virtual DbSet<TrainingProgramSyllabus> TrainingProgramSyllabi { get; set; }

    public virtual DbSet<UnitChapter> UnitChapters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=SA;pwd=12345;database=FAMS;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssessmentScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assessme__3213E83F9A925627");

            entity.ToTable("AssessmentScheme");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssesmentSchemeId)
                .IsRequired()
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
                .HasConstraintName("FK__Assessmen__sylla__42E1EEFE");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignme__3213E83F1DBDD8F5");

            entity.ToTable("Assignment");

            entity.HasIndex(e => e.AssignmentId, "UQ__Assignme__52C218215B09E6E5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignmentId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("assignmentId");
            entity.Property(e => e.AssignmentName).IsRequired();
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.ModuleId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Module).WithMany(p => p.Assignments)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_Assignment_Module");
        });

        modelBuilder.Entity<AttendeeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendee__3213E83F5D4DFD40");

            entity.ToTable("AttendeeType");

            entity.HasIndex(e => e.AttendeeTypeId, "UQ__Attendee__114FA6922BACA7FB").IsUnique();

            entity.HasIndex(e => e.AttendeeTypeName, "attendee_type_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendeeTypeId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("attendeeTypeId");
            entity.Property(e => e.AttendeeTypeName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3213E83FC9479365");

            entity.ToTable("Class");

            entity.HasIndex(e => e.ClassId, "UQ__Class__7577347FBF3D8A8A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovedBy).HasMaxLength(36);
            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.AttendeeLevelId).HasMaxLength(36);
            entity.Property(e => e.ClassCode)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.ClassId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("classId");
            entity.Property(e => e.ClassName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.ClassStatus)
                .IsRequired()
                .HasMaxLength(255);
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
            entity.HasKey(e => new { e.UserId, e.ClassId }).HasName("PK__ClassUse__0B395E3001B36C70");

            entity.ToTable("ClassUser");

            entity.Property(e => e.UserId).HasMaxLength(36);
            entity.Property(e => e.ClassId).HasMaxLength(36);
            entity.Property(e => e.UserType).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.ClassId)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__ClassUser__Class__489AC854");

            entity.HasOne(d => d.User).WithMany(p => p.ClassUsers)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ClassUser__UserI__498EEC8D");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F5EDBBC69");

            entity.ToTable("DeliveryType");

            entity.HasIndex(e => e.Name, "UQ__Delivery__72E12F1B41C9047A").IsUnique();

            entity.HasIndex(e => e.DeliveryTypeId, "UQ__Delivery__BA19297BF211C79E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryTypeId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("deliveryTypeId");
            entity.Property(e => e.Descriptions)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("descriptions");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<EmailSend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83F33F13811");

            entity.ToTable("EmailSend");

            entity.HasIndex(e => e.EmailSendId, "UQ__EmailSen__4B3B46D7954ACC01").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.EmailSendId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailSendId");
            entity.Property(e => e.SenderId).HasMaxLength(36);
            entity.Property(e => e.TemplateId)
                .IsRequired()
                .HasMaxLength(36);

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
            entity.HasKey(e => e.Id).HasName("PK__EmailSen__3213E83FC04E7D50");

            entity.ToTable("EmailSendStudent");

            entity.HasIndex(e => e.EmailSendStudentId, "UQ__EmailSen__2D96D8D675831095").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.EmailSendStudentId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailSendStudentId");
            entity.Property(e => e.ReceiverId)
                .IsRequired()
                .HasMaxLength(36);

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
            entity.HasKey(e => e.Id).HasName("PK__EmailTem__3213E83FA4421598");

            entity.ToTable("EmailTemplate");

            entity.HasIndex(e => e.EmailTemplateId, "UQ__EmailTem__C443B510EBD257EB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.EmailTemplateId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailTemplateId");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Fsu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fsu__3213E83FB02F91B8");

            entity.ToTable("fsu");

            entity.HasIndex(e => e.FsuId, "UQ__fsu__E1FCEFCACB432895").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FsuId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("fsuId");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC070F7989C1");

            entity.ToTable("Location");

            entity.HasIndex(e => e.LocationId, "UQ__Location__30646B6F0BE088F7").IsUnique();

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.LocationId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("locationId");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Major__3213E83F7B3B438B");

            entity.ToTable("Major");

            entity.HasIndex(e => e.Name, "FK_MAJOR_NAME").IsUnique();

            entity.HasIndex(e => e.MajorId, "UQ__Major__A5B1B4B54E354109").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MajorId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("majorId");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Module__3213E83FE3373B1D");

            entity.ToTable("Module");

            entity.HasIndex(e => e.ModuleId, "UQ__Module__8EEC8E16D76E51A1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModuleId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("moduleId");
            entity.Property(e => e.ModuleName).IsRequired();
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<OutputStandard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OutputSt__3213E83FE003D0B0");

            entity.ToTable("OutputStandard");

            entity.HasIndex(e => e.OutputStandardId, "UQ__OutputSt__BED5012D37082054").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.Descriptions)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("descriptions");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OutputStandardId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("outputStandardId");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quiz__3213E83F96385B29");

            entity.ToTable("Quiz");

            entity.HasIndex(e => e.QuizId, "UQ__Quiz__CFF54C3CF76218FB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.ModuleId).HasMaxLength(36);
            entity.Property(e => e.QuizId)
                .IsRequired()
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
            entity.HasKey(e => e.Id).HasName("PK__QuizStud__3213E83FCE472983");

            entity.ToTable("QuizStudent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuizId).HasMaxLength(36);
            entity.Property(e => e.QuizStudentId)
                .IsRequired()
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
            entity.HasKey(e => e.Id).HasName("PK__Reserved__3213E83F917EDFC7");

            entity.ToTable("ReservedClass");

            entity.HasIndex(e => e.ReservedClassId, "UQ__Reserved__12EF4C50E7626FC5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.Reason)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.ReservedClassId)
                .IsRequired()
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
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83F47A52260");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleId, "UQ__Role__CD98462BEA28F289").IsUnique();

            entity.HasIndex(e => e.RoleName, "role_name_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(36);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("roleId");
            entity.Property(e => e.RoleName).HasMaxLength(255);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("PK__RolePerm__6400A1A89A59DC39");

            entity.ToTable("RolePermission");

            entity.HasIndex(e => e.RoleId, "UQ__RolePerm__8AFACE1B20EBA29A").IsUnique();

            entity.HasIndex(e => e.PermissionId, "UQ__RolePerm__EFA6FB2E832CDF2C").IsUnique();

            entity.Property(e => e.RoleId).HasMaxLength(36);
            entity.Property(e => e.PermissionId).HasMaxLength(36);

            entity.HasOne(d => d.Permission).WithOne(p => p.RolePermission)
                .HasPrincipalKey<UserPermission>(p => p.UserPermissionId)
                .HasForeignKey<RolePermission>(d => d.PermissionId)
                .HasConstraintName("FK__RolePermi__Permi__531856C7");

            entity.HasOne(d => d.Role).WithOne(p => p.RolePermission)
                .HasPrincipalKey<Role>(p => p.RoleId)
                .HasForeignKey<RolePermission>(d => d.RoleId)
                .HasConstraintName("FK__RolePermi__RoleI__540C7B00");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Score__3213E83FCF0BEED7");

            entity.ToTable("Score");

            entity.HasIndex(e => e.ScoreId, "UQ__Score__B56A0C8C1966E3C0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignmentId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.Score1)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Score");
            entity.Property(e => e.ScoreId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("scoreId");
            entity.Property(e => e.StudentId)
                .IsRequired()
                .HasMaxLength(36);

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
            entity.HasKey(e => e.Id).HasName("PK__Student__3213E83F39DCAEA2");

            entity.ToTable("Student");

            entity.HasIndex(e => e.StudentId, "UQ__Student__4D11D63D6BEE8FFD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.Area).IsRequired();
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Faaccount)
                .IsRequired()
                .HasColumnName("FAAccount");
            entity.Property(e => e.FullName).IsRequired();
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("GPA");
            entity.Property(e => e.MajorId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.Recer)
                .HasMaxLength(100)
                .HasColumnName("RECer");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.StudentId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("studentId");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasPrincipalKey(p => p.MajorId)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK__Student__MajorId__56E8E7AB");
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentC__3213E83FF06CBB18");

            entity.ToTable("StudentClass");

            entity.HasIndex(e => e.StudentClassId, "UQ__StudentC__114B99029C3B1AA7").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendingStatus)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ClassId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.FinalScore).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Gpalevel).HasColumnName("GPALevel");
            entity.Property(e => e.StudentClassId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("studentClassId");
            entity.Property(e => e.StudentId)
                .IsRequired()
                .HasMaxLength(36);

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
            entity.HasKey(e => e.Id).HasName("PK__StudentM__3213E83FC1189A06");

            entity.ToTable("StudentModule");

            entity.HasIndex(e => e.StudentModuleId, "UQ__StudentM__4A54FA66534D37ED").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ModuleId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.ModuleScore).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StudentId)
                .IsRequired()
                .HasMaxLength(36);
            entity.Property(e => e.StudentModuleId)
                .IsRequired()
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
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83FDA42627F");

            entity.ToTable("Syllabus");

            entity.HasIndex(e => e.SyllabusId, "UQ__Syllabus__915EDF81F1BA128D").IsUnique();

            entity.HasIndex(e => e.TopicCode, "topic_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendeeNumber).HasColumnName("attendee_number");
            entity.Property(e => e.CourseObjective)
                .IsRequired()
                .HasColumnName("course_objective");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Days).HasColumnName("days");
            entity.Property(e => e.DeliveryPrinciple)
                .IsRequired()
                .HasColumnName("delivery_principle");
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
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("syllabusId");
            entity.Property(e => e.TechnicalRequirement)
                .IsRequired()
                .HasColumnName("technical_requirement");
            entity.Property(e => e.TopicCode)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("topic_code");
            entity.Property(e => e.TopicName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("topic_name");
            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<SyllabusDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83FA9CFD086");

            entity.ToTable("SyllabusDay");

            entity.HasIndex(e => e.SyllabusDayId, "UQ__Syllabus__6F1A138115A4EEA2").IsUnique();

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
                .IsRequired()
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
                .HasConstraintName("FK__SyllabusD__sylla__5BAD9CC8");
        });

        modelBuilder.Entity<SyllabusUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syllabus__3213E83F3CE11C45");

            entity.ToTable("SyllabusUnit");

            entity.HasIndex(e => e.SyllabusUnitId, "UQ__Syllabus__D5A44901787EC572").IsUnique();

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
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SyllabusDayId)
                .HasMaxLength(36)
                .HasColumnName("syllabus_day_id");
            entity.Property(e => e.SyllabusUnitId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("syllabusUnitId");
            entity.Property(e => e.UnitNo).HasColumnName("unit_no");

            entity.HasOne(d => d.SyllabusDay).WithMany(p => p.SyllabusUnits)
                .HasPrincipalKey(p => p.SyllabusDayId)
                .HasForeignKey(d => d.SyllabusDayId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SyllabusU__sylla__5CA1C101");
        });

        modelBuilder.Entity<TechnicalCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83F10CBBEFC");

            entity.ToTable("TechnicalCode");

            entity.HasIndex(e => e.TechnicalCodeId, "UQ__Technica__7E6FA2951436642C").IsUnique();

            entity.HasIndex(e => e.TechnicalCodeName, "technical_code_unique_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.TechnicalCodeId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("technicalCodeId");
            entity.Property(e => e.TechnicalCodeName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<TechnicalGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technica__3213E83F88D71E6C");

            entity.ToTable("TechnicalGroup");

            entity.HasIndex(e => e.TechnicalGroupName, "TechnicalGroupNameUnique").IsUnique();

            entity.HasIndex(e => e.TechnicalGroupId, "UQ__Technica__07542F359E334825").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.TechnicalGroupId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("technicalGroupId");
            entity.Property(e => e.TechnicalGroupName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<TrainingMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83FA61183A9");

            entity.ToTable("TrainingMaterial");

            entity.HasIndex(e => e.TrainingMaterialId, "UQ__Training__E3CB00D6DA2EE0A2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.FileName)
                .IsRequired()
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
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TrainingMaterialId)
                .IsRequired()
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
                .HasConstraintName("FK__TrainingM__unit___5D95E53A");
        });

        modelBuilder.Entity<TrainingProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3213E83F8CFCAAEA");

            entity.ToTable("TrainingProgram");

            entity.HasIndex(e => e.TrainingProgramCode, "UQ__Training__8245E6A3D87C65F4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TechnicalCodeId).HasMaxLength(36);
            entity.Property(e => e.TechnicalGroupId).HasMaxLength(36);
            entity.Property(e => e.TrainingProgramCode)
                .IsRequired()
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
            entity.HasKey(e => new { e.ProgramId, e.ModuleId }).HasName("PK__Training__1792272264D6D473");

            entity.ToTable("TrainingProgramModule");

            entity.HasIndex(e => e.ProgramId, "UQ__Training__752560592D93E228").IsUnique();

            entity.Property(e => e.ProgramId).HasMaxLength(36);
            entity.Property(e => e.ModuleId).HasMaxLength(36);

            entity.HasOne(d => d.Module).WithMany(p => p.TrainingProgramModules)
                .HasPrincipalKey(p => p.ModuleId)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_TrainingProgramModule_Module");

            entity.HasOne(d => d.Program).WithOne(p => p.TrainingProgramModule)
                .HasPrincipalKey<TrainingProgram>(p => p.TrainingProgramCode)
                .HasForeignKey<TrainingProgramModule>(d => d.ProgramId)
                .HasConstraintName("FK_TrainingProgramModule_Program");
        });

        modelBuilder.Entity<TrainingProgramSyllabus>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SyllabusId, e.TrainingProgramCode }).HasName("PK__Training__2EE7C7119510DB57");

            entity.ToTable("TrainingProgramSyllabus");

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
                .HasConstraintName("FK__TrainingP__sylla__634EBE90");

            entity.HasOne(d => d.TrainingProgramCodeNavigation).WithMany(p => p.TrainingProgramSyllabi)
                .HasPrincipalKey(p => p.TrainingProgramCode)
                .HasForeignKey(d => d.TrainingProgramCode)
                .HasConstraintName("FK__TrainingP__train__6442E2C9");
        });

        modelBuilder.Entity<UnitChapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UnitChap__3213E83F488B7386");

            entity.ToTable("UnitChapter");

            entity.HasIndex(e => e.UnitChapterId, "UQ__UnitChap__A4B0833C95E26A66").IsUnique();

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
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OutputStandardId)
                .HasMaxLength(36)
                .HasColumnName("output_standard_id");
            entity.Property(e => e.SyllabusUnitId)
                .HasMaxLength(36)
                .HasColumnName("syllabus_unit_id");
            entity.Property(e => e.UnitChapterId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("unitChapterId");

            entity.HasOne(d => d.DeliveryType).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.DeliveryTypeId)
                .HasForeignKey(d => d.DeliveryTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UnitChapt__deliv__65370702");

            entity.HasOne(d => d.OutputStandard).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.OutputStandardId)
                .HasForeignKey(d => d.OutputStandardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UnitChapt__outpu__662B2B3B");

            entity.HasOne(d => d.SyllabusUnit).WithMany(p => p.UnitChapters)
                .HasPrincipalKey(p => p.SyllabusUnitId)
                .HasForeignKey(d => d.SyllabusUnitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UnitChapt__sylla__671F4F74");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F1B69F5D9");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "EmailUnique").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__User__CB9A1CFEAED005AC").IsUnique();

            entity.HasIndex(e => e.Username, "UsernameUnique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.FullName).IsRequired();
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(36);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Phone).IsRequired();
            entity.Property(e => e.RoleId).HasMaxLength(36);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("userId");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasPrincipalKey(p => p.RoleId)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_Role");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3214EC077D0B9CC8");

            entity.ToTable("UserPermission");

            entity.HasIndex(e => e.UserPermissionId, "UQ__UserPerm__0E30AD2EEF49F117").IsUnique();

            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.UserPermissionId)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("userPermissionId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
