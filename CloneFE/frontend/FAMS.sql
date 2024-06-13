/****** Object:  Database [FAMS]    Script Date: 1/31/2024 10:05:06 AM ******/
CREATE DATABASE [FAMS]
GO

USE [FAMS]
GO

ALTER DATABASE [FAMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FAMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FAMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FAMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FAMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [FAMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FAMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FAMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FAMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FAMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FAMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FAMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FAMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FAMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FAMS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FAMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FAMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FAMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FAMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FAMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FAMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FAMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FAMS] SET RECOVERY FULL 
GO
ALTER DATABASE [FAMS] SET  MULTI_USER 
GO
ALTER DATABASE [FAMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FAMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FAMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FAMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FAMS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FAMS', N'ON'
GO
ALTER DATABASE [FAMS] SET QUERY_STORE = OFF
GO
USE [FAMS]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [FAMS]
GO
/****** Object:  Table [dbo].[AssessmentScheme]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssessmentScheme](
	[assesmentSchemeId] [nvarchar](36) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[assignment] [float] NULL,
	[final_practice] [float] NULL,
	[final] [float] NULL,
	[final_theory] [float] NULL,
	[gpa] [float] NULL,
	[quiz] [float] NULL,
	[syllabus_id] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[assignmentId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [nvarchar](36) NOT NULL,
	[AssignmentName] [nvarchar](max) NOT NULL,
	[AssignmentType] [int] NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttendeeType]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeType](
	[attendeeTypeId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[AttendeeTypeName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[classId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
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
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassUser]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassUser](
	[UserId] [nvarchar](36) NOT NULL,
	[ClassId] [nvarchar](36) NOT NULL,
	[UserType] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryType]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryType](
	[deliveryTypeId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descriptions] [nvarchar](255) NOT NULL,
	[icon] [nvarchar](255) NULL,
	[name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSend]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSend](
	[emailSendId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TemplateId] [nvarchar](36) NOT NULL,
	[SenderId] [nvarchar](36) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[SendDate] [datetime2](7) NOT NULL,
	[ReceiverType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSendStudent]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSendStudent](
	[emailSendStudentId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReceiverId] [nvarchar](36) NOT NULL,
	[EmailId] [nvarchar](36) NOT NULL,
	[ReceiverType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[emailTemplateId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fsu]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fsu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fsuId] [nvarchar](36) NOT NULL UNIQUE,
	[name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[locationId] [nvarchar](36) NOT NULL UNIQUE,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[majorId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[moduleId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutputStandard]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutputStandard](
	[outputStandardId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](255) NOT NULL,
	[descriptions] [nvarchar](255) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz](
	[quizId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [nvarchar](36) NULL,
	[QuizName] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizStudent]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizStudent](
	[quizStudentId] [nvarchar](36) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](36) NULL,
	[QuizId] [nvarchar](36) NULL,
	[Score] [decimal](18, 0) NULL,
	[SubmissionDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservedClass]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservedClass](
	[reservedClassId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](36) NULL,
	[ClassId] [nvarchar](36) NOT NULL,
    [Reason] [nvarchar] (200) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](36) NULL,
	[ModifiedDate] [datetime] NULL,
	[RoleName] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermission]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermission](
	[RoleId] [nvarchar](36) NOT NULL UNIQUE,
	[PermissionId] [nvarchar](36) NOT NULL UNIQUE,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[scoreId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](36) NOT NULL,
	[AssignmentId] [nvarchar](36) NOT NULL,
	[Score] [decimal](18, 0) NULL,
	[SubmissionDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[studentId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Gender] [nvarchar] (10) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[MajorId] [nvarchar](36) NOT NULL,
	[GraduatedDate] [datetime2](7) NOT NULL,
	[GPA] [decimal](18, 0) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[FAAccount] [nvarchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[JoinedDate] [datetime2](7) NOT NULL,
	[Area] [nvarchar](max) NOT NULL,
	[RECer] [nvarchar](100) NULL,
	[University] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClass]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClass](
	[studentClassId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](36) NOT NULL,
	[ClassId] [nvarchar](36) NOT NULL,
	[AttendingStatus] [int] NOT NULL,
	[Result] [int] NOT NULL,
	[FinalScore] [decimal](18, 0) NOT NULL,
	[GPALevel] [int] NOT NULL,
	[CertificationStatus] [int] NOT NULL,
	[CertificationDate] [datetime2](7) NULL,
	[Method] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentModule]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentModule](
	[studentModuleId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](36) NOT NULL,
	[ModuleId] [nvarchar](36) NOT NULL,
	[ModuleScore] [decimal](18, 0) NOT NULL,
	[ModuleLevel] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Syllabus]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Syllabus](
	[syllabusId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[topic_code] [nvarchar](20) NOT NULL,
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
	[delivery_principle] [varbinary](max) NOT NULL,
	[days] [int] NULL,
	[hours] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SyllabusDay]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyllabusDay](
	[syllabusDayId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created_by] [nvarchar](36) NULL,
	[created_date] [datetime] NULL,
	[is_deleted] [bit] NOT NULL,
	[modified_by] [nvarchar](36) NULL,
	[modified_date] [datetime] NULL,
	[day_no] [int] NULL,
	[syllabus_id] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SyllabusUnit]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyllabusUnit](
	[syllabusUnitId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created_by] [nvarchar](36) NULL,
	[created_date] [datetime] NULL,
	[is_deleted] [bit] NOT NULL,
	[modified_by] [nvarchar](36) NULL,
	[modified_date] [datetime] NULL,
	[duration] [int] NULL,
	[name] [nvarchar](255) NOT NULL,
	[unit_no] [int] NOT NULL,
	[syllabus_day_id] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TechnicalCode]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TechnicalCode](
	[technicalCodeId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[TechnicalCodeName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TechnicalGroup]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TechnicalGroup](
	[technicalGroupId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[TechnicalGroupName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingMaterial]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingMaterial](
	[trainingMaterialId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
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
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgram]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgram](
	[TrainingProgramCode] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
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
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgramModule]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgramModule](
	[ProgramId] [nvarchar](36) NOT NULL UNIQUE,
	[ModuleId] [nvarchar](36) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgramSyllabus]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgramSyllabus](
	[syllabus_id] [nvarchar](36) NOT NULL,
	[training_program_code] [nvarchar](36) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnitChapter]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitChapter](
	[unitChapterId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
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
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [nvarchar](36) NOT NULL UNIQUE,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
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
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 1/31/2024 10:05:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[userPermissionId] [nvarchar](36) NOT NULL UNIQUE,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
	[UpdatedTime] [datetime] NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [attendee_type_unique]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[AttendeeType] ADD  CONSTRAINT [attendee_type_unique] UNIQUE NONCLUSTERED 
(
	[AttendeeTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Delivery__72E12F1B8D4DB194]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[DeliveryType] ADD UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [FK_MAJOR_NAME]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[Major] ADD  CONSTRAINT [FK_MAJOR_NAME] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [role_name_unique]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [role_name_unique] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [topic_code_unique]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[Syllabus] ADD  CONSTRAINT [topic_code_unique] UNIQUE NONCLUSTERED 
(
	[topic_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [technical_code_unique_name]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[TechnicalCode] ADD  CONSTRAINT [technical_code_unique_name] UNIQUE NONCLUSTERED 
(
	[TechnicalCodeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [TechnicalGroupNameUnique]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[TechnicalGroup] ADD  CONSTRAINT [TechnicalGroupNameUnique] UNIQUE NONCLUSTERED 
(
	[TechnicalGroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailUnique]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [EmailUnique] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UsernameUnique]    Script Date: 1/31/2024 10:05:07 AM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UsernameUnique] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE [dbo].[AssessmentScheme]  WITH CHECK ADD FOREIGN KEY([syllabus_id])
REFERENCES [dbo].[Syllabus] ([syllabusId])
ON DELETE CASCADE
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
ALTER TABLE [dbo].[ClassUser]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([classId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassUser]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([userId])
ON DELETE CASCADE
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
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD FOREIGN KEY([PermissionId])
REFERENCES [dbo].[UserPermission] ([userPermissionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([roleId])
ON DELETE CASCADE
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
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([majorId])
ON DELETE CASCADE
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
ALTER TABLE [dbo].[SyllabusDay]  WITH CHECK ADD FOREIGN KEY([syllabus_id])
REFERENCES [dbo].[Syllabus] ([syllabusId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SyllabusUnit]  WITH CHECK ADD FOREIGN KEY([syllabus_day_id])
REFERENCES [dbo].[SyllabusDay] ([syllabusDayId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingMaterial]  WITH CHECK ADD FOREIGN KEY([unit_chapter_id])
REFERENCES [dbo].[UnitChapter] ([unitChapterId])
ON DELETE CASCADE
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
ALTER TABLE [dbo].[TrainingProgramSyllabus]  WITH CHECK ADD FOREIGN KEY([syllabus_id])
REFERENCES [dbo].[Syllabus] ([syllabusId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingProgramSyllabus]  WITH CHECK ADD FOREIGN KEY([training_program_code])
REFERENCES [dbo].[TrainingProgram] ([TrainingProgramCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UnitChapter]  WITH CHECK ADD FOREIGN KEY([delivery_type_id])
REFERENCES [dbo].[DeliveryType] ([deliveryTypeId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[UnitChapter]  WITH CHECK ADD FOREIGN KEY([output_standard_id])
REFERENCES [dbo].[OutputStandard] ([outputStandardId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[UnitChapter]  WITH CHECK ADD FOREIGN KEY([syllabus_unit_id])
REFERENCES [dbo].[SyllabusUnit] ([syllabusUnitId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([roleId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Users_Role]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD CHECK  (([Gender]>=(1) AND [Gender]<=(3)))
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD CHECK  (([Gender]='Other' OR [Gender]='Female' OR [Gender]='Male'))
GO
USE [master]
GO
ALTER DATABASE [FAMS] SET  READ_WRITE 
GO
ALTER TABLE [dbo].[Class]
ADD [ModuleId] [int] NULL,
CONSTRAINT [FK_Class_Module] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Module] ([id]);

