using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingProgramManagementAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendeeType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    attendeeTypeId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AttendeeTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendee__3213E83F84282634", x => x.id);
                    table.UniqueConstraint("AK_AttendeeType_attendeeTypeId", x => x.attendeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deliveryTypeId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    descriptions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__3213E83FC15D3F86", x => x.id);
                    table.UniqueConstraint("AK_DeliveryType_deliveryTypeId", x => x.deliveryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emailTemplateId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailTem__3213E83FE2C75866", x => x.id);
                    table.UniqueConstraint("AK_EmailTemplate_emailTemplateId", x => x.emailTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "fsu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fsuId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__fsu__3213E83F6BA5BBB1", x => x.id);
                    table.UniqueConstraint("AK_fsu_fsuId", x => x.fsuId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    locationId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Location__3214EC07D1938511", x => x.Id);
                    table.UniqueConstraint("AK_Location_locationId", x => x.locationId);
                });

            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    majorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Major__3213E83F9673AB07", x => x.id);
                    table.UniqueConstraint("AK_Major_majorId", x => x.majorId);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    moduleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Module__3213E83F9FA0D766", x => x.id);
                    table.UniqueConstraint("AK_Module_moduleId", x => x.moduleId);
                });

            migrationBuilder.CreateTable(
                name: "OutputStandard",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    outputStandardId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descriptions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OutputSt__3213E83F10CD5257", x => x.id);
                    table.UniqueConstraint("AK_OutputStandard_outputStandardId", x => x.outputStandardId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3213E83FF479330D", x => x.id);
                    table.UniqueConstraint("AK_Role_roleId", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Syllabus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    syllabusId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    topic_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    topic_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    attendee_number = table.Column<int>(type: "int", nullable: true),
                    level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    technical_requirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    course_objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delivery_principle = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    days = table.Column<int>(type: "int", nullable: true),
                    hours = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Syllabus__3213E83F43057209", x => x.id);
                    table.UniqueConstraint("AK_Syllabus_syllabusId", x => x.syllabusId);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalCode",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    technicalCodeId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TechnicalCodeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Technica__3213E83FE26B9A1D", x => x.id);
                    table.UniqueConstraint("AK_TechnicalCode_technicalCodeId", x => x.technicalCodeId);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalGroup",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    technicalGroupId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TechnicalGroupName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Technica__3213E83FE7BC530C", x => x.id);
                    table.UniqueConstraint("AK_TechnicalGroup_technicalGroupId", x => x.technicalGroupId);
                });

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userPermissionId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserPerm__3214EC076C80C724", x => x.Id);
                    table.UniqueConstraint("AK_UserPermission_userPermissionId", x => x.userPermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    GraduatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RECer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student__3213E83F37F48545", x => x.id);
                    table.UniqueConstraint("AK_Student_studentId", x => x.studentId);
                    table.ForeignKey(
                        name: "FK__Student__MajorId__42E1EEFE",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "majorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assignmentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    ModuleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssignmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignmentType = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assignme__3213E83F7A5E6252", x => x.id);
                    table.UniqueConstraint("AK_Assignment_assignmentId", x => x.assignmentId);
                    table.ForeignKey(
                        name: "FK_Assignment_Module",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "moduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quizId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    ModuleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quiz__3213E83FC0A5F013", x => x.id);
                    table.UniqueConstraint("AK_Quiz_quizId", x => x.quizId);
                    table.ForeignKey(
                        name: "FK_Quiz_Module",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "moduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3213E83F17DCC196", x => x.id);
                    table.UniqueConstraint("AK_User_userId", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Users_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentScheme",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assesmentSchemeId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    assignment = table.Column<double>(type: "float", nullable: true),
                    final_practice = table.Column<double>(type: "float", nullable: true),
                    final = table.Column<double>(type: "float", nullable: true),
                    final_theory = table.Column<double>(type: "float", nullable: true),
                    gpa = table.Column<double>(type: "float", nullable: true),
                    quiz = table.Column<double>(type: "float", nullable: true),
                    syllabus_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__3213E83FB978B6FB", x => x.id);
                    table.ForeignKey(
                        name: "FK__Assessmen__sylla__2EDAF651",
                        column: x => x.syllabus_id,
                        principalTable: "Syllabus",
                        principalColumn: "syllabusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyllabusDay",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    syllabusDayId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    day_no = table.Column<int>(type: "int", nullable: true),
                    syllabus_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Syllabus__3213E83F91ED0402", x => x.id);
                    table.UniqueConstraint("AK_SyllabusDay_syllabusDayId", x => x.syllabusDayId);
                    table.ForeignKey(
                        name: "FK__SyllabusD__sylla__47A6A41B",
                        column: x => x.syllabus_id,
                        principalTable: "Syllabus",
                        principalColumn: "syllabusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RolePerm__6400A1A89B80BCF9", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK__RolePermi__Permi__3F115E1A",
                        column: x => x.PermissionId,
                        principalTable: "UserPermission",
                        principalColumn: "userPermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__RolePermi__RoleI__40058253",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentModule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentModuleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    StudentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ModuleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ModuleScore = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ModuleLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentM__3213E83FC78C781B", x => x.id);
                    table.ForeignKey(
                        name: "FK_StudentModule_Module",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "moduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentModule_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scoreId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    StudentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssignmentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Score__3213E83F90A87152", x => x.id);
                    table.ForeignKey(
                        name: "FK_Score_Assignment",
                        column: x => x.AssignmentId,
                        principalTable: "Assignment",
                        principalColumn: "assignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Score_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizStudent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quizStudentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    StudentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    QuizId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizStud__3213E83FBE688614", x => x.id);
                    table.ForeignKey(
                        name: "FK_QuizStudent_Quiz",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "quizId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizStudent_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailSend",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emailSendId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    TemplateId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiverType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailSen__3213E83F3F5B0336", x => x.id);
                    table.UniqueConstraint("AK_EmailSend_emailSendId", x => x.emailSendId);
                    table.ForeignKey(
                        name: "FK_EmailSend_Sender",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EmailSend_Template",
                        column: x => x.TemplateId,
                        principalTable: "EmailTemplate",
                        principalColumn: "emailTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgram",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingProgramCode = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Days = table.Column<int>(type: "int", nullable: true),
                    Hours = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    TechnicalCodeId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    TechnicalGroupId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Training__3213E83F90EAAA70", x => x.id);
                    table.UniqueConstraint("AK_TrainingProgram_TrainingProgramCode", x => x.TrainingProgramCode);
                    table.ForeignKey(
                        name: "FK_TrainingProgram_TechnicalCode",
                        column: x => x.TechnicalCodeId,
                        principalTable: "TechnicalCode",
                        principalColumn: "technicalCodeId");
                    table.ForeignKey(
                        name: "FK_TrainingProgram_TechnicalGroup",
                        column: x => x.TechnicalGroupId,
                        principalTable: "TechnicalGroup",
                        principalColumn: "technicalGroupId");
                    table.ForeignKey(
                        name: "FK_TrainingProgram_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "SyllabusUnit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    syllabusUnitId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    duration = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    unit_no = table.Column<int>(type: "int", nullable: false),
                    syllabus_day_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Syllabus__3213E83FA8C36ECD", x => x.id);
                    table.UniqueConstraint("AK_SyllabusUnit_syllabusUnitId", x => x.syllabusUnitId);
                    table.ForeignKey(
                        name: "FK__SyllabusU__sylla__489AC854",
                        column: x => x.syllabus_day_id,
                        principalTable: "SyllabusDay",
                        principalColumn: "syllabusDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailSendStudent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emailSendStudentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    ReceiverId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReceiverType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailSen__3213E83F8CDE158B", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmailSendStudent_Email",
                        column: x => x.EmailId,
                        principalTable: "EmailSend",
                        principalColumn: "emailSendId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailSendStudent_Receiver",
                        column: x => x.ReceiverId,
                        principalTable: "Student",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    classId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ClassCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReviewBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AcceptedAttendee = table.Column<int>(type: "int", nullable: false),
                    ActualAttendee = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fsu_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    AttendeeLevelId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    TrainingProgramCode = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    PlannedAttendee = table.Column<int>(type: "int", nullable: false),
                    SlotTime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Class__3213E83FCA48A773", x => x.id);
                    table.UniqueConstraint("AK_Class_classId", x => x.classId);
                    table.ForeignKey(
                        name: "FK_Class_AttendeeType",
                        column: x => x.AttendeeLevelId,
                        principalTable: "AttendeeType",
                        principalColumn: "attendeeTypeId");
                    table.ForeignKey(
                        name: "FK_Class_FSU",
                        column: x => x.fsu_id,
                        principalTable: "fsu",
                        principalColumn: "fsuId");
                    table.ForeignKey(
                        name: "FK_Class_Location",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "locationId");
                    table.ForeignKey(
                        name: "FK_Class_TrainingProgram",
                        column: x => x.TrainingProgramCode,
                        principalTable: "TrainingProgram",
                        principalColumn: "TrainingProgramCode");
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramModule",
                columns: table => new
                {
                    ProgramId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ModuleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TrainingProgramModule_Module",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "moduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingProgramModule_Program",
                        column: x => x.ProgramId,
                        principalTable: "TrainingProgram",
                        principalColumn: "TrainingProgramCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramSyllabus",
                columns: table => new
                {
                    syllabus_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    training_program_code = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Training__CF42F2E07A552292", x => new { x.syllabus_id, x.training_program_code });
                    table.ForeignKey(
                        name: "FK__TrainingP__sylla__4F47C5E3",
                        column: x => x.syllabus_id,
                        principalTable: "Syllabus",
                        principalColumn: "syllabusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__TrainingP__train__503BEA1C",
                        column: x => x.training_program_code,
                        principalTable: "TrainingProgram",
                        principalColumn: "TrainingProgramCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitChapter",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unitChapterId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    chapter_no = table.Column<int>(type: "int", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: true),
                    is_online = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    delivery_type_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    output_standard_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    syllabus_unit_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UnitChap__3213E83FBA27657A", x => x.id);
                    table.UniqueConstraint("AK_UnitChapter_unitChapterId", x => x.unitChapterId);
                    table.ForeignKey(
                        name: "FK__UnitChapt__deliv__51300E55",
                        column: x => x.delivery_type_id,
                        principalTable: "DeliveryType",
                        principalColumn: "deliveryTypeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__UnitChapt__outpu__5224328E",
                        column: x => x.output_standard_id,
                        principalTable: "OutputStandard",
                        principalColumn: "outputStandardId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__UnitChapt__sylla__531856C7",
                        column: x => x.syllabus_unit_id,
                        principalTable: "SyllabusUnit",
                        principalColumn: "syllabusUnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClassUse__0B395E30E4C67046", x => new { x.UserId, x.ClassId });
                    table.ForeignKey(
                        name: "FK__ClassUser__Class__3493CFA7",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "classId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ClassUser__UserI__3587F3E0",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservedClass",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reservedClassId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    StudentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ClassId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reserved__3213E83F2E8E78D5", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReservedClass_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "classId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservedClass_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "StudentClass",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentClassId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    StudentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AttendingStatus = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    FinalScore = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    GPALevel = table.Column<int>(type: "int", nullable: false),
                    CertificationStatus = table.Column<int>(type: "int", nullable: false),
                    CertificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Method = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentC__3213E83FD2FCBE9E", x => x.id);
                    table.ForeignKey(
                        name: "FK_StudentClass_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "classId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClass_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingMaterial",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trainingMaterialId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    file_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    is_file = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    unit_chapter_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Training__3213E83FF678E92B", x => x.id);
                    table.ForeignKey(
                        name: "FK__TrainingM__unit___498EEC8D",
                        column: x => x.unit_chapter_id,
                        principalTable: "UnitChapter",
                        principalColumn: "unitChapterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentScheme_syllabus_id",
                table: "AssessmentScheme",
                column: "syllabus_id");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_ModuleId",
                table: "Assignment",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Assignme__52C218214C093618",
                table: "Assignment",
                column: "assignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "attendee_type_unique",
                table: "AttendeeType",
                column: "AttendeeTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Attendee__114FA69245342566",
                table: "AttendeeType",
                column: "attendeeTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Class_AttendeeLevelId",
                table: "Class",
                column: "AttendeeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_fsu_id",
                table: "Class",
                column: "fsu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Class_LocationId",
                table: "Class",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_TrainingProgramCode",
                table: "Class",
                column: "TrainingProgramCode");

            migrationBuilder.CreateIndex(
                name: "UQ__Class__7577347FF6AD2CF4",
                table: "Class",
                column: "classId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassUser_ClassId",
                table: "ClassUser",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "UQ__Delivery__72E12F1B177ADEA7",
                table: "DeliveryType",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Delivery__BA19297B6176554A",
                table: "DeliveryType",
                column: "deliveryTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailSend_SenderId",
                table: "EmailSend",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSend_TemplateId",
                table: "EmailSend",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "UQ__EmailSen__4B3B46D7A46A64B5",
                table: "EmailSend",
                column: "emailSendId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendStudent_EmailId",
                table: "EmailSendStudent",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendStudent_ReceiverId",
                table: "EmailSendStudent",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "UQ__EmailSen__2D96D8D6C3D961E7",
                table: "EmailSendStudent",
                column: "emailSendStudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__EmailTem__C443B5106EBE293E",
                table: "EmailTemplate",
                column: "emailTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__fsu__E1FCEFCA8A70E292",
                table: "fsu",
                column: "fsuId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Location__30646B6F0C1C5BC5",
                table: "Location",
                column: "locationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_MAJOR_NAME",
                table: "Major",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Major__A5B1B4B5DEA77469",
                table: "Major",
                column: "majorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Module__8EEC8E16A3D5A820",
                table: "Module",
                column: "moduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__OutputSt__BED5012DA3635214",
                table: "OutputStandard",
                column: "outputStandardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_ModuleId",
                table: "Quiz",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Quiz__CFF54C3C00F26B80",
                table: "Quiz",
                column: "quizId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizStudent_QuizId",
                table: "QuizStudent",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizStudent_StudentId",
                table: "QuizStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedClass_ClassId",
                table: "ReservedClass",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedClass_StudentId",
                table: "ReservedClass",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "UQ__Reserved__12EF4C5072FCB98D",
                table: "ReservedClass",
                column: "reservedClassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "role_name_unique",
                table: "Role",
                column: "RoleName",
                unique: true,
                filter: "[RoleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Role__CD98462B6030D1EF",
                table: "Role",
                column: "roleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__RolePerm__8AFACE1BAC74F8B5",
                table: "RolePermission",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__RolePerm__EFA6FB2E95AC195C",
                table: "RolePermission",
                column: "PermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_AssignmentId",
                table: "Score",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_StudentId",
                table: "Score",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "UQ__Score__B56A0C8CCE7E16FB",
                table: "Score",
                column: "scoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_MajorId",
                table: "Student",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "UQ__Student__4D11D63D7B074F93",
                table: "Student",
                column: "studentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_ClassId",
                table: "StudentClass",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_StudentId",
                table: "StudentClass",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "UQ__StudentC__114B9902D9CFED03",
                table: "StudentClass",
                column: "studentClassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentModule_ModuleId",
                table: "StudentModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModule_StudentId",
                table: "StudentModule",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "UQ__StudentM__4A54FA6620B2B562",
                table: "StudentModule",
                column: "studentModuleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "topic_code_unique",
                table: "Syllabus",
                column: "topic_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Syllabus__915EDF81276B6197",
                table: "Syllabus",
                column: "syllabusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusDay_syllabus_id",
                table: "SyllabusDay",
                column: "syllabus_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Syllabus__6F1A138137C61246",
                table: "SyllabusDay",
                column: "syllabusDayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusUnit_syllabus_day_id",
                table: "SyllabusUnit",
                column: "syllabus_day_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Syllabus__D5A44901F079FD64",
                table: "SyllabusUnit",
                column: "syllabusUnitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "technical_code_unique_name",
                table: "TechnicalCode",
                column: "TechnicalCodeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Technica__7E6FA295E09BE7AD",
                table: "TechnicalCode",
                column: "technicalCodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TechnicalGroupNameUnique",
                table: "TechnicalGroup",
                column: "TechnicalGroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Technica__07542F35CF70F813",
                table: "TechnicalGroup",
                column: "technicalGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingMaterial_unit_chapter_id",
                table: "TrainingMaterial",
                column: "unit_chapter_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Training__E3CB00D6B202C1D6",
                table: "TrainingMaterial",
                column: "trainingMaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgram_TechnicalCodeId",
                table: "TrainingProgram",
                column: "TechnicalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgram_TechnicalGroupId",
                table: "TrainingProgram",
                column: "TechnicalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgram_UserId",
                table: "TrainingProgram",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Training__8245E6A3B677DBC3",
                table: "TrainingProgram",
                column: "TrainingProgramCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramModule_ModuleId",
                table: "TrainingProgramModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Training__752560593A81E55E",
                table: "TrainingProgramModule",
                column: "ProgramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramSyllabus_training_program_code",
                table: "TrainingProgramSyllabus",
                column: "training_program_code");

            migrationBuilder.CreateIndex(
                name: "IX_UnitChapter_delivery_type_id",
                table: "UnitChapter",
                column: "delivery_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_UnitChapter_output_standard_id",
                table: "UnitChapter",
                column: "output_standard_id");

            migrationBuilder.CreateIndex(
                name: "IX_UnitChapter_syllabus_unit_id",
                table: "UnitChapter",
                column: "syllabus_unit_id");

            migrationBuilder.CreateIndex(
                name: "UQ__UnitChap__A4B0833C52C67D7D",
                table: "UnitChapter",
                column: "unitChapterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailUnique",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__CB9A1CFE18846C55",
                table: "User",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UsernameUnique",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__UserPerm__0E30AD2E4F65BF14",
                table: "UserPermission",
                column: "userPermissionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentScheme");

            migrationBuilder.DropTable(
                name: "ClassUser");

            migrationBuilder.DropTable(
                name: "EmailSendStudent");

            migrationBuilder.DropTable(
                name: "QuizStudent");

            migrationBuilder.DropTable(
                name: "ReservedClass");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "StudentClass");

            migrationBuilder.DropTable(
                name: "StudentModule");

            migrationBuilder.DropTable(
                name: "TrainingMaterial");

            migrationBuilder.DropTable(
                name: "TrainingProgramModule");

            migrationBuilder.DropTable(
                name: "TrainingProgramSyllabus");

            migrationBuilder.DropTable(
                name: "EmailSend");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "UnitChapter");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "AttendeeType");

            migrationBuilder.DropTable(
                name: "fsu");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "TrainingProgram");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.DropTable(
                name: "DeliveryType");

            migrationBuilder.DropTable(
                name: "OutputStandard");

            migrationBuilder.DropTable(
                name: "SyllabusUnit");

            migrationBuilder.DropTable(
                name: "TechnicalCode");

            migrationBuilder.DropTable(
                name: "TechnicalGroup");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SyllabusDay");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Syllabus");
        }
    }
}
