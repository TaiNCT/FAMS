use [FAMS];
GO





DELETE FROM [Student];
GO
DELETE FROM [TrainingProgram];
GO
DELETE FROM [Class];
GO
DELETE FROM [StudentClass];
GO

-- Fake data of table "Student"
INSERT INTO [Student](Fullname,DOB,Gender,Email,Major,GraduatedDate,GPA,Address,Location,Permanent_Residence,FAAccount,Type,Status,JoinedDate,Area,RECer,University)
VALUES
  ('Lara Carpenter','Nov 1, 2024',2,'lobortis.nisi@protonmail.org',5,'Jun 10, 2024',4,'Ap #369-3953 Sem Rd.','P.O. Box 560, 7649 Orci. Road','Germany','elementum at, egestas a, scelerisque sed, sapien.',2,'false','Sep 16, 2023','Ap #145-2613 Tempus Rd.','IFC76LFA4IB','VEU14BFV4DR'),
  ('Ashton Pollard','May 13, 2023',1,'eu.tempor.erat@outlook.net',7,'May 3, 2023',80,'1187 Sem Ave','Ap #296-8618 Ornare. Ave','Sweden','semper et, lacinia vitae, sodales at, velit.',9,'false','Feb 28, 2024','Ap #267-6923 Malesuada Ave','QSH10IFZ1PM','HWG46TOV4UP'),
  ('Christen York','Jan 27, 2024',3,'orci.sem@aol.ca',7,'Apr 5, 2023',31,'Ap #916-3832 Gravida Rd.','Ap #113-5359 Tristique St.','Philippines','imperdiet non,',2,'true','Sep 30, 2024','708-9633 Suspendisse Rd.','IMP38VWF5CG','OGA74EBP5RX'),
  ('Claudia Case','Nov 20, 2023',1,'molestie@google.net',9,'May 31, 2023',33,'7713 Ligula Rd.','Ap #313-7463 Turpis. Ave','Spain','orci, adipiscing non, luctus sit',6,'true','Feb 7, 2024','Ap #751-4867 Euismod Ave','GFC08HOK2CU','IWI23IEJ2IT'),
  ('Jorden Abbott','Feb 5, 2024',2,'donec@protonmail.com',6,'Feb 8, 2024',1,'Ap #149-2309 Blandit Rd.','256-7273 Turpis Avenue','Poland','euismod mauris eu',1,'false','Nov 30, 2024','664-1427 Sapien. Ave','OHM38CUG5VP','NUD83DLK2RK');


INSERT INTO TrainingProgram (Name, Status, Code, Duration, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) 
VALUES 
('Introduction to SQL', 1, 'SQL101', '02:30:00', '2024-01-28 08:00:00', 101, '2024-01-28 08:00:00', 101),
('Advanced Data Analysis', 0, 'ADA202', '01:45:00', '2024-01-27 10:30:00', 102, '2024-01-27 12:15:00', 102),
('Python for Beginners', 1, 'PYTH101', '03:15:00', '2024-01-26 14:00:00', 103, '2024-01-28 09:30:00', 103);


SELECT * FROM TrainingProgram;
INSERT INTO Class(ClassName, StartDate, EndDate, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, Duration, Location, Status, ProgramId) 
VALUES 
('Web Development Fundamentals', CONVERT(datetime, '2024-02-05 09:00:00', 120), CONVERT(datetime, '2024-02-07 17:00:00', 120), CONVERT(datetime, '2024-01-28 10:00:00', 120), 201, CONVERT(datetime, '2024-01-28 10:00:00', 120), 201, '20:00:00', 'Virtual Classroom', 1, 7),
('Data Science Workshop', CONVERT(datetime, '2024-02-10 13:00:00', 120), CONVERT(datetime, '2024-02-12 16:00:00', 120), CONVERT(datetime, '2024-01-25 11:30:00', 120), 202, CONVERT(datetime, '2024-01-28 11:30:00', 120), 202, '15:00:00', 'Conference Room A', 1, 7),
('Mobile App Development Bootcamp', CONVERT(datetime, '2024-02-15 10:00:00', 120), CONVERT(datetime, '2024-02-18 18:00:00', 120), CONVERT(datetime, '2024-01-22 09:00:00', 120), 203, CONVERT(datetime, '2024-01-28 09:00:00', 120), 203, '32:00:00', 'Training Center B', 0, 8),
('UI/UX Design Crash Course', CONVERT(datetime, '2024-02-20 14:00:00', 120), CONVERT(datetime, '2024-02-21 17:30:00', 120), CONVERT(datetime, '2024-01-20 14:30:00', 120), 204, CONVERT(datetime, '2024-01-28 14:30:00', 120), 204, '09:30:00', 'Design Lab', 1, 9),
('Cybersecurity Essentials', CONVERT(datetime, '2024-02-25 09:30:00', 120), CONVERT(datetime, '2024-02-27 16:30:00', 120), CONVERT(datetime, '2024-01-18 08:45:00', 120), 205, CONVERT(datetime, '2024-01-28 08:45:00', 120), 205, '19:00:00', 'Security Training Room', 1, 8);


SELECT * FROM Class;

INSERT INTO [dbo].[StudentClass]
           ([StudentId]
           ,[ClassId]
           ,[AttendingStatus]
           ,[Result]
           ,[FinalScore]
           ,[GPALevel]
           ,[CertificationStatus]
           ,[CertificationDate]
           ,[Method])
     VALUES
           (1, 1, 3, 3, 15, 15, 'true', '2024-02-05 09:00:00', 2),
           (2, 1, 3, 3, 15, 15, 'true', '2024-02-05 09:00:00', 2),
           (2, 2, 3, 3, 15, 15, 'true', '2024-02-05 09:00:00', 2),
           (3, 3, 3, 3, 15, 15, 'true', '2024-02-05 09:00:00', 2),
           (4, 3, 3, 3, 15, 15, 'true', '2024-02-05 09:00:00', 2)
GO