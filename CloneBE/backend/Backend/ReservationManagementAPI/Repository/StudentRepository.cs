using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ReservationManagementAPI;
using ReservationManagementAPI.Contracts;
using Entities;
using ReservationManagementAPI.Entities.DTOs;
using ReservationManagementAPI.Entities.Errors;
using Entities.Models;
using ReservationManagementAPI.Exceptions;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Entities.Context;
using MassTransit;
using Contracts.StudentManagement;

namespace ReservationManagementAPI.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        private readonly FamsContext _repositoryContext;
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IPublishEndpoint _publishEndpoint;



        public StudentRepository(FamsContext repositoryContext) : base(repositoryContext)
        {
        }

        public StudentRepository(FamsContext repositoryContext, IMapper mapper,  
            IClassRepository classRepository,
            IModuleRepository moduleRepository, IPublishEndpoint publishEndpoint)
            : base(repositoryContext)
        {
            this._repositoryContext = repositoryContext;
            _mapper = mapper;
            _classRepository = classRepository;
            _moduleRepository = moduleRepository;
            _publishEndpoint = publishEndpoint;
        }


        public async Task<List<StudentReservedDTO>> GetAllReservedStudents(int pageNumber)
        {          
            int pageSize = 10; 

            List<ReservedClass> listStudentReserved = await RepositoryContext.ReservedClasses
                .ToListAsync();

            List<StudentReservedDTO> listStudentReservedDTO = new List<StudentReservedDTO>();

            List<StudentClass> studentClassList = await RepositoryContext.StudentClasses.ToListAsync();

            List<ReservedClass> listStudentClassReserved = new List<ReservedClass>();

            foreach (var i in listStudentReserved)
            {
                var student = await RepositoryContext.StudentClasses
                    .FirstOrDefaultAsync(s => s.StudentId.Equals(i.StudentId) && s.ClassId.Equals(i.ClassId));

                if (student != null)
                {
                    listStudentClassReserved.Add(i);
                }
            }

            foreach (var i in listStudentClassReserved)
            {
                var student = await GetStudentById(i.StudentId);
                var studentClass = await RepositoryContext.StudentClasses.FirstOrDefaultAsync(s => s.StudentId.Equals(i.StudentId) && s.ClassId.Equals(i.ClassId));

                if (studentClass.AttendingStatus.Equals("Reserve", StringComparison.OrdinalIgnoreCase))
                {
                    var objClass = await _classRepository.GetClassByClassId(i.ClassId);
                    var objModule = await _moduleRepository.GetModuleByTrainingProgramId(objClass.TrainingProgramCode);
                    StudentReservedDTO studentReservedDTO = new StudentReservedDTO
                    {
                        ReservedClassId = i.ReservedClassId,
                        StudentId = i.StudentId,
                        MutatableStudentId = student.MutatableStudentId,
                        ClassId = i.ClassId,
                        Reason = i.Reason,
                        StartDate = i.StartDate.ToString("dd/MM/yyyy"),
                        EndDate = i.EndDate.ToString("dd/MM/yyyy"),
                        ClassName = objClass.ClassName,
                        ModuleName = objModule.ModuleName,
                        StudentName = student.FullName,
                        Dob = student.Dob.ToString("dd/MM/yyyy"),
                        Gender = (student.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase)) ? "Male" : "Female",
                        Address = student.Address,
                        Email = student.Email,
                        ClassEndDate = objClass.EndDate.ToString("dd/MM/yyyy")
                    };
                    listStudentReservedDTO.Add(studentReservedDTO);
                }
            }

            return listStudentReservedDTO;


        }

        public async Task<Result> UpdateStatusStudent(string studentId, string reservedClassId)
        {

            if (string.IsNullOrEmpty(studentId))
            {
                return Result.Failure(StudentMessages.StudentIdIsNull);
            }

            if (string.IsNullOrEmpty(reservedClassId))
            {
                return Result.Failure(ReservedClassMessages.ReservedClassIdIsNull);
            }

            var updatedStudentClass = await _repositoryContext.ReservedClasses.Where(rc => rc.ReservedClassId.ToUpper().Equals(reservedClassId.ToUpper())
                                                                                        && rc.StudentId.ToUpper().Equals(studentId.ToUpper())).FirstOrDefaultAsync();


            if (updatedStudentClass is null)
            {
                return Result.Failure(StudentMessages.StudentNotFound);
            }

            var studentClass = await RepositoryContext.StudentClasses
                .FirstOrDefaultAsync(st => st.StudentId.ToUpper().Equals(studentId.ToUpper())
                                            && st.ClassId.ToUpper().Equals(updatedStudentClass.ClassId.ToUpper()));

            if (studentClass == null)
            {
                return Result.Failure(StudentMessages.StudentNotFound);
            }

            if (studentClass.AttendingStatus.Equals("Reserve", StringComparison.OrdinalIgnoreCase))
            {
                var student = await RepositoryContext.Students.Where(s => s.StudentId == studentId).FirstOrDefaultAsync();
                GeneralStudentUpdated generalStudentUpdated = new GeneralStudentUpdated()
                {
                    classid = studentClass.ClassId,
                    studentid = studentClass.StudentId,
                    sid = student.MutatableStudentId,
                    name = student.FullName,
                    gender = student.Gender,
                    dob = student.Dob,
                    permanentResidence = student.Address,
                    status = "DropOut",
                    phone = student.Phone,
                    email = student.Email,
                    certificateStatus = studentClass.CertificationStatus == 1 ? true : false,
                    certificateDate = studentClass.CertificationDate
                };
                await _publishEndpoint.Publish(generalStudentUpdated);
                //delete student from ReservedClass
                RepositoryContext.Remove(updatedStudentClass);
                await RepositoryContext.SaveChangesAsync();
            }
            else
            {
                return Result.Failure(StudentMessages.StudentNotReserved);
            }

            return Result.Success(StudentMessages.StudentUpdateSuccessful);

        }

        public async Task<StudentDTO> GetStudentById(string studentId)
        {
            // Fetch student from database by ID
            var student = await RepositoryContext.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);

            // If student is found, map it to DTO and return
            if (student != null)
            {
                var studentDTO = _mapper.Map<StudentDTO>(student);
                return studentDTO;
            }

            // If student is not found, return null
            return null;
        }

        public async Task<List<ClassDTO>>? SearchStudent(string studentIdOrEmail)
        {
            List<ClassDTO> classes = new List<ClassDTO>();

            var result = await _repositoryContext.StudentClasses
                            .Include(sc => sc.Class)
                            .ThenInclude(c => c.TrainingProgramCodeNavigation)
                            .ThenInclude(tp => tp.TrainingProgramModules)
                            .ThenInclude(tm => tm.Module)
                            .Include(sc => sc.Student)
                            .Where(sc => (sc.StudentId.ToLower() == studentIdOrEmail.ToLower()
                                    || sc.Student.Email == studentIdOrEmail)
                                    && sc.AttendingStatus.ToLower().Equals("InClass".ToLower())).ToListAsync();

            if (result == null || result.Count == 0)
            {
                throw new Exception("Students not found");
                return null;
            }

            foreach (var studentClass in result)
            {
                var classDto = new ClassDTO()
                {
                    StudentId = studentClass.StudentId,
                    ClassId = studentClass.ClassId,
                    ClassCode = studentClass.Class.ClassCode,
                    ClassName = studentClass.Class.ClassName,
                    ModuleName = studentClass.Class.TrainingProgramCodeNavigation.
                    TrainingProgramModules.FirstOrDefault().Module.ModuleName
                };
                classes.Add(classDto);
            }
            return classes;
        }



        public async Task<List<StudentReservedDTO>> GetAllReservedStudentsByReserveTime()
        {
            /*var list = await RepositoryContext.Students.Where(student => student.Status.Equals("Reserved"))
                                                         .Include(Student => Student.ReservedClasses)
                                                            .ThenInclude(ReservedClasses => ReservedClasses.Class)
                                                         .Include(Student => Student.ReservedClasses)
                                                            .ThenInclude(ReservedClasses => ReservedClasses.Student)
                                                         .ToListAsync();
            var result = list.Select(student => new StudentReservedDTO
            {
                ReservedClassId = student?.ReservedClasses?.FirstOrDefault()?.Class?.ReservedClasses.FirstOrDefault()?.ReservedClassId,
                StudentId = student?.ReservedClasses?.FirstOrDefault()?.Student?.StudentId,
                ClassId = student?.ReservedClasses?.FirstOrDefault()?.Class?.ClassId,
                Reason = student?.ReservedClasses?.FirstOrDefault()?.Reason,
                StartDate = student?.ReservedClasses?.FirstOrDefault()?.StartDate.ToString("dd/MM/yyyy"),
                EndDate = student?.ReservedClasses?.FirstOrDefault()?.EndDate.ToString("dd/MM/yyyy"),
                ClassName = student?.ReservedClasses?.FirstOrDefault()?.Class?.ClassName,
                ModuleName = student?.ReservedClasses?.FirstOrDefault()?.Class?.Module?.ModuleName,
                StudentName = student?.FullName,
                Dob = student?.Dob.ToString("dd/MM/yyyy"),
                Gender = (student?.Gender == "1") ? "Male" : "Female",
                Address = student?.Address,
                Email = student?.Email
            }).OrderBy(student => (DateTime.ParseExact(student?.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                    - DateTime.ParseExact(student?.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Days).ToList();

            return result;*/

            List<ReservedClass> listStudentReserved = await RepositoryContext.ReservedClasses.ToListAsync();

            List<StudentReservedDTO> listStudentReservedDTO = new List<StudentReservedDTO>();

            List<StudentClass> studentClassList = await RepositoryContext.StudentClasses.ToListAsync();

            List<ReservedClass> listStudentClassReserved = new List<ReservedClass>();

            foreach (var i in listStudentReserved)
            {
                var student = await RepositoryContext.StudentClasses.FirstOrDefaultAsync(s => s.StudentId.Equals(i.StudentId) && s.ClassId.Equals(i.ClassId));

                if (student != null)
                {
                    listStudentClassReserved.Add(i);
                }

            }

            foreach (var i in listStudentClassReserved)
            {
                var student = await GetStudentById(i.StudentId);
                var studentClass = await RepositoryContext.StudentClasses.FirstOrDefaultAsync(s => s.StudentId.Equals(i.StudentId));

                if (studentClass.AttendingStatus.Equals("Reserve", StringComparison.OrdinalIgnoreCase))
                {
                    var objClass = await _classRepository.GetClassByClassId(i.ClassId);
                    var objModule = await _moduleRepository.GetModuleByTrainingProgramId(objClass.TrainingProgramCode);
                    StudentReservedDTO studentReservedDTO = new StudentReservedDTO
                    {
                        ReservedClassId = i.ReservedClassId,
                        StudentId = i.StudentId,
                        MutatableStudentId = student.MutatableStudentId,
                        ClassId = i.ClassId,
                        Reason = i.Reason,
                        StartDate = i.StartDate.ToString("dd/MM/yyyy"),
                        EndDate = i.EndDate.ToString("dd/MM/yyyy"),
                        ClassName = objClass.ClassName,
                        ModuleName = objModule.ModuleName,
                        StudentName = student.FullName,
                        Dob = student.Dob.ToString("dd/MM/yyyy"),
                        Gender = (student.Gender == "1") ? "Male" : "Female",
                        Address = student.Address,
                        Email = student.Email
                    };
                    listStudentReservedDTO.Add(studentReservedDTO);
                }
            }

            listStudentReservedDTO.OrderBy(student => student.StartDate).ToList();

            return listStudentReservedDTO;
        }

        public async Task<StudentReservedDTO> GetReservedStudentDTOByStudentIdAndClassId(string studentId, string classId, string reason, DateTime startDate, DateTime endDate, DateOnly classEndDate)
        {
            var student = await GetStudentById(studentId);
            StudentReservedDTO studentReservedDTO = new StudentReservedDTO();
            
            var studentClass = await _repositoryContext.StudentClasses.Where(c => c.ClassId == classId && c.StudentId == studentId).FirstOrDefaultAsync();
            //if (studentClass.AttendingStatus.Equals("Reserve", StringComparison.OrdinalIgnoreCase))
            //{
                var objClass = await _classRepository.GetClassByClassId(classId);
                var objModule = await _moduleRepository.GetModuleByTrainingProgramId(objClass.TrainingProgramCode);
                var objReservedClass = await _repositoryContext.ReservedClasses.Where(c => c.ClassId == classId && c.StudentId == studentId).FirstOrDefaultAsync();
            studentReservedDTO = new StudentReservedDTO
            {
                ReservedClassId = objReservedClass.ReservedClassId,
                StudentId = studentId,
                MutatableStudentId = student.MutatableStudentId,
                ClassId = classId,
                Reason = reason,
                StartDate = startDate.ToString("dd/MM/yyyy"),
                EndDate = endDate.ToString("dd/MM/yyyy"),
                ClassName = objClass.ClassName,
                ModuleName = objModule.ModuleName,
                StudentName = student.FullName,
                Dob = student.Dob.ToString("dd/MM/yyyy"),
                Gender = (student.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase)) ? "Male" : "Female",
                Address = student.Address,
                Email = student.Email,
                ClassEndDate = classEndDate.ToString("dd/MM/yyyy"),
                CreatedDate = DateTime.Now,
                };
        //}
            return studentReservedDTO;

        }

        public async Task<Result> UpdateReserveStatusStudent(string studentId, string reservedClassId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return Result.Failure(StudentMessages.StudentIdIsNull);
            }

            if (string.IsNullOrEmpty(reservedClassId))
            {
                return Result.Failure(ReservedClassMessages.ReservedClassIdIsNull);
            }

            var updatedStudentClass = await _repositoryContext.ReservedClasses.Where(rc => rc.ReservedClassId.ToUpper().Equals(reservedClassId.ToUpper())
                                                                                        && rc.StudentId.ToUpper().Equals(studentId.ToUpper())).FirstOrDefaultAsync();
                 

            if (updatedStudentClass is null)
            {
                return Result.Failure(StudentMessages.StudentNotFound);
            }

            var studentClass = await RepositoryContext.StudentClasses
                .FirstOrDefaultAsync(st => st.StudentId.ToUpper().Equals(studentId.ToUpper())
                                            && st.ClassId.ToUpper().Equals(updatedStudentClass.ClassId.ToUpper()));

            if (studentClass == null)
            {
                return Result.Failure(StudentMessages.StudentNotFound);
            }

            if (studentClass.AttendingStatus.Equals("Reserve", StringComparison.OrdinalIgnoreCase))
            {
                var student = await RepositoryContext.Students.Where(s => s.StudentId == studentId).FirstOrDefaultAsync();
                GeneralStudentUpdated generalStudentUpdated = new GeneralStudentUpdated()
                {
                    classid = studentClass.ClassId,
                    studentid = studentClass.StudentId,
                    sid = student.MutatableStudentId,
                    name = student.FullName,
                    gender = student.Gender,
                    dob = student.Dob,
                    permanentResidence = student.Address,
                    status = "InClass",
                    phone = student.Phone,
                    email = student.Email,
                    certificateStatus = studentClass.CertificationStatus == 1 ? true : false,
                    certificateDate = studentClass.CertificationDate
                };
                await _publishEndpoint.Publish(generalStudentUpdated);
                //delete student from ReservedClass
                RepositoryContext.Remove(updatedStudentClass);
                await RepositoryContext.SaveChangesAsync();
            }
            else
            {
                return Result.Failure(StudentMessages.StudentNotReserved);
            }


            return Result.Success(StudentMessages.StudentUpdateSuccessful);
        }

        public async Task AddStudentBackToClass(string reClassId, string studentId)
        {
            StudentClassCreated studentClassCreated = new StudentClassCreated()
            {
                StudentId = studentId,
                ClassId = reClassId,
                AttendingStatus = "InClass",
                Result = 0,
                FinalScore = 0,
                Gpalevel = 0,
                CertificationStatus = 0,
                CertificationDate = null,
                Method = 1
            };
            await _publishEndpoint.Publish(studentClassCreated);

        }

    }
}
