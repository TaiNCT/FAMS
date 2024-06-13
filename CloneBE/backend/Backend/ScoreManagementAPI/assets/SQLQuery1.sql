USE [FAMS];
GO



CREATE TABLE [dbo].[Assignment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[AssignmentName] [nvarchar](max) NOT NULL,
	[AssignmentType] [int] NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[Class](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](max) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
	[Duration] [time](7) NOT NULL,
	[Location] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
	[ProgramId] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSend]    Script Date: 1/27/2024 10:05:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSend](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[SenderId] [int] NULL,
	[Content] [nvarchar](max) NOT NULL,
	[SendDate] [datetime2](7) NOT NULL,
	[ReceiverType] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSendStudent]    Script Date: 1/27/2024 10:05:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSendStudent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[EmailId] [int] NOT NULL,
	[ReceiverType] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 1/27/2024 10:05:19 AM ******/
CREATE TABLE [dbo].[EmailTemplate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[Module](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[Quiz](
	[id] [int] NOT NULL,
	[ModuleId] [int] NULL,
	[QuizName] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizStudent]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[QuizStudent](
	[id] [int] NOT NULL,
	[StudentId] [int] NULL,
	[QuizId] [int] NULL,
	[Score] [decimal](18, 0) NULL,
	[SubmissionDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservedClass]    Script Date: 1/27/2024 10:05:19 AM ******/
CREATE TABLE [dbo].[ReservedClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[ClassId] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
	[title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 1/27/2024 10:05:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[AssignmentId] [int] NOT NULL,
	[Score] [decimal](18, 0) NULL,
	[SubmissionDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[Student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Gender] [int] NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Major] [int] NOT NULL,
	[GraduatedDate] [datetime2](7) NOT NULL,
	[GPA] [decimal](18, 0) NOT NULL,
	[Address] [nvarchar](max),
	[Location] [nvarchar](max) NOT NULL,
	[Permanent_Residence] [nvarchar](max) NOT NULL,
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClass]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[StudentClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[AttendingStatus] [int] NOT NULL,
	[Result] [int] NOT NULL,
	[FinalScore] [decimal](18, 0) NOT NULL,
	[GPALevel] [int] NOT NULL,
	[CertificationStatus] [bit] NOT NULL, --- Fix : should be "bit", it's either "Done" or "Not yet"
	[CertificationDate] [datetime2](7) NULL,
	[Method] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentModule]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[StudentModule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[ModuleScore] [decimal](18, 0) NOT NULL,
	[ModuleLevel] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgram]    Script Date: 1/27/2024 10:05:19 AM ******/
CREATE TABLE [dbo].[TrainingProgram](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[Duration] [time](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgramModule]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[TrainingProgramModule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgramId] [int] NOT NULL,
	[ModuleId] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClass]    Script Date: 1/27/2024 10:05:19 AM ******/

CREATE TABLE [dbo].[UserClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/27/2024 10:05:19 AM ******/
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Gender] [int] NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[roleid] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assignment] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Assignment] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Class] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Class] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Module] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Module] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[TrainingProgram] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD FOREIGN KEY([ProgramId])
REFERENCES [dbo].[TrainingProgram] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailSend]  WITH CHECK ADD FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[EmailSend]  WITH CHECK ADD FOREIGN KEY([TemplateId])
REFERENCES [dbo].[EmailTemplate] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailSendStudent]  WITH CHECK ADD FOREIGN KEY([EmailId])
REFERENCES [dbo].[EmailSend] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailSendStudent]  WITH CHECK ADD FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Student] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([id])
GO
ALTER TABLE [dbo].[QuizStudent]  WITH CHECK ADD FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quiz] ([id])
GO
ALTER TABLE [dbo].[QuizStudent]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[ReservedClass]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReservedClass]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentModule]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentModule]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingProgramModule]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[TrainingProgramModule]  WITH CHECK ADD FOREIGN KEY([ProgramId])
REFERENCES [dbo].[TrainingProgram] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClass]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClass]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([roleid])
REFERENCES [dbo].[Role] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD CHECK  (([Gender]>=(1) AND [Gender]<=(3)))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Gender]>=(1) AND [Gender]<=(3)))
GO
USE [master]
GO
ALTER DATABASE [FAMS] SET  READ_WRITE
GO


