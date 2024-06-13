USE [FAMS]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssessmentScheme]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssessmentScheme](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[assesmentSchemeId] [nvarchar](36) NOT NULL,
	[assignment] [float] NULL,
	[final_practice] [float] NULL,
	[final] [float] NULL,
	[final_theory] [float] NULL,
	[gpa] [float] NULL,
	[quiz] [float] NULL,
	[syllabus_id] [nvarchar](36) NULL,
 CONSTRAINT [PK__Assessme__3213E83FCC60AC6C] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[assignmentId] [nvarchar](36) NOT NULL,
	[ModuleId] [nvarchar](36) NOT NULL,
	[AssignmentName] [nvarchar](max) NOT NULL,
	[AssignmentType] [int] NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK__Assignme__3213E83FAFB0A0F5] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Assignment_assignmentId] UNIQUE NONCLUSTERED 
(
	[assignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttendeeType]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[attendeeTypeId] [nvarchar](36) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[AttendeeTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Attendee__3213E83F9919DF08] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_AttendeeType_attendeeTypeId] UNIQUE NONCLUSTERED 
(
	[attendeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[classId] [nvarchar](36) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime] NULL,
	[ClassStatus] [nvarchar](255) NOT NULL,
	[ClassCode] [nvarchar](255) NOT NULL,
	[Duration] [int] NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[ApprovedBy] [nvarchar](36) NULL,
	[ApprovedDate] [datetime] NULL,
	[ReviewBy] [nvarchar](36) NULL,
	[ReviewDate] [datetime] NULL,
	[AcceptedAttendee] [int] NOT NULL,
	[ActualAttendee] [int] NOT NULL,
	[ClassName] [nvarchar](255) NOT NULL,
	[fsu_id] [nvarchar](36) NULL,
	[LocationId] [nvarchar](36) NULL,
	[AttendeeLevelId] [nvarchar](36) NULL,
	[TrainingProgramCode] [nvarchar](36) NULL,
	[PlannedAttendee] [int] NOT NULL,
	[SlotTime] [nvarchar](30) NULL,
	[TotalHours] [float] NOT NULL,
	[TotalDays] [int] NOT NULL,
 CONSTRAINT [PK__Class__3213E83F5E231634] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Class_classId] UNIQUE NONCLUSTERED 
(
	[classId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassUser]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassUser](
	[UserId] [nvarchar](36) NOT NULL,
	[ClassId] [nvarchar](36) NOT NULL,
	[UserType] [nvarchar](50) NULL,
 CONSTRAINT [PK__ClassUse__0B395E30283D2B0E] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryType]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deliveryTypeId] [nvarchar](36) NOT NULL,
	[descriptions] [nvarchar](255) NOT NULL,
	[icon] [nvarchar](255) NULL,
	[name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Delivery__3213E83F97C1CCC9] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_DeliveryType_deliveryTypeId] UNIQUE NONCLUSTERED 
(
	[deliveryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSend]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSend](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[emailSendId] [nvarchar](36) NOT NULL,
	[TemplateId] [nvarchar](36) NOT NULL,
	[SenderId] [nvarchar](36) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[SendDate] [datetime2](7) NOT NULL,
	[ReceiverType] [int] NOT NULL,
 CONSTRAINT [PK__EmailSen__3213E83F3E3F0F74] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_EmailSend_emailSendId] UNIQUE NONCLUSTERED 
(
	[emailSendId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSendStudent]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSendStudent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[emailSendStudentId] [nvarchar](36) NOT NULL,
	[ReceiverId] [nvarchar](36) NOT NULL,
	[EmailId] [nvarchar](36) NOT NULL,
	[ReceiverType] [int] NOT NULL,
 CONSTRAINT [PK__EmailSen__3213E83F60B657A4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[emailTemplateId] [nvarchar](36) NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](36) NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK__EmailTem__3213E83FC51A311E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_EmailTemplate_emailTemplateId] UNIQUE NONCLUSTERED 
(
	[emailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fsu]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fsu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fsuId] [nvarchar](36) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__fsu__3213E83F64A66A7E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_fsu_fsuId] UNIQUE NONCLUSTERED 
(
	[fsuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[locationId] [nvarchar](36) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Location__3214EC0778E516FB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Location_locationId] UNIQUE NONCLUSTERED 
(
	[locationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[majorId] [nvarchar](36) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Major__3213E83F613E5602] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Major_majorId] UNIQUE NONCLUSTERED 
(
	[majorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[moduleId] [nvarchar](36) NOT NULL,
	[ModuleName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK__Module__3213E83F19463C25] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Module_moduleId] UNIQUE NONCLUSTERED 
(
	[moduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutputStandard]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutputStandard](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[outputStandardId] [nvarchar](36) NOT NULL,
	[code] [nvarchar](255) NOT NULL,
	[descriptions] [nvarchar](255) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__OutputSt__3213E83FFE1AA419] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_OutputStandard_outputStandardId] UNIQUE NONCLUSTERED 
(
	[outputStandardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz](
	[id] [int] NOT NULL,
	[quizId] [nvarchar](36) NOT NULL,
	[ModuleId] [nvarchar](36) NULL,
	[QuizName] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK__Quiz__3213E83FA0E025B4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Quiz_quizId] UNIQUE NONCLUSTERED 
(
	[quizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizStudent]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizStudent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[quizStudentId] [nvarchar](36) NOT NULL,
	[StudentId] [nvarchar](36) NULL,
	[QuizId] [nvarchar](36) NULL,
	[Score] [decimal](18, 0) NULL,
	[SubmissionDate] [datetime2](7) NULL,
 CONSTRAINT [PK__QuizStud__3213E83F47D940D4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservedClass]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservedClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[reservedClassId] [nvarchar](36) NOT NULL,
	[StudentId] [nvarchar](36) NULL,
	[ClassId] [nvarchar](36) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__Reserved__3213E83F27E8B567] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roleId] [nvarchar](36) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](36) NULL,
	[ModifiedDate] [datetime] NULL,
	[RoleName] [nvarchar](255) NULL,
 CONSTRAINT [PK__Role__3213E83FC8774946] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Role_roleId] UNIQUE NONCLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermission]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermission](
	[PermissionId] [nvarchar](36) NOT NULL,
	[RoleId] [nvarchar](36) NOT NULL,
	[Syllabus] [nvarchar](36) NOT NULL,
	[TrainingProgram] [nvarchar](36) NOT NULL,
	[Class] [nvarchar](36) NOT NULL,
	[LearningMaterial] [nvarchar](36) NOT NULL,
	[UserManagement] [nvarchar](36) NOT NULL,
 CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[scoreId] [nvarchar](36) NOT NULL,
	[StudentId] [nvarchar](36) NOT NULL,
	[AssignmentId] [nvarchar](36) NOT NULL,
	[Score] [decimal](18, 0) NULL,
	[SubmissionDate] [datetime2](7) NULL,
 CONSTRAINT [PK__Score__3213E83F09BAE30F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [nvarchar](36) NOT NULL,
	[MutatableStudentID] [nvarchar](max) NOT NULL,
	[CertificationDate] [datetime2](7) NULL,
	[CertificationStatus] [bit] NOT NULL,
	[Audit] [int] NOT NULL,
	[Mock] [float] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[MajorId] [nvarchar](36) NOT NULL,
	[GraduatedDate] [datetime2](7) NOT NULL,
	[GPA] [decimal](18, 0) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[FAAccount] [nvarchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[JoinedDate] [datetime2](7) NOT NULL,
	[Area] [nvarchar](max) NOT NULL,
	[RECer] [nvarchar](100) NULL,
	[University] [nvarchar](max) NULL,
 CONSTRAINT [PK__Student__3213E83F166127A4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Student_studentId] UNIQUE NONCLUSTERED 
(
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClass]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentClassId] [nvarchar](36) NOT NULL,
	[StudentId] [nvarchar](36) NOT NULL,
	[ClassId] [nvarchar](36) NOT NULL,
	[AttendingStatus] [nvarchar](50) NOT NULL,
	[Result] [int] NOT NULL,
	[FinalScore] [decimal](18, 0) NOT NULL,
	[GPALevel] [int] NOT NULL,
	[CertificationStatus] [int] NOT NULL,
	[CertificationDate] [datetime2](7) NULL,
	[Method] [int] NOT NULL,
 CONSTRAINT [PK__StudentC__3213E83F82F467D8] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentModule]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentModule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentModuleId] [nvarchar](36) NOT NULL,
	[StudentId] [nvarchar](36) NOT NULL,
	[ModuleId] [nvarchar](36) NOT NULL,
	[ModuleScore] [decimal](18, 0) NOT NULL,
	[ModuleLevel] [int] NOT NULL,
 CONSTRAINT [PK__StudentM__3213E83F1922A532] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Syllabus]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Syllabus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[syllabusId] [nvarchar](36) NOT NULL,
	[topic_code] [nvarchar](20) NULL,
	[topic_name] [nvarchar](255) NOT NULL,
	[version] [nvarchar](50) NOT NULL,
	[created_by] [nvarchar](36) NULL,
	[created_date] [datetime] NULL,
	[modified_by] [nvarchar](36) NULL,
	[modified_date] [datetime] NULL,
	[attendee_number] [int] NULL,
	[level] [nvarchar](50) NULL,
	[technical_requirement] [nvarchar](max) NOT NULL,
	[course_objective] [nvarchar](max) NOT NULL,
	[delivery_principle] [nvarchar](max) NOT NULL,
	[days] [int] NULL,
	[hours] [float] NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Syllabus__3213E83FE0AED0E4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Syllabus_syllabusId] UNIQUE NONCLUSTERED 
(
	[syllabusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SyllabusDay]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyllabusDay](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[syllabusDayId] [nvarchar](36) NOT NULL,
	[created_by] [nvarchar](36) NULL,
	[created_date] [datetime] NULL,
	[is_deleted] [bit] NOT NULL,
	[modified_by] [nvarchar](36) NULL,
	[modified_date] [datetime] NULL,
	[day_no] [int] NULL,
	[syllabus_id] [nvarchar](36) NULL,
 CONSTRAINT [PK__Syllabus__3213E83F96DB42A0] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_SyllabusDay_syllabusDayId] UNIQUE NONCLUSTERED 
(
	[syllabusDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SyllabusUnit]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyllabusUnit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[syllabusUnitId] [nvarchar](36) NOT NULL,
	[created_by] [nvarchar](36) NULL,
	[created_date] [datetime] NULL,
	[is_deleted] [bit] NOT NULL,
	[modified_by] [nvarchar](36) NULL,
	[modified_date] [datetime] NULL,
	[duration] [int] NULL,
	[name] [nvarchar](255) NOT NULL,
	[unit_no] [int] NOT NULL,
	[syllabus_day_id] [nvarchar](36) NULL,
 CONSTRAINT [PK__Syllabus__3213E83F976FDEE6] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_SyllabusUnit_syllabusUnitId] UNIQUE NONCLUSTERED 
(
	[syllabusUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TechnicalCode]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TechnicalCode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[technicalCodeId] [nvarchar](36) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[TechnicalCodeName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Technica__3213E83F5D376B44] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TechnicalCode_technicalCodeId] UNIQUE NONCLUSTERED 
(
	[technicalCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TechnicalGroup]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TechnicalGroup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[technicalGroupId] [nvarchar](36) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[TechnicalGroupName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Technica__3213E83F78E9E1EF] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TechnicalGroup_technicalGroupId] UNIQUE NONCLUSTERED 
(
	[technicalGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingMaterial]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingMaterial](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[trainingMaterialId] [nvarchar](36) NOT NULL,
	[created_by] [nvarchar](36) NULL,
	[created_date] [datetime] NULL,
	[is_deleted] [bit] NOT NULL,
	[modified_by] [nvarchar](36) NULL,
	[modified_date] [datetime] NULL,
	[file_name] [nvarchar](255) NOT NULL,
	[is_file] [bit] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[url] [nvarchar](255) NULL,
	[unit_chapter_id] [nvarchar](36) NULL,
 CONSTRAINT [PK__Training__3213E83F393D468D] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgram]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgram](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TrainingProgramCode] [nvarchar](36) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime] NULL,
	[Days] [int] NULL,
	[Hours] [int] NULL,
	[StartTime] [time](7) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[UserId] [nvarchar](36) NULL,
	[TechnicalCodeId] [nvarchar](36) NULL,
	[TechnicalGroupId] [nvarchar](36) NULL,
 CONSTRAINT [PK__Training__3213E83FAE168522] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TrainingProgram_TrainingProgramCode] UNIQUE NONCLUSTERED 
(
	[TrainingProgramCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgramModule]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgramModule](
	[ProgramId] [nvarchar](36) NOT NULL,
	[ModuleId] [nvarchar](36) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgramSyllabus]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgramSyllabus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[syllabus_id] [nvarchar](36) NOT NULL,
	[training_program_code] [nvarchar](36) NOT NULL,
 CONSTRAINT [PK__Training__2EE7C7116A349350] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[syllabus_id] ASC,
	[training_program_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnitChapter]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitChapter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[unitChapterId] [nvarchar](36) NOT NULL,
	[created_by] [nvarchar](36) NULL,
	[created_date] [datetime] NULL,
	[is_deleted] [bit] NOT NULL,
	[modified_by] [nvarchar](36) NULL,
	[modified_date] [datetime] NULL,
	[chapter_no] [int] NOT NULL,
	[duration] [int] NULL,
	[is_online] [bit] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[delivery_type_id] [nvarchar](36) NULL,
	[output_standard_id] [nvarchar](36) NULL,
	[syllabus_unit_id] [nvarchar](36) NULL,
 CONSTRAINT [PK__UnitChap__3213E83FEE1207B2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_UnitChapter_unitChapterId] UNIQUE NONCLUSTERED 
(
	[unitChapterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [nvarchar](36) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[Gender] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[RoleId] [nvarchar](36) NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](36) NULL,
	[ModifiedDate] [datetime] NULL,
	[Avatar] [nvarchar](255) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK__User__3213E83FD7603D84] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_User_userId] UNIQUE NONCLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 3/21/2024 9:25:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[userPermissionId] [nvarchar](36) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
	[UpdatedTime] [datetime] NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__UserPerm__3214EC07C0E79A0B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AssessmentScheme_syllabus_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_AssessmentScheme_syllabus_id] ON [dbo].[AssessmentScheme]
(
	[syllabus_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Assignment_ModuleId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Assignment_ModuleId] ON [dbo].[Assignment]
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Assignme__52C21821D9283E9E]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Assignme__52C21821D9283E9E] ON [dbo].[Assignment]
(
	[assignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [attendee_type_unique]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [attendee_type_unique] ON [dbo].[AttendeeType]
(
	[AttendeeTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Attendee__114FA692CC3579EB]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Attendee__114FA692CC3579EB] ON [dbo].[AttendeeType]
(
	[attendeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Class_AttendeeLevelId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_AttendeeLevelId] ON [dbo].[Class]
(
	[AttendeeLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Class_fsu_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_fsu_id] ON [dbo].[Class]
(
	[fsu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Class_LocationId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_LocationId] ON [dbo].[Class]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Class_TrainingProgramCode]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_TrainingProgramCode] ON [dbo].[Class]
(
	[TrainingProgramCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Class__7577347F0E09220E]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Class__7577347F0E09220E] ON [dbo].[Class]
(
	[classId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ClassUser_ClassId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_ClassUser_ClassId] ON [dbo].[ClassUser]
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Delivery__72E12F1BCAE909DF]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Delivery__72E12F1BCAE909DF] ON [dbo].[DeliveryType]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Delivery__BA19297BE49A3755]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Delivery__BA19297BE49A3755] ON [dbo].[DeliveryType]
(
	[deliveryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EmailSend_SenderId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmailSend_SenderId] ON [dbo].[EmailSend]
(
	[SenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EmailSend_TemplateId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmailSend_TemplateId] ON [dbo].[EmailSend]
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__EmailSen__4B3B46D7354B2CBB]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__EmailSen__4B3B46D7354B2CBB] ON [dbo].[EmailSend]
(
	[emailSendId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EmailSendStudent_EmailId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmailSendStudent_EmailId] ON [dbo].[EmailSendStudent]
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EmailSendStudent_ReceiverId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmailSendStudent_ReceiverId] ON [dbo].[EmailSendStudent]
(
	[ReceiverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__EmailSen__2D96D8D684A06733]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__EmailSen__2D96D8D684A06733] ON [dbo].[EmailSendStudent]
(
	[emailSendStudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__EmailTem__C443B510273F0DA5]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__EmailTem__C443B510273F0DA5] ON [dbo].[EmailTemplate]
(
	[emailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__fsu__E1FCEFCA7EF29731]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__fsu__E1FCEFCA7EF29731] ON [dbo].[fsu]
(
	[fsuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Location__30646B6F32B77C0A]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Location__30646B6F32B77C0A] ON [dbo].[Location]
(
	[locationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [FK_MAJOR_NAME]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [FK_MAJOR_NAME] ON [dbo].[Major]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Major__A5B1B4B55E10EFFF]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Major__A5B1B4B55E10EFFF] ON [dbo].[Major]
(
	[majorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Module__8EEC8E166C0A30EB]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Module__8EEC8E166C0A30EB] ON [dbo].[Module]
(
	[moduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__OutputSt__BED5012D7A551E0B]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__OutputSt__BED5012D7A551E0B] ON [dbo].[OutputStandard]
(
	[outputStandardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Quiz_ModuleId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Quiz_ModuleId] ON [dbo].[Quiz]
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Quiz__CFF54C3C832E8C81]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Quiz__CFF54C3C832E8C81] ON [dbo].[Quiz]
(
	[quizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_QuizStudent_QuizId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_QuizStudent_QuizId] ON [dbo].[QuizStudent]
(
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_QuizStudent_StudentId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_QuizStudent_StudentId] ON [dbo].[QuizStudent]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReservedClass_ClassId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReservedClass_ClassId] ON [dbo].[ReservedClass]
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReservedClass_StudentId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReservedClass_StudentId] ON [dbo].[ReservedClass]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Reserved__12EF4C50142F7E21]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Reserved__12EF4C50142F7E21] ON [dbo].[ReservedClass]
(
	[reservedClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [role_name_unique]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [role_name_unique] ON [dbo].[Role]
(
	[RoleName] ASC
)
WHERE ([RoleName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Role__CD98462B408B2C9E]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Role__CD98462B408B2C9E] ON [dbo].[Role]
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RolePerm__8AFACE1B5732A972]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__RolePerm__8AFACE1B5732A972] ON [dbo].[RolePermission]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Score_AssignmentId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Score_AssignmentId] ON [dbo].[Score]
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Score_StudentId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Score_StudentId] ON [dbo].[Score]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Score__B56A0C8C4A13EA97]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Score__B56A0C8C4A13EA97] ON [dbo].[Score]
(
	[scoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Student_MajorId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_Student_MajorId] ON [dbo].[Student]
(
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Student__4D11D63DBF618A0B]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Student__4D11D63DBF618A0B] ON [dbo].[Student]
(
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StudentClass_ClassId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_StudentClass_ClassId] ON [dbo].[StudentClass]
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StudentClass_StudentId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_StudentClass_StudentId] ON [dbo].[StudentClass]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__StudentC__114B9902500C79F3]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__StudentC__114B9902500C79F3] ON [dbo].[StudentClass]
(
	[studentClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StudentModule_ModuleId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_StudentModule_ModuleId] ON [dbo].[StudentModule]
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StudentModule_StudentId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_StudentModule_StudentId] ON [dbo].[StudentModule]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__StudentM__4A54FA669301BCB6]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__StudentM__4A54FA669301BCB6] ON [dbo].[StudentModule]
(
	[studentModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [topic_code_unique]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [topic_code_unique] ON [dbo].[Syllabus]
(
	[topic_code] ASC
)
WHERE ([topic_code] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Syllabus__915EDF81F530350C]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Syllabus__915EDF81F530350C] ON [dbo].[Syllabus]
(
	[syllabusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SyllabusDay_syllabus_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_SyllabusDay_syllabus_id] ON [dbo].[SyllabusDay]
(
	[syllabus_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Syllabus__6F1A1381F1101CC8]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Syllabus__6F1A1381F1101CC8] ON [dbo].[SyllabusDay]
(
	[syllabusDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SyllabusUnit_syllabus_day_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_SyllabusUnit_syllabus_day_id] ON [dbo].[SyllabusUnit]
(
	[syllabus_day_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Syllabus__D5A449019B1027EC]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Syllabus__D5A449019B1027EC] ON [dbo].[SyllabusUnit]
(
	[syllabusUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [technical_code_unique_name]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [technical_code_unique_name] ON [dbo].[TechnicalCode]
(
	[TechnicalCodeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Technica__7E6FA295E18ED5C5]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Technica__7E6FA295E18ED5C5] ON [dbo].[TechnicalCode]
(
	[technicalCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [TechnicalGroupNameUnique]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [TechnicalGroupNameUnique] ON [dbo].[TechnicalGroup]
(
	[TechnicalGroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Technica__07542F35E754278E]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Technica__07542F35E754278E] ON [dbo].[TechnicalGroup]
(
	[technicalGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TrainingMaterial_unit_chapter_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_TrainingMaterial_unit_chapter_id] ON [dbo].[TrainingMaterial]
(
	[unit_chapter_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Training__E3CB00D61261AAEC]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Training__E3CB00D61261AAEC] ON [dbo].[TrainingMaterial]
(
	[trainingMaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TrainingProgram_TechnicalCodeId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_TrainingProgram_TechnicalCodeId] ON [dbo].[TrainingProgram]
(
	[TechnicalCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TrainingProgram_TechnicalGroupId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_TrainingProgram_TechnicalGroupId] ON [dbo].[TrainingProgram]
(
	[TechnicalGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TrainingProgram_UserId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_TrainingProgram_UserId] ON [dbo].[TrainingProgram]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Training__8245E6A31E34736B]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Training__8245E6A31E34736B] ON [dbo].[TrainingProgram]
(
	[TrainingProgramCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TrainingProgramModule_ModuleId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_TrainingProgramModule_ModuleId] ON [dbo].[TrainingProgramModule]
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Training__179227236E66ECA7]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Training__179227236E66ECA7] ON [dbo].[TrainingProgramModule]
(
	[ProgramId] ASC,
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TrainingProgramSyllabus_syllabus_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_TrainingProgramSyllabus_syllabus_id] ON [dbo].[TrainingProgramSyllabus]
(
	[syllabus_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TrainingProgramSyllabus_training_program_code]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_TrainingProgramSyllabus_training_program_code] ON [dbo].[TrainingProgramSyllabus]
(
	[training_program_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UnitChapter_delivery_type_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_UnitChapter_delivery_type_id] ON [dbo].[UnitChapter]
(
	[delivery_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UnitChapter_output_standard_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_UnitChapter_output_standard_id] ON [dbo].[UnitChapter]
(
	[output_standard_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UnitChapter_syllabus_unit_id]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_UnitChapter_syllabus_unit_id] ON [dbo].[UnitChapter]
(
	[syllabus_unit_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__UnitChap__A4B0833C1DDD0FF2]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__UnitChap__A4B0833C1DDD0FF2] ON [dbo].[UnitChapter]
(
	[unitChapterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailUnique]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [EmailUnique] ON [dbo].[User]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_RoleId]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_RoleId] ON [dbo].[User]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__CB9A1CFE05390903]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__User__CB9A1CFE05390903] ON [dbo].[User]
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UsernameUnique]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UsernameUnique] ON [dbo].[User]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__UserPerm__0E30AD2E5F3873EC]    Script Date: 3/21/2024 9:25:21 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__UserPerm__0E30AD2E5F3873EC] ON [dbo].[UserPermission]
(
	[userPermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AssessmentScheme] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [assesmentSchemeId]
GO
ALTER TABLE [dbo].[Assignment] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [assignmentId]
GO
ALTER TABLE [dbo].[Assignment] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Assignment] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[AttendeeType] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [attendeeTypeId]
GO
ALTER TABLE [dbo].[Class] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [classId]
GO
ALTER TABLE [dbo].[DeliveryType] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [deliveryTypeId]
GO
ALTER TABLE [dbo].[EmailSend] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [emailSendId]
GO
ALTER TABLE [dbo].[EmailSendStudent] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [emailSendStudentId]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [emailTemplateId]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[fsu] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [fsuId]
GO
ALTER TABLE [dbo].[Location] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [locationId]
GO
ALTER TABLE [dbo].[Major] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [majorId]
GO
ALTER TABLE [dbo].[Module] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [moduleId]
GO
ALTER TABLE [dbo].[Module] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Module] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[OutputStandard] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [outputStandardId]
GO
ALTER TABLE [dbo].[Quiz] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [quizId]
GO
ALTER TABLE [dbo].[QuizStudent] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [quizStudentId]
GO
ALTER TABLE [dbo].[ReservedClass] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [reservedClassId]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [roleId]
GO
ALTER TABLE [dbo].[RolePermission] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [PermissionId]
GO
ALTER TABLE [dbo].[Score] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [scoreId]
GO
ALTER TABLE [dbo].[Student] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [studentId]
GO
ALTER TABLE [dbo].[StudentClass] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [studentClassId]
GO
ALTER TABLE [dbo].[StudentModule] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [studentModuleId]
GO
ALTER TABLE [dbo].[Syllabus] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [syllabusId]
GO
ALTER TABLE [dbo].[SyllabusDay] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [syllabusDayId]
GO
ALTER TABLE [dbo].[SyllabusUnit] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [syllabusUnitId]
GO
ALTER TABLE [dbo].[TechnicalCode] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [technicalCodeId]
GO
ALTER TABLE [dbo].[TechnicalGroup] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [technicalGroupId]
GO
ALTER TABLE [dbo].[TrainingMaterial] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [trainingMaterialId]
GO
ALTER TABLE [dbo].[TrainingProgram] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [TrainingProgramCode]
GO
ALTER TABLE [dbo].[UnitChapter] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [unitChapterId]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [userId]
GO
ALTER TABLE [dbo].[UserPermission] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [userPermissionId]
GO
ALTER TABLE [dbo].[AssessmentScheme]  WITH CHECK ADD  CONSTRAINT [FK__Assessmen__sylla__45BE5BA9] FOREIGN KEY([syllabus_id])
REFERENCES [dbo].[Syllabus] ([syllabusId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssessmentScheme] CHECK CONSTRAINT [FK__Assessmen__sylla__45BE5BA9]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Assignment_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([moduleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_Assignment_Module]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_AttendeeType] FOREIGN KEY([AttendeeLevelId])
REFERENCES [dbo].[AttendeeType] ([attendeeTypeId])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_AttendeeType]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_FSU] FOREIGN KEY([fsu_id])
REFERENCES [dbo].[fsu] ([fsuId])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_FSU]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([locationId])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Location]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_TrainingProgram] FOREIGN KEY([TrainingProgramCode])
REFERENCES [dbo].[TrainingProgram] ([TrainingProgramCode])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_TrainingProgram]
GO
ALTER TABLE [dbo].[ClassUser]  WITH CHECK ADD  CONSTRAINT [FK__ClassUser__Class__4B7734FF] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([classId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassUser] CHECK CONSTRAINT [FK__ClassUser__Class__4B7734FF]
GO
ALTER TABLE [dbo].[ClassUser]  WITH CHECK ADD  CONSTRAINT [FK__ClassUser__UserI__3587F3E0] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([userId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassUser] CHECK CONSTRAINT [FK__ClassUser__UserI__3587F3E0]
GO
ALTER TABLE [dbo].[EmailSend]  WITH CHECK ADD  CONSTRAINT [FK_EmailSend_Sender] FOREIGN KEY([SenderId])
REFERENCES [dbo].[User] ([userId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[EmailSend] CHECK CONSTRAINT [FK_EmailSend_Sender]
GO
ALTER TABLE [dbo].[EmailSend]  WITH CHECK ADD  CONSTRAINT [FK_EmailSend_Template] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[EmailTemplate] ([emailTemplateId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailSend] CHECK CONSTRAINT [FK_EmailSend_Template]
GO
ALTER TABLE [dbo].[EmailSendStudent]  WITH CHECK ADD  CONSTRAINT [FK_EmailSendStudent_Email] FOREIGN KEY([EmailId])
REFERENCES [dbo].[EmailSend] ([emailSendId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailSendStudent] CHECK CONSTRAINT [FK_EmailSendStudent_Email]
GO
ALTER TABLE [dbo].[EmailSendStudent]  WITH CHECK ADD  CONSTRAINT [FK_EmailSendStudent_Receiver] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Student] ([studentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailSendStudent] CHECK CONSTRAINT [FK_EmailSendStudent_Receiver]
GO
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([moduleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quiz] CHECK CONSTRAINT [FK_Quiz_Module]
GO
ALTER TABLE [dbo].[QuizStudent]  WITH CHECK ADD  CONSTRAINT [FK_QuizStudent_Quiz] FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quiz] ([quizId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizStudent] CHECK CONSTRAINT [FK_QuizStudent_Quiz]
GO
ALTER TABLE [dbo].[QuizStudent]  WITH CHECK ADD  CONSTRAINT [FK_QuizStudent_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([studentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizStudent] CHECK CONSTRAINT [FK_QuizStudent_Student]
GO
ALTER TABLE [dbo].[ReservedClass]  WITH CHECK ADD  CONSTRAINT [FK_ReservedClass_Class] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([classId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReservedClass] CHECK CONSTRAINT [FK_ReservedClass_Class]
GO
ALTER TABLE [dbo].[ReservedClass]  WITH CHECK ADD  CONSTRAINT [FK_ReservedClass_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([studentId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[ReservedClass] CHECK CONSTRAINT [FK_ReservedClass_Student]
GO
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK__RolePermi__RoleI__40058253] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([roleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK__RolePermi__RoleI__40058253]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_Assignment] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([assignmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_Assignment]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([studentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_Student]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK__Student__MajorId__43D61337] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([majorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK__Student__MajorId__43D61337]
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD  CONSTRAINT [FK_StudentClass_Class] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([classId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentClass] CHECK CONSTRAINT [FK_StudentClass_Class]
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD  CONSTRAINT [FK_StudentClass_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([studentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentClass] CHECK CONSTRAINT [FK_StudentClass_Student]
GO
ALTER TABLE [dbo].[StudentModule]  WITH CHECK ADD  CONSTRAINT [FK_StudentModule_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([moduleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentModule] CHECK CONSTRAINT [FK_StudentModule_Module]
GO
ALTER TABLE [dbo].[StudentModule]  WITH CHECK ADD  CONSTRAINT [FK_StudentModule_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([studentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentModule] CHECK CONSTRAINT [FK_StudentModule_Student]
GO
ALTER TABLE [dbo].[SyllabusDay]  WITH CHECK ADD  CONSTRAINT [FK__SyllabusD__sylla__5D95E53A] FOREIGN KEY([syllabus_id])
REFERENCES [dbo].[Syllabus] ([syllabusId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SyllabusDay] CHECK CONSTRAINT [FK__SyllabusD__sylla__5D95E53A]
GO
ALTER TABLE [dbo].[SyllabusUnit]  WITH CHECK ADD  CONSTRAINT [FK__SyllabusU__sylla__5E8A0973] FOREIGN KEY([syllabus_day_id])
REFERENCES [dbo].[SyllabusDay] ([syllabusDayId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SyllabusUnit] CHECK CONSTRAINT [FK__SyllabusU__sylla__5E8A0973]
GO
ALTER TABLE [dbo].[TrainingMaterial]  WITH CHECK ADD  CONSTRAINT [FK__TrainingM__unit___5F7E2DAC] FOREIGN KEY([unit_chapter_id])
REFERENCES [dbo].[UnitChapter] ([unitChapterId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingMaterial] CHECK CONSTRAINT [FK__TrainingM__unit___5F7E2DAC]
GO
ALTER TABLE [dbo].[TrainingProgram]  WITH CHECK ADD  CONSTRAINT [FK_TrainingProgram_TechnicalCode] FOREIGN KEY([TechnicalCodeId])
REFERENCES [dbo].[TechnicalCode] ([technicalCodeId])
GO
ALTER TABLE [dbo].[TrainingProgram] CHECK CONSTRAINT [FK_TrainingProgram_TechnicalCode]
GO
ALTER TABLE [dbo].[TrainingProgram]  WITH CHECK ADD  CONSTRAINT [FK_TrainingProgram_TechnicalGroup] FOREIGN KEY([TechnicalGroupId])
REFERENCES [dbo].[TechnicalGroup] ([technicalGroupId])
GO
ALTER TABLE [dbo].[TrainingProgram] CHECK CONSTRAINT [FK_TrainingProgram_TechnicalGroup]
GO
ALTER TABLE [dbo].[TrainingProgram]  WITH CHECK ADD  CONSTRAINT [FK_TrainingProgram_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[TrainingProgram] CHECK CONSTRAINT [FK_TrainingProgram_User]
GO
ALTER TABLE [dbo].[TrainingProgramModule]  WITH CHECK ADD  CONSTRAINT [FK_TrainingProgramModule_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([moduleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingProgramModule] CHECK CONSTRAINT [FK_TrainingProgramModule_Module]
GO
ALTER TABLE [dbo].[TrainingProgramModule]  WITH CHECK ADD  CONSTRAINT [FK_TrainingProgramModule_Program] FOREIGN KEY([ProgramId])
REFERENCES [dbo].[TrainingProgram] ([TrainingProgramCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingProgramModule] CHECK CONSTRAINT [FK_TrainingProgramModule_Program]
GO
ALTER TABLE [dbo].[TrainingProgramSyllabus]  WITH CHECK ADD  CONSTRAINT [FK__TrainingP__sylla__65370702] FOREIGN KEY([syllabus_id])
REFERENCES [dbo].[Syllabus] ([syllabusId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingProgramSyllabus] CHECK CONSTRAINT [FK__TrainingP__sylla__65370702]
GO
ALTER TABLE [dbo].[TrainingProgramSyllabus]  WITH CHECK ADD  CONSTRAINT [FK__TrainingP__train__662B2B3B] FOREIGN KEY([training_program_code])
REFERENCES [dbo].[TrainingProgram] ([TrainingProgramCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingProgramSyllabus] CHECK CONSTRAINT [FK__TrainingP__train__662B2B3B]
GO
ALTER TABLE [dbo].[UnitChapter]  WITH CHECK ADD  CONSTRAINT [FK__UnitChapt__deliv__671F4F74] FOREIGN KEY([delivery_type_id])
REFERENCES [dbo].[DeliveryType] ([deliveryTypeId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[UnitChapter] CHECK CONSTRAINT [FK__UnitChapt__deliv__671F4F74]
GO
ALTER TABLE [dbo].[UnitChapter]  WITH CHECK ADD  CONSTRAINT [FK__UnitChapt__outpu__690797E6] FOREIGN KEY([output_standard_id])
REFERENCES [dbo].[OutputStandard] ([outputStandardId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[UnitChapter] CHECK CONSTRAINT [FK__UnitChapt__outpu__690797E6]
GO
ALTER TABLE [dbo].[UnitChapter]  WITH CHECK ADD  CONSTRAINT [FK__UnitChapt__sylla__69FBBC1F] FOREIGN KEY([syllabus_unit_id])
REFERENCES [dbo].[SyllabusUnit] ([syllabusUnitId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UnitChapter] CHECK CONSTRAINT [FK__UnitChapt__sylla__69FBBC1F]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([roleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
