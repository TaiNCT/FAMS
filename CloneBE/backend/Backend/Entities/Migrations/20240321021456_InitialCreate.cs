﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
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
                    table.PrimaryKey("PK__Attendee__3213E83F9919DF08", x => x.id);
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
                    table.PrimaryKey("PK__Delivery__3213E83F97C1CCC9", x => x.id);
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
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailTem__3213E83FC51A311E", x => x.id);
                    table.UniqueConstraint("AK_EmailTemplate_emailTemplateId", x => x.emailTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "fsu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fsuId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__fsu__3213E83F64A66A7E", x => x.id);
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
                    table.PrimaryKey("PK__Location__3214EC0778E516FB", x => x.Id);
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
                    table.PrimaryKey("PK__Major__3213E83F613E5602", x => x.id);
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
                    table.PrimaryKey("PK__Module__3213E83F19463C25", x => x.id);
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
                    table.PrimaryKey("PK__OutputSt__3213E83FFE1AA419", x => x.id);
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
                    table.PrimaryKey("PK__Role__3213E83FC8774946", x => x.id);
                    table.UniqueConstraint("AK_Role_roleId", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Syllabus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    syllabusId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    topic_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
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
                    delivery_principle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    days = table.Column<int>(type: "int", nullable: true),
                    hours = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Syllabus__3213E83FE0AED0E4", x => x.id);
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
                    table.PrimaryKey("PK__Technica__3213E83F5D376B44", x => x.id);
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
                    table.PrimaryKey("PK__Technica__3213E83F78E9E1EF", x => x.id);
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
                    table.PrimaryKey("PK__UserPerm__3214EC07C0E79A0B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    MutatableStudentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CertificationStatus = table.Column<bool>(type: "bit", nullable: false),
                    Audit = table.Column<int>(type: "int", nullable: false),
                    Mock = table.Column<double>(type: "float", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    GraduatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RECer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student__3213E83F166127A4", x => x.id);
                    table.UniqueConstraint("AK_Student_studentId", x => x.studentId);
                    table.ForeignKey(
                        name: "FK__Student__MajorId__43D61337",
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
                    table.PrimaryKey("PK__Assignme__3213E83FAFB0A0F5", x => x.id);
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
                    id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK__Quiz__3213E83FA0E025B4", x => x.id);
                    table.UniqueConstraint("AK_Quiz_quizId", x => x.quizId);
                    table.ForeignKey(
                        name: "FK_Quiz_Module",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "moduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    PermissionId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    RoleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Syllabus = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TrainingProgram = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Class = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LearningMaterial = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserManagement = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK__RolePermi__RoleI__40058253",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "roleId",
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
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK__User__3213E83FD7603D84", x => x.id);
                    table.UniqueConstraint("AK_User_userId", x => x.userId);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "roleId");
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
                    table.PrimaryKey("PK__Assessme__3213E83FCC60AC6C", x => x.id);
                    table.ForeignKey(
                        name: "FK__Assessmen__sylla__45BE5BA9",
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
                    table.PrimaryKey("PK__Syllabus__3213E83F96DB42A0", x => x.id);
                    table.UniqueConstraint("AK_SyllabusDay_syllabusDayId", x => x.syllabusDayId);
                    table.ForeignKey(
                        name: "FK__SyllabusD__sylla__5D95E53A",
                        column: x => x.syllabus_id,
                        principalTable: "Syllabus",
                        principalColumn: "syllabusId",
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
                    table.PrimaryKey("PK__StudentM__3213E83F1922A532", x => x.id);
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
                    table.PrimaryKey("PK__Score__3213E83F09BAE30F", x => x.id);
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
                    table.PrimaryKey("PK__QuizStud__3213E83F47D940D4", x => x.id);
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
                    table.PrimaryKey("PK__EmailSen__3213E83F3E3F0F74", x => x.id);
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
                    table.PrimaryKey("PK__Training__3213E83FAE168522", x => x.id);
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
                    table.PrimaryKey("PK__Syllabus__3213E83F976FDEE6", x => x.id);
                    table.UniqueConstraint("AK_SyllabusUnit_syllabusUnitId", x => x.syllabusUnitId);
                    table.ForeignKey(
                        name: "FK__SyllabusU__sylla__5E8A0973",
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
                    table.PrimaryKey("PK__EmailSen__3213E83F60B657A4", x => x.id);
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
                    SlotTime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TotalHours = table.Column<double>(type: "float", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Class__3213E83F5E231634", x => x.id);
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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    syllabus_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    training_program_code = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Training__2EE7C7116A349350", x => new { x.id, x.syllabus_id, x.training_program_code });
                    table.ForeignKey(
                        name: "FK__TrainingP__sylla__65370702",
                        column: x => x.syllabus_id,
                        principalTable: "Syllabus",
                        principalColumn: "syllabusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__TrainingP__train__662B2B3B",
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
                    table.PrimaryKey("PK__UnitChap__3213E83FEE1207B2", x => x.id);
                    table.UniqueConstraint("AK_UnitChapter_unitChapterId", x => x.unitChapterId);
                    table.ForeignKey(
                        name: "FK__UnitChapt__deliv__671F4F74",
                        column: x => x.delivery_type_id,
                        principalTable: "DeliveryType",
                        principalColumn: "deliveryTypeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__UnitChapt__outpu__690797E6",
                        column: x => x.output_standard_id,
                        principalTable: "OutputStandard",
                        principalColumn: "outputStandardId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__UnitChapt__sylla__69FBBC1F",
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
                    table.PrimaryKey("PK__ClassUse__0B395E30283D2B0E", x => new { x.UserId, x.ClassId });
                    table.ForeignKey(
                        name: "FK__ClassUser__Class__4B7734FF",
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
                    table.PrimaryKey("PK__Reserved__3213E83F27E8B567", x => x.id);
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
                    AttendingStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    FinalScore = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    GPALevel = table.Column<int>(type: "int", nullable: false),
                    CertificationStatus = table.Column<int>(type: "int", nullable: false),
                    CertificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Method = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentC__3213E83F82F467D8", x => x.id);
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
                    table.PrimaryKey("PK__Training__3213E83F393D468D", x => x.id);
                    table.ForeignKey(
                        name: "FK__TrainingM__unit___5F7E2DAC",
                        column: x => x.unit_chapter_id,
                        principalTable: "UnitChapter",
                        principalColumn: "unitChapterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "id", "AcceptedAttendee", "ActualAttendee", "ApprovedBy", "ApprovedDate", "AttendeeLevelId", "ClassCode", "classId", "ClassName", "ClassStatus", "CreatedBy", "CreatedDate", "Duration", "EndDate", "EndTime", "fsu_id", "LocationId", "PlannedAttendee", "ReviewBy", "ReviewDate", "SlotTime", "StartDate", "StartTime", "TotalDays", "TotalHours", "TrainingProgramCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 2, 5, "1", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(580), null, "CL1", "176d899b-1c24-49fc-baf1-8755ef89f1b3", "Web Development Fundamentals", "Finished", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(573), 30, new DateOnly(2022, 5, 3), new TimeOnly(10, 24, 22), null, null, 10, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(583), "11", new DateOnly(2022, 1, 1), new TimeOnly(6, 20, 25), 0, 0.0, null, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(574) },
                    { 2, 2, 5, "1", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(594), null, "CL2", "4fb5a126-7a1a-4dc5-ae48-689aa5f26464", "Advanced Web Development", "In class", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(587), 45, new DateOnly(2022, 12, 2), new TimeOnly(10, 24, 22), null, null, 10, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(595), "11", new DateOnly(2022, 2, 28), new TimeOnly(6, 20, 25), 0, 0.0, null, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(587) },
                    { 3, 2, 5, "1", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(599), null, "CL3", "a5c34e23-2b1f-4ef6-80b9-78ac176d091e", "Mobile App Development Basics", "In class", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(597), 23, new DateOnly(2023, 6, 1), new TimeOnly(10, 24, 22), null, null, 10, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(600), "11", new DateOnly(2023, 3, 17), new TimeOnly(6, 20, 25), 0, 0.0, null, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(598) },
                    { 4, 2, 5, "1", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(604), null, "CL4", "6bfb28e9-10c2-4a02-9755-c1fcb9a01d14", "Data Science Essentials", "Finished", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(603), 15, new DateOnly(2022, 11, 10), new TimeOnly(10, 24, 22), null, null, 10, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(605), "11", new DateOnly(2022, 5, 27), new TimeOnly(6, 20, 25), 0, 0.0, null, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(603) },
                    { 5, 2, 5, "1", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(609), null, "CL5", "e4e338a8-4413-4fb6-8652-990e20c40526", "Cybersecurity Fundamentals", "In class", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(607), 30, new DateOnly(2022, 2, 28), new TimeOnly(10, 24, 22), null, null, 10, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(610), "11", new DateOnly(2022, 1, 2), new TimeOnly(6, 20, 25), 0, 0.0, null, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(608) },
                    { 6, 2, 5, "1", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(614), null, "CL6", "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9", "Artificial Intelligence Basics", "Finished", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(612), 9, new DateOnly(2012, 8, 23), new TimeOnly(10, 24, 22), null, null, 10, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(616), "11", new DateOnly(2022, 6, 23), new TimeOnly(6, 20, 25), 0, 0.0, null, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(613) }
                });

            migrationBuilder.InsertData(
                table: "Major",
                columns: new[] { "id", "majorId", "name" },
                values: new object[,]
                {
                    { 1, "IT101", "Information Technology" },
                    { 2, "AS101", "Applied Science" },
                    { 3, "TC101", "Telecommunication" },
                    { 4, "ER101", "Economy research" }
                });

            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "id", "CreatedBy", "CreatedDate", "moduleId", "ModuleName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Dinh The Vinh", new DateTime(2024, 3, 1, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(506), "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40", "Python programming", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(511) },
                    { 2, "Dinh The Vinh", new DateTime(2024, 2, 28, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(513), "b684c89f-7b7e-4145-b345-9347a67673a3", "C# programming", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(514) },
                    { 3, "Dinh The Vinh", new DateTime(2024, 2, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(516), "f74e3a7a-312f-4f80-86cb-25f7d52735bc", "OOP Module", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(516) },
                    { 4, "Dinh The Vinh", new DateTime(2024, 2, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(518), "9bb0a5cb-6bf6-418b-b549-4b3d8abbebb7", "Cyber security", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(519) },
                    { 5, "Dinh The Vinh", new DateTime(2024, 2, 25, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(521), "315fd315-6764-4514-a289-569c07b91894", "Fundamental programmng", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(521) },
                    { 6, "Dinh The Vinh", new DateTime(2024, 2, 23, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(523), "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", "Lua programming", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(523) },
                    { 7, "Dinh The Vinh", new DateTime(2024, 2, 28, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(525), "09cf6935-9c54-49c5-8a48-202268ad4f55", "C++ programming", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(525) },
                    { 8, "Dinh The Vinh", new DateTime(2024, 2, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(527), "5488d35d-0a1e-4634-898e-92dbde019029", "Project Management", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(527) },
                    { 9, "Dinh The Vinh", new DateTime(2024, 2, 20, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(529), "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", "DevOps practices", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(530) },
                    { 10, "Dinh The Vinh", new DateTime(2024, 2, 19, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(531), "c8a4b3cc-78c5-4f20-804e-d617c35aa769", "ASP.NET Fundamentals", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(532) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "roleId", "RoleName", "Title" },
                values: new object[,]
                {
                    { 1, "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8642), "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8657), "4463c0cd-ff13-441c-8cd5-5e68961ec70b", "Administrator", "Admin" },
                    { 2, "2", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8660), "3", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8661), "81e3b0e8-02f1-4f51-bb68-5b6e465a45b2", "Standard", "User" },
                    { 3, "3", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8662), "3", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8663), "e2353151-51ec-41f7-907a-fc1d60f15a6d", "Manager", "Manager" }
                });

            migrationBuilder.InsertData(
                table: "TechnicalCode",
                columns: new[] { "id", "Description", "technicalCodeId", "TechnicalCodeName" },
                values: new object[,]
                {
                    { 1, "Technical 1", "7a9b5e84-1a8c-4e57-bfc7-23dcb6d78e92", "TECH1" },
                    { 2, "Technical 2", "4f58ebf5-79d8-4fb9-bd7b-12119bfa3171", "TECH2" },
                    { 3, "Technical 3", "ac2a35a0-c9d7-4a19-b1d4-0f0e6c1f14c7", "TECH3" }
                });

            migrationBuilder.InsertData(
                table: "TechnicalGroup",
                columns: new[] { "id", "Description", "technicalGroupId", "TechnicalGroupName" },
                values: new object[,]
                {
                    { 1, "Group1", "e792ad2b-9d75-46f3-a1f0-eb3184372f92", "GRTECH1" },
                    { 2, "Group2", "3fc26468-3fd0-49ff-bde4-0b2484aa3c3f", "GRTECH2" },
                    { 3, "Group3", "b9358a53-d1c1-4d3d-8f22-1e75a8a9a6d8", "GRTECH3" }
                });

            migrationBuilder.InsertData(
                table: "Assignment",
                columns: new[] { "id", "assignmentId", "AssignmentName", "AssignmentType", "CreatedBy", "CreatedDate", "Description", "DueDate", "ModuleId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "7e18a9ef-3723-472b-afd5-6786c4545d54", "Practice 1", 1, "Dinh The Vinh", new DateTime(2024, 3, 1, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(692), "Assignment 1 About", new DateTime(2024, 3, 25, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(691), "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(693) },
                    { 2, "a11e94d1-ebec-48d5-84e6-7c8589a1851f", "Practice 2", 2, "Dinh The Vinh", new DateTime(2024, 3, 1, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(696), "Assignment 2 About", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(695), "b684c89f-7b7e-4145-b345-9347a67673a3", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(696) },
                    { 3, "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", "Practice 3", 3, "Dinh The Vinh", new DateTime(2024, 3, 1, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(699), "Assignment 3 About", new DateTime(2024, 3, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(699), "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(700) },
                    { 4, "3f257a47-0c8b-4b06-aa0d-487d03e83db7", "Practice Final", 3, "Dinh The Vinh", new DateTime(2024, 3, 1, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(702), "Practice Final About", new DateTime(2024, 3, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(701), "c8a4b3cc-78c5-4f20-804e-d617c35aa769", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(702) }
                });

            migrationBuilder.InsertData(
                table: "Quiz",
                columns: new[] { "id", "CreateDate", "CreatedBy", "ModuleId", "quizId", "QuizName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(736), "Dinh The Vinh", "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", "HTML", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(738) },
                    { 2, new DateTime(2024, 2, 29, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(740), "Dinh The Vinh", "5488d35d-0a1e-4634-898e-92dbde019029", "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "CSS", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(741) },
                    { 3, new DateTime(2024, 2, 28, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(742), "Dinh The Vinh", "c8a4b3cc-78c5-4f20-804e-d617c35aa769", "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "Quiz 3", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(743) },
                    { 4, new DateTime(2024, 2, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(773), "Dinh The Vinh", "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", "f5b34985-2322-4884-9d29-3d550b2cf0a4", "Quiz 4", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(774) },
                    { 5, new DateTime(2024, 2, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(775), "Dinh The Vinh", "c8a4b3cc-78c5-4f20-804e-d617c35aa769", "71ac61b5-5991-4977-a9e1-38235d18b7c5", "Quiz 5", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(776) },
                    { 6, new DateTime(2024, 2, 25, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(777), "Dinh The Vinh", "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "Quiz 6", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(778) },
                    { 7, new DateTime(2024, 3, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(779), "Dinh The Vinh", "09cf6935-9c54-49c5-8a48-202268ad4f55", "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", "Quiz Final", "", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(780) }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "id", "Address", "Area", "Audit", "CertificationDate", "CertificationStatus", "DOB", "Email", "FAAccount", "FullName", "Gender", "GPA", "GraduatedDate", "JoinedDate", "MajorId", "Mock", "MutatableStudentID", "Phone", "RECer", "Status", "studentId", "Type", "University" },
                values: new object[,]
                {
                    { 1, "{\"permanent_res\":\"Vietnam, Ho Chi Minh city\",\"location\":\"Ho Chi Minh city\"}", "Ho Chi Minh", 0, null, false, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9244), "vinh@fpt.edu.vn", "DTVinh12223", "Đinh Thế Vinh", "Male", 88m, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9249), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9249), "IT101", 0.0, "STD1001", "111-232-1312", "Ho Hai Quang", "In Class", "cd9fe541-1c6f-4e9e-a94b-1ff748186975", 11, "Greenwich" },
                    { 2, "{\"permanent_res\":\"Vietnam, Ho Chi Minh city\",\"location\":\"Ho Chi Minh city\"}", "Ho Chi Minh city", 0, null, false, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9334), "v122@fpt.edu.vn", "HHSon2k1", "Hoàng Hải Sơn", "Male", 22m, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9336), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9347), "AS101", 0.0, "STD1002", "111-111-123", "Tran Tat Nghia", "In Class", "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", 23, "FPT" },
                    { 3, "{\"permanent_res\":\"Vietnam, Ha Noi\",\"location\":\"Ha Noi\"}", "Da Nang", 0, null, false, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9397), "add112@fpt.edu.vn", "DungNQ111", "Nguyễn Quang Dũng", "Male", 12m, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9399), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9400), "TC101", 0.0, "STD1003", "111-111-33", "Nguyen Quang Dung", "In Class", "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", 41, "RMIT" },
                    { 4, "{\"permanent_res\":\"United States\",\"location\":\"New York\"}", "Da Nang", 0, null, false, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9432), "camecam@fpt.edu.vn", "CarmOD1k9", "Carmila Odesn", "Male", 32m, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9434), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9434), "TC101", 0.0, "STD1004", "111-111-2411", "Ho Hai Quang", "Inactive", "3dc8f1f1-0a7a-4db4-91c3-98b93ec1b2a3", 2, "Hutech" },
                    { 5, "{\"permanent_res\":\"Finland\",\"location\":\"Tampere\"}", "Hue", 0, null, false, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9489), "hestamp@fpt.edu.vn", "HelT112", "Helsinji Tampe", "Male", 88m, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9490), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9490), "AS101", 0.0, "STD1005", "111-111-2411", "Tran Tat Nghia", "In Class", "97d39a95-2e4b-437f-a032-35e6357f06aa", 22, "Tampere" },
                    { 6, "{\"permanent_res\":\"Finland\",\"location\":\"Helsinki\"}", "Helsinki", 0, null, false, new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "example1@example.com", "example1", "John Doe", "Female", 75m, new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AS101", 0.0, "STD1006", "123-456-7890", "Alice Johnson", "Inactive", "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", 18, "Helsinki" },
                    { 7, "{\"permanent_res\":\"Finland\",\"location\":\"Turku\"}", "Turku", 0, null, false, new DateTime(2001, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "example2@example.com", "example2", "Jane Smith", "Male", 92m, new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ER101", 0.0, "STD1007", "987-654-3210", "Bob Anderson", "In Class", "d269b62c-c9dc-4d8f-8b3c-602d88e72438", 20, "Turku" },
                    { 8, "{\"permanent_res\":\"Finland\",\"location\":\"Oulu\"}", "Oulu", 0, null, false, new DateTime(1999, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "example3@example.com", "example3", "Emma Johnson", "Female", 85m, new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AS101", 0.0, "STD1008", "456-789-0123", "David Williams", "Inactive", "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", 21, "Oulu" },
                    { 9, "{\"permanent_res\":\"Finland\",\"location\":\"Vaasa\"}", "Vaasa", 0, null, false, new DateTime(2002, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "example4@example.com", "example4", "Michael Brown", "Male", 68m, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ER101", 0.0, "STD1009", "789-012-3456", "Emily Garcia", "In Class", "d5d5c72c-6b4a-4299-bc57-29cb8120e118", 19, "Vaasa" },
                    { 10, "{\"permanent_res\":\"Finland\",\"location\":\"Tampere\"}", "Tampere", 0, null, false, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9938), "hestamp@fpt.edu.vn", "HelT112", "Helsinji Tampe", "Male", 88m, new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9940), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(9941), "TC101", 0.0, "STD1010", "111-111-2411", "Tran Tat Nghia", "In Class", "e8011711-9367-404e-b0a1-3cfa0e54f015", 22, "Tampere" },
                    { 11, "{\"permanent_res\":\"Finland\",\"location\":\"Espoo\"}", "Espoo", 0, null, false, new DateTime(2003, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "example6@example.com", "example6", "Olivia Miller", "Female", 81m, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AS101", 0.0, "STD1011", "555-555-5555", "Daniel Moore", "Inactive", "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", 23, "Espoo" },
                    { 12, "{\"permanent_res\":\"Finland\",\"location\":\"Jyväskylä\"}", "Jyväskylä", 0, null, false, new DateTime(2004, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "example7@example.com", "example7", "William Wilson", "Male", 95m, new DateTime(2027, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "TC101", 0.0, "STD1012", "222-222-2222", "Sophia Taylor", "In Class", "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", 24, "Jyväskylä" },
                    { 13, "{\"permanent_res\":\"Finland\",\"location\":\"Kuopio\"}", "Kuopio", 0, null, false, new DateTime(2005, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "example8@example.com", "example8", "Liam Brown", "Male", 72m, new DateTime(2028, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT101", 0.0, "STD1013", "333-333-3333", "Mia Clark", "Inactive", "573e5801-f47e-4a82-bcb5-90fc722e4d4f", 25, "Kuopio" },
                    { 14, "{\"permanent_res\":\"Finland\",\"location\":\"Rovaniemi\"}", "Rovaniemi", 0, null, false, new DateTime(2006, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "example9@example.com", "example9", "Noah Anderson", "Male", 88m, new DateTime(2029, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT101", 0.0, "STD1014", "444-444-4444", "Ethan Adams", "In Class", "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", 26, "Rovaniemi" },
                    { 15, "{\"permanent_res\":\"Finland\",\"location\":\"Lahti\"}", "Lahti", 0, null, false, new DateTime(2007, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "example10@example.com", "example10", "Sophia Wilson", "Female", 79m, new DateTime(2030, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TC101", 0.0, "STD1015", "666-666-6666", "Logan Baker", "Inactive", "b739b16a-24eb-49bb-b57a-86f8c3c9c2fc", 27, "Lahti" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "Address", "Avatar", "CreatedBy", "CreatedDate", "DOB", "Email", "FullName", "Gender", "ModifiedBy", "ModifiedDate", "Password", "Phone", "RoleId", "Status", "userId", "Username" },
                values: new object[,]
                {
                    { 1, "Something", "avatar.png", "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8893), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8895), "email@gmail.com", "Dinh The Vinh", "Female", "2", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8890), "password", "111-111-1111", "4463c0cd-ff13-441c-8cd5-5e68961ec70b", false, "4463c0cdff13441c8cd55e68961ec70b", "dtvin123" },
                    { 2, "Something", "avatar.png", "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8902), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8903), "email12@gmail.com", "Nguyen Tan Phat", "Male", "2", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8901), "password", "111-111-1111", "4463c0cd-ff13-441c-8cd5-5e68961ec70b", false, "81e3b0e802f14f51bb685b6e465a45b2", "phatn111" },
                    { 3, "Something", "avatar.png", "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8906), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8906), "emai22l@gmail.com", "Hoang DUng", "Male", "2", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8905), "password", "111-111-1111", "81e3b0e8-02f1-4f51-bb68-5b6e465a45b2", false, "e235315151ec41f7907afc1d60f15a6d", "hd2k211" },
                    { 4, "Something", "avatar.png", "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8910), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8910), "emai23l@gmail.com", "Tan Phat", "Male", "2", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8909), "password", "111-111-1111", "81e3b0e8-02f1-4f51-bb68-5b6e465a45b2", false, "d915d11d38de42a2b6b39ffebbc5bc1d", "tanphatdd" },
                    { 5, "Something", "avatar.png", "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8913), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8914), "emai111l@gmail.com", "Truong Pham", "Female", "2", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8913), "password", "111-111-1111", "e2353151-51ec-41f7-907a-fc1d60f15a6d", false, "99b2bc17dbec476e9a99b34a95dd5b26", "Tpham111" },
                    { 6, "Something", "avatar.png", "1", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8917), new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8917), "em444ail@gmail.com", "Hoang Tri", "Female", "2", new DateTime(2024, 3, 21, 9, 14, 55, 405, DateTimeKind.Local).AddTicks(8916), "password", "111-111-1111", "e2353151-51ec-41f7-907a-fc1d60f15a6d", false, "a3db3e4e0f25445282b2e61a32c582a4", "tien2k2" }
                });

            migrationBuilder.InsertData(
                table: "QuizStudent",
                columns: new[] { "id", "QuizId", "quizStudentId", "Score", "StudentId", "SubmissionDate" },
                values: new object[,]
                {
                    { 1, "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", "aab179ec-9467-4db9-a17e-2c7ef6405d3d", 23m, "573e5801-f47e-4a82-bcb5-90fc722e4d4f", new DateTime(2024, 3, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(814) },
                    { 2, "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "60ac2dcf-7a67-4fa5-bca3-c4e59d101fb7", 56m, "97d39a95-2e4b-437f-a032-35e6357f06aa", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(817) },
                    { 3, "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "a86cb2d2-0e32-4326-aa9d-5d417a96cd25", 82m, "573e5801-f47e-4a82-bcb5-90fc722e4d4f", new DateTime(2024, 3, 31, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(818) },
                    { 4, "f5b34985-2322-4884-9d29-3d550b2cf0a4", "71e684c2-ef11-45ad-89cb-832fd28928b3", 77m, "97d39a95-2e4b-437f-a032-35e6357f06aa", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(820) },
                    { 5, "71ac61b5-5991-4977-a9e1-38235d18b7c5", "eba6827d-f367-41b1-88bf-414bd56fe53c", 52m, "e8011711-9367-404e-b0a1-3cfa0e54f015", new DateTime(2024, 3, 25, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(822) },
                    { 6, "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "14b7f4a6-d13e-4c87-b344-37613c46e91d", 63m, "e8011711-9367-404e-b0a1-3cfa0e54f015", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(828) },
                    { 7, "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", "c3b869df-1b82-4318-95f6-7b31780c107e", 100m, "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", new DateTime(2024, 4, 3, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(830) },
                    { 8, "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "6fbc7b7a-308b-4a49-bd52-99ff76a07094", 98m, "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", new DateTime(2024, 3, 28, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(831) },
                    { 9, "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "93bfb29e-4f1f-4f69-858e-92f4f6ed8ef0", 86m, "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", new DateTime(2024, 3, 29, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(833) },
                    { 10, "f5b34985-2322-4884-9d29-3d550b2cf0a4", "314ff57a-1a7f-4dd5-9ff0-2bdcf163fc5f", 23m, "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", new DateTime(2024, 4, 10, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(835) },
                    { 11, "71ac61b5-5991-4977-a9e1-38235d18b7c5", "75b01c5b-363b-4d39-b2d2-93bf41e95a4a", 67m, "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", new DateTime(2024, 3, 30, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(837) },
                    { 12, "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "e0c6dc55-6c79-4e2b-a1e3-38f70062d1f9", 34m, "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", new DateTime(2024, 3, 23, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(838) },
                    { 13, "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", "7f85e30e-2e5b-4c51-b91e-2213b0246709", 75m, "d5d5c72c-6b4a-4299-bc57-29cb8120e118", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(840) },
                    { 14, "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "2eab2e34-3454-4315-818a-c2284462b4c6", 99m, "d5d5c72c-6b4a-4299-bc57-29cb8120e118", new DateTime(2024, 3, 31, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(841) },
                    { 15, "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "a2d3d344-7e50-4cb2-9ae6-d6020df012fb", 89m, "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", new DateTime(2024, 4, 10, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(843) },
                    { 16, "f5b34985-2322-4884-9d29-3d550b2cf0a4", "53944115-bbb0-4eb8-b437-5eaf20b0fd1e", 49m, "d269b62c-c9dc-4d8f-8b3c-602d88e72438", new DateTime(2024, 3, 29, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(844) },
                    { 17, "71ac61b5-5991-4977-a9e1-38235d18b7c5", "5a39a8fc-24d3-46e0-94ed-5e8f8a3b17b7", 76m, "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", new DateTime(2024, 3, 30, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(846) },
                    { 18, "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "18686b3f-042b-4b13-a3c5-67b3f9a7eab0", 87m, "d269b62c-c9dc-4d8f-8b3c-602d88e72438", new DateTime(2024, 3, 29, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(847) },
                    { 19, "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "3064c949-2940-4e61-aa94-c0e1a3927754", 67m, "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", new DateTime(2024, 4, 13, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(849) },
                    { 20, "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "d6b0b963-4d06-4908-b479-69c097e9295a", 87m, "d269b62c-c9dc-4d8f-8b3c-602d88e72438", new DateTime(2024, 4, 7, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(851) },
                    { 21, "f5b34985-2322-4884-9d29-3d550b2cf0a4", "0b504d2e-3681-4e76-9f3d-4f4a02eb98f7", 67m, "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", new DateTime(2024, 4, 17, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(852) },
                    { 22, "71ac61b5-5991-4977-a9e1-38235d18b7c5", "4e5dd609-2a63-4cc6-aee3-ec2ff9a2c04b", 87m, "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", new DateTime(2024, 3, 31, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(854) },
                    { 23, "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "e41dc6d5-8e94-4d27-8e32-409c9e09e623", 100m, "573e5801-f47e-4a82-bcb5-90fc722e4d4f", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(856) },
                    { 24, "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", "1c8ff533-4b5a-4870-8941-70c2fc62b63c", 23m, "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(857) },
                    { 25, "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "6942c349-e742-40d1-8409-8629a9272b89", 53m, "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 3, 24, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(859) },
                    { 26, "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "d28a8a30-6be0-4dd1-a699-49d8a88317d2", 63m, "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 3, 28, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(863) },
                    { 27, "f5b34985-2322-4884-9d29-3d550b2cf0a4", "8b51cb96-9917-4453-a499-f87dd63f5c81", 23m, "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(864) },
                    { 28, "71ac61b5-5991-4977-a9e1-38235d18b7c5", "f9c2aa52-50b7-4e7e-a75c-2a9f7b7479f4", 29m, "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 3, 31, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(867) },
                    { 29, "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "593d26cd-1e62-4f07-9c4d-f7b7c73b109d", 25m, "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(868) },
                    { 30, "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", "4a9e7e7f-7b94-4bbd-bd7d-f95b69e4f3c5", 97m, "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 4, 11, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(877) },
                    { 31, "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "b3b8d5a9-b303-4c88-8c04-77f3a03bb87b", 75m, "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 3, 29, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(879) },
                    { 32, "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "0f62e8fc-2956-4d16-a6e1-4b4d51d86780", 54m, "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 3, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(880) },
                    { 33, "f5b34985-2322-4884-9d29-3d550b2cf0a4", "e79f979c-8a08-478b-a05b-4418db6f5467", 65m, "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 3, 30, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(882) },
                    { 34, "71ac61b5-5991-4977-a9e1-38235d18b7c5", "bcc05624-9145-4b4a-a29e-c51a02efc0f4", 24m, "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 3, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(884) },
                    { 35, "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "94a42f62-ee98-4e01-b99f-f5d89aadee8c", 12m, "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(885) },
                    { 36, "a4b6eb12-22df-4eeb-bc98-7d3f2b27b67a", "2ee2d5cb-bc4f-4f79-9a8d-d98df9bb98b4", 86m, "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 4, 10, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(889) },
                    { 37, "e1af1567-8259-4b10-91bf-0f9ff57a2a42", "3d53c66f-89b3-4b92-ba41-d84d91707349", 10m, "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 4, 20, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(891) },
                    { 38, "3cfc58d0-eb6f-4725-aa63-44f065d1dbb8", "19a7d14c-0ba0-4b85-835d-9c8a454c9c6c", 33m, "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 3, 24, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(892) },
                    { 39, "f5b34985-2322-4884-9d29-3d550b2cf0a4", "10cc3b78-0e50-4e63-8367-f3fd2c61d7d1", 23m, "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 3, 29, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(895) },
                    { 40, "71ac61b5-5991-4977-a9e1-38235d18b7c5", "b1421bc3-9d47-41c7-9f4b-785d65c5867a", 87m, "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(896) },
                    { 41, "88e6f2b6-3f82-45c7-a5b9-3768b2cc2d85", "c894c187-8375-4578-bb11-4b4899ff52ab", 53m, "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 4, 6, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(898) },
                    { 42, "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", "00042f62-ee98-4e01-b99f-f5d89aadee8c", 12m, "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(876) },
                    { 43, "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", "94a2af62-ee98-4e01-b99f-f5d89aadee8c", 12m, "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(888) },
                    { 44, "8f3d5fe2-9c61-4fd5-92b2-e3c06b7c51e7", "04a42f62-ee98-4e01-b99f-f5d89aadee8c", 12m, "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(899) }
                });

            migrationBuilder.InsertData(
                table: "Score",
                columns: new[] { "id", "AssignmentId", "Score", "scoreId", "StudentId", "SubmissionDate" },
                values: new object[,]
                {
                    { 1, "7e18a9ef-3723-472b-afd5-6786c4545d54", 12m, "a0b4ed42-0d4c-41cf-9b86-84b0f8e19db7", "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", new DateTime(2024, 3, 23, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1039) },
                    { 2, "a11e94d1-ebec-48d5-84e6-7c8589a1851f", 75m, "1acbe9b9-6fc4-4e90-83cf-2143d10fbfd1", "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", new DateTime(2024, 3, 26, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1041) },
                    { 3, "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", 66m, "3b3eb3b7-b755-4429-b92a-cb9c0f5198bb", "d269b62c-c9dc-4d8f-8b3c-602d88e72438", new DateTime(2024, 3, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1043) },
                    { 4, "3f257a47-0c8b-4b06-aa0d-487d03e83db7", 96m, "c88edf8c-09b7-46cf-b2c1-d394d4f589c4", "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", new DateTime(2024, 4, 13, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1044) },
                    { 5, "7e18a9ef-3723-472b-afd5-6786c4545d54", 87m, "2a09d83e-d639-4de8-81dd-30476b30084a", "97d39a95-2e4b-437f-a032-35e6357f06aa", new DateTime(2024, 4, 2, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1046) },
                    { 6, "a11e94d1-ebec-48d5-84e6-7c8589a1851f", 64m, "4d2db7d5-46f1-4697-a540-6b6c04384e5d", "d5d5c72c-6b4a-4299-bc57-29cb8120e118", new DateTime(2024, 4, 13, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1048) },
                    { 7, "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", 60m, "a8d318c7-1767-4345-b4cb-7e7731f19fe4", "e8011711-9367-404e-b0a1-3cfa0e54f015", new DateTime(2024, 3, 30, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1050) },
                    { 8, "3f257a47-0c8b-4b06-aa0d-487d03e83db7", 90m, "c033f1a2-67c5-4f9e-b3f2-99cbf8034db1", "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", new DateTime(2024, 3, 30, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1052) },
                    { 9, "7e18a9ef-3723-472b-afd5-6786c4545d54", 100m, "2ebabf2b-5e2d-455f-b4e4-8f40cc32ad9c", "573e5801-f47e-4a82-bcb5-90fc722e4d4f", new DateTime(2024, 3, 29, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1054) },
                    { 10, "a11e94d1-ebec-48d5-84e6-7c8589a1851f", 87m, "0dc10aaf-8887-420f-b3c1-70f4987b31e2", "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", new DateTime(2024, 4, 20, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1056) },
                    { 11, "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", 78m, "c4544428-1229-49fb-bbe5-c3f607f2e0b6", "e8011711-9367-404e-b0a1-3cfa0e54f015", new DateTime(2024, 4, 9, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1058) },
                    { 12, "3f257a47-0c8b-4b06-aa0d-487d03e83db7", 69m, "e2f722f0-f570-49de-a2cc-dfb6483b7276", "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", new DateTime(2024, 4, 6, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1060) },
                    { 13, "7e18a9ef-3723-472b-afd5-6786c4545d54", 28m, "d36b67c8-0e0e-4873-80ec-b3fa0bfbb92c", "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 4, 22, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1061) },
                    { 14, "a11e94d1-ebec-48d5-84e6-7c8589a1851f", 82m, "4a8f7f7e-ec5c-4b5b-8e52-8eae6c286e63", "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 4, 1, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1063) },
                    { 15, "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", 92m, "6a5a89b8-2457-4d89-b7cb-1d255f286f47", "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 3, 30, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1064) },
                    { 16, "3f257a47-0c8b-4b06-aa0d-487d03e83db7", 72m, "7e4884a4-6b2b-47e5-a982-d5c2a697309b", "cd9fe541-1c6f-4e9e-a94b-1ff748186975", new DateTime(2024, 3, 28, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1066) },
                    { 17, "7e18a9ef-3723-472b-afd5-6786c4545d54", 77m, "e491e4a2-7cb5-4fe8-a6e0-5f2059eebf0f", "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 3, 24, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1068) },
                    { 18, "a11e94d1-ebec-48d5-84e6-7c8589a1851f", 65m, "7ef2b3c8-1e0d-434e-b1ad-6a0b3782b8cb", "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 3, 22, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1069) },
                    { 19, "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", 87m, "99e51e1c-246d-4e5d-af46-1fc605f28d8f", "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 3, 31, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1070) },
                    { 20, "3f257a47-0c8b-4b06-aa0d-487d03e83db7", 59m, "e6a8b0e7-c19a-4b56-b4b7-2ec8fc6d26f5", "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", new DateTime(2024, 4, 8, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1072) },
                    { 21, "7e18a9ef-3723-472b-afd5-6786c4545d54", 50m, "1d08c07e-8c67-4c8e-9d32-89d981f31f4b", "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 4, 13, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1074) },
                    { 22, "a11e94d1-ebec-48d5-84e6-7c8589a1851f", 23m, "60be68a2-b3fd-44b0-a187-d2b11b1d23d7", "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 3, 22, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1075) },
                    { 23, "e921a7f7-78e8-4f13-82d5-2d2b2c35ab44", 34m, "1b33f1d4-1ac4-492d-aa3e-b086de84dab1", "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 3, 28, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1077) },
                    { 24, "3f257a47-0c8b-4b06-aa0d-487d03e83db7", 45m, "f0f23e62-9a67-4a85-83ff-d8a5b2f83dd4", "ea21b42d-89a2-4e9e-a6b6-d6a9c7b52480", new DateTime(2024, 3, 27, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(1079) }
                });

            migrationBuilder.InsertData(
                table: "StudentClass",
                columns: new[] { "id", "AttendingStatus", "CertificationDate", "CertificationStatus", "ClassId", "FinalScore", "GPALevel", "Method", "Result", "studentClassId", "StudentId" },
                values: new object[,]
                {
                    { 1, "Active", null, 0, "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9", 55m, 3, 3, 10, "2f1e6d1d-7a0a-4b42-b4d1-25f0e7b4a4f1", "815dd6f4-2d41-4c6c-a032-5e78a1cf065b" },
                    { 2, "Active", null, 0, "e4e338a8-4413-4fb6-8652-990e20c40526", 35m, 3, 1, 92, "d32f4d16-78d6-4fe1-89d2-9653c8b12c6d", "35f2f906-2a79-442e-b7d8-2f3f59c7c89c" },
                    { 3, "Active", null, 0, "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9", 78m, 1, 2, 80, "9ae7ec99-1ee5-4a61-b2ff-308b14f2bb38", "815dd6f4-2d41-4c6c-a032-5e78a1cf065b" },
                    { 4, "Active", null, 0, "6bfb28e9-10c2-4a02-9755-c1fcb9a01d14", 85m, 2, 2, 23, "c7e1f7c5-615e-4328-8e4f-77a70c57fd52", "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50" },
                    { 5, "Active", null, 0, "6bfb28e9-10c2-4a02-9755-c1fcb9a01d14", 92m, 3, 1, 5, "b23a6a23-b8eb-4ec6-bd34-c3e11b446441", "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6" },
                    { 6, "Active", null, 0, "176d899b-1c24-49fc-baf1-8755ef89f1b3", 70m, 1, 2, 18, "5a9e7fd7-d49e-4c5b-946b-dac02ac8565d", "cd9fe541-1c6f-4e9e-a94b-1ff748186975" },
                    { 7, "Active", null, 0, "176d899b-1c24-49fc-baf1-8755ef89f1b3", 63m, 2, 3, 63, "87acbe6a-7bcb-4c59-ba27-7f62421709f9", "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50" }
                });

            migrationBuilder.InsertData(
                table: "StudentModule",
                columns: new[] { "id", "ModuleId", "ModuleLevel", "ModuleScore", "StudentId", "studentModuleId" },
                values: new object[,]
                {
                    { 1, "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40", 1, 22m, "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "977aef17-9c24-48e8-83a4-d55e5fc0e319" },
                    { 2, "b684c89f-7b7e-4145-b345-9347a67673a3", 2, 42m, "d5d5c72c-6b4a-4299-bc57-29cb8120e118", "e426c7e7-05e0-4798-8cf9-8ec2bc18d2d4" },
                    { 3, "6e68f0c2-d9b2-4ab7-8b3b-8fbf81a8dd40", 1, 52m, "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", "8b2a69f2-625b-4749-afe5-5a2a60f3cb53" },
                    { 4, "b684c89f-7b7e-4145-b345-9347a67673a3", 3, 22m, "d5d5c72c-6b4a-4299-bc57-29cb8120e118", "b1446b4c-3b57-4dbf-a94f-6f29e2d33a9e" },
                    { 5, "f74e3a7a-312f-4f80-86cb-25f7d52735bc", 1, 24m, "39ffcf76-0db7-4f95-b1ae-3f527c8fe5a7", "9ae1e7c4-459b-4155-9a28-c5ad8fbdb1f6" },
                    { 6, "f74e3a7a-312f-4f80-86cb-25f7d52735bc", 3, 72m, "35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "70d26e2f-b330-4e97-b2d2-d3be9ec0ac0b" },
                    { 7, "5488d35d-0a1e-4634-898e-92dbde019029", 2, 22m, "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", "c20d89a0-8d3f-43c3-b9d1-8764ebd6f5a5" },
                    { 8, "5488d35d-0a1e-4634-898e-92dbde019029", 3, 92m, "e8011711-9367-404e-b0a1-3cfa0e54f015", "f145ac5c-49d2-44b2-a6c7-86a4bc855de5" },
                    { 9, "9bb0a5cb-6bf6-418b-b549-4b3d8abbebb7", 1, 65m, "a4f15d45-0b76-4c1b-97b5-6a6b27229aae", "87225985-9f69-4fc7-9c54-8ec93b8e7021" },
                    { 10, "9bb0a5cb-6bf6-418b-b549-4b3d8abbebb7", 2, 22m, "e8011711-9367-404e-b0a1-3cfa0e54f015", "c45f8b95-2c59-4e32-a2a2-29ecabe9e2cf" },
                    { 11, "315fd315-6764-4514-a289-569c07b91894", 2, 44m, "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "2e5ef0f0-9a09-470b-87f5-c58f831bcf56" },
                    { 12, "315fd315-6764-4514-a289-569c07b91894", 3, 34m, "815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "c9a1865c-bbf1-47d6-bd4e-4be5f1ee9e92" },
                    { 13, "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", 1, 23m, "573e5801-f47e-4a82-bcb5-90fc722e4d4f", "76b04144-1a4b-4c9e-af23-b6e494a621d2" },
                    { 14, "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", 3, 22m, "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "598a4f03-f9c8-4b4c-9fb7-2eb5b9941b3a" },
                    { 15, "c8a4b3cc-78c5-4f20-804e-d617c35aa769", 1, 74m, "f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "2c350499-b099-4d9a-a52b-4a77d8e31579" },
                    { 16, "09cf6935-9c54-49c5-8a48-202268ad4f55", 2, 93m, "573e5801-f47e-4a82-bcb5-90fc722e4d4f", "3d97c81b-6e05-4d56-855b-8f570e1f88b4" },
                    { 17, "d067d9d4-2c4e-4dcf-85aa-30edc14263e2", 3, 84m, "d269b62c-c9dc-4d8f-8b3c-602d88e72438", "b20933f4-f19c-4b65-bec7-48b9bcb105d8" },
                    { 18, "c485fd06-7b81-470d-a4d1-2e66e8fbd11d", 2, 77m, "d269b62c-c9dc-4d8f-8b3c-602d88e72438", "61be548f-67f2-4fd3-a717-7bc97d3b84dc" },
                    { 19, "09cf6935-9c54-49c5-8a48-202268ad4f55", 2, 65m, "97d39a95-2e4b-437f-a032-35e6357f06aa", "7e904f7f-34a4-43e2-b5b1-36a3e50f15d8" }
                });

            migrationBuilder.InsertData(
                table: "TrainingProgram",
                columns: new[] { "id", "CreatedBy", "CreatedDate", "Days", "Hours", "Name", "StartTime", "Status", "TechnicalCodeId", "TechnicalGroupId", "TrainingProgramCode", "UpdatedBy", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, "1", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(430), 10, 10, "Introduction to SQL", new TimeOnly(10, 30, 15), "Done", "7a9b5e84-1a8c-4e57-bfc7-23dcb6d78e92", "e792ad2b-9d75-46f3-a1f0-eb3184372f92", "b26cfe3f-6d6c-4d24-afe0-12e21715b042", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(433), "4463c0cdff13441c8cd55e68961ec70b" },
                    { 2, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(435), null, null, "Advanced Data Analysis", new TimeOnly(12, 17, 5), "Not yet", "4f58ebf5-79d8-4fb9-bd7b-12119bfa3171", "3fc26468-3fd0-49ff-bde4-0b2484aa3c3f", "8c06310b-4d44-4e0e-a7a0-b84d09ccfc3a", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(436), "e235315151ec41f7907afc1d60f15a6d" },
                    { 3, "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(438), null, null, "Python Fundamentals", new TimeOnly(6, 20, 25), "Done", "ac2a35a0-c9d7-4a19-b1d4-0f0e6c1f14c7", "b9358a53-d1c1-4d3d-8f22-1e75a8a9a6d8", "1f749079-2a92-4f32-aa0b-8391a028cfe4", "0", new DateTime(2024, 3, 21, 9, 14, 55, 406, DateTimeKind.Local).AddTicks(439), "a3db3e4e0f25445282b2e61a32c582a4" }
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
                name: "UQ__Assignme__52C21821D9283E9E",
                table: "Assignment",
                column: "assignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "attendee_type_unique",
                table: "AttendeeType",
                column: "AttendeeTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Attendee__114FA692CC3579EB",
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
                name: "UQ__Class__7577347F0E09220E",
                table: "Class",
                column: "classId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassUser_ClassId",
                table: "ClassUser",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "UQ__Delivery__72E12F1BCAE909DF",
                table: "DeliveryType",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Delivery__BA19297BE49A3755",
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
                name: "UQ__EmailSen__4B3B46D7354B2CBB",
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
                name: "UQ__EmailSen__2D96D8D684A06733",
                table: "EmailSendStudent",
                column: "emailSendStudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__EmailTem__C443B510273F0DA5",
                table: "EmailTemplate",
                column: "emailTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__fsu__E1FCEFCA7EF29731",
                table: "fsu",
                column: "fsuId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Location__30646B6F32B77C0A",
                table: "Location",
                column: "locationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_MAJOR_NAME",
                table: "Major",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Major__A5B1B4B55E10EFFF",
                table: "Major",
                column: "majorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Module__8EEC8E166C0A30EB",
                table: "Module",
                column: "moduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__OutputSt__BED5012D7A551E0B",
                table: "OutputStandard",
                column: "outputStandardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_ModuleId",
                table: "Quiz",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Quiz__CFF54C3C832E8C81",
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
                name: "UQ__Reserved__12EF4C50142F7E21",
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
                name: "UQ__Role__CD98462B408B2C9E",
                table: "Role",
                column: "roleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__RolePerm__8AFACE1B5732A972",
                table: "RolePermission",
                column: "RoleId",
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
                name: "UQ__Score__B56A0C8C4A13EA97",
                table: "Score",
                column: "scoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_MajorId",
                table: "Student",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "UQ__Student__4D11D63DBF618A0B",
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
                name: "UQ__StudentC__114B9902500C79F3",
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
                name: "UQ__StudentM__4A54FA669301BCB6",
                table: "StudentModule",
                column: "studentModuleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "topic_code_unique",
                table: "Syllabus",
                column: "topic_code",
                unique: true,
                filter: "[topic_code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Syllabus__915EDF81F530350C",
                table: "Syllabus",
                column: "syllabusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusDay_syllabus_id",
                table: "SyllabusDay",
                column: "syllabus_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Syllabus__6F1A1381F1101CC8",
                table: "SyllabusDay",
                column: "syllabusDayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusUnit_syllabus_day_id",
                table: "SyllabusUnit",
                column: "syllabus_day_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Syllabus__D5A449019B1027EC",
                table: "SyllabusUnit",
                column: "syllabusUnitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "technical_code_unique_name",
                table: "TechnicalCode",
                column: "TechnicalCodeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Technica__7E6FA295E18ED5C5",
                table: "TechnicalCode",
                column: "technicalCodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TechnicalGroupNameUnique",
                table: "TechnicalGroup",
                column: "TechnicalGroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Technica__07542F35E754278E",
                table: "TechnicalGroup",
                column: "technicalGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingMaterial_unit_chapter_id",
                table: "TrainingMaterial",
                column: "unit_chapter_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Training__E3CB00D61261AAEC",
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
                name: "UQ__Training__8245E6A31E34736B",
                table: "TrainingProgram",
                column: "TrainingProgramCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramModule_ModuleId",
                table: "TrainingProgramModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Training__179227236E66ECA7",
                table: "TrainingProgramModule",
                columns: new[] { "ProgramId", "ModuleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramSyllabus_syllabus_id",
                table: "TrainingProgramSyllabus",
                column: "syllabus_id");

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
                name: "UQ__UnitChap__A4B0833C1DDD0FF2",
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
                name: "UQ__User__CB9A1CFE05390903",
                table: "User",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UsernameUnique",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__UserPerm__0E30AD2E5F3873EC",
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
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "EmailSend");

            migrationBuilder.DropTable(
                name: "Quiz");

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
