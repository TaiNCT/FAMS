using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationManagementAPI.Contracts;
using Entities;
using ReservationManagementAPI.Entities.DTOs;
using Entities.Models;
using ReservationManagementAPI.Repository;
using System.Collections.Generic;
using Entities.Context;

namespace ReservationManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReClassController : ControllerBase
    {
        private readonly FamsContext _repositoryContext;
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IReservedClassRepository _reservedClassRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        public ReClassController(FamsContext repositoryContext,
            IStudentRepository studentRepository, 
            IClassRepository classRepository,
            IReservedClassRepository reservedClassRepository,
            IModuleRepository moduleRepository,
            IQuizRepository quizRepository,
            IAssignmentRepository assignmentRepository)
        {
            _repositoryContext = repositoryContext;
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _reservedClassRepository = reservedClassRepository;
            _moduleRepository = moduleRepository;
            _quizRepository = quizRepository;
            _assignmentRepository = assignmentRepository;

        }
        [HttpGet("GetQuizListStudent/{reservedClassId}", Name = "GetQuizListStudent")]
        public async Task<List<QuizStudentDTO>> GetQuizListStudent (string reservedClassId)
        {
            List<QuizStudentDTO>  listQuizStudentDTO = new List<QuizStudentDTO>();

            var reservedClass = await _reservedClassRepository.GetReservedClassByReservedClassId(reservedClassId);
            var objClass = await _classRepository.GetClassByClassId(reservedClass.ClassId);
            var objModule = await _moduleRepository.GetModuleByTrainingProgramId(objClass.TrainingProgramCode);

            List<Quiz> listQuiz = await _repositoryContext.Quizzes.ToListAsync();
            List<QuizStudent> listQuizStudent = await _repositoryContext.QuizStudents.ToListAsync();

            var resultListQuizStudent = from quizStu in listQuizStudent
                                        join quiz in listQuiz
                                        on quizStu.QuizId equals quiz.QuizId
                                        where quiz.ModuleId.Equals(objModule.ModuleId)
                                        && quizStu.StudentId.Equals(reservedClass.StudentId)
                                        select quizStu;

            foreach (var i in resultListQuizStudent)
            {
                var objQuiz = await _quizRepository.GetQuizByQuizId(i.QuizId);
                QuizStudentDTO quizStudentDTO = new QuizStudentDTO
                {
                    QuizStudentId = i.QuizStudentId,
                    StudentId = i.StudentId,
                    QuizId = i.QuizId,
                    QuizName = objQuiz.QuizName,
                    QuizScore = i.Score,
                    SubmissionDate = i.SubmissionDate
                };
                listQuizStudentDTO.Add(quizStudentDTO);
            }
            return listQuizStudentDTO;
        }

        [HttpGet("GetAssignmentListStudent/{reservedClassId}", Name = "GetAssignmentListStudent")]
        public async Task<List<AssignmentStudentDTO>> GetAssignmentListStudent(string reservedClassId)
        {
            List<AssignmentStudentDTO> listAssignmentStudentDTO = new List<AssignmentStudentDTO>();

            var reservedClass = await _reservedClassRepository.GetReservedClassByReservedClassId(reservedClassId);
            var objClass = await _classRepository.GetClassByClassId(reservedClass.ClassId);
            var objModule = await _moduleRepository.GetModuleByTrainingProgramId(objClass.TrainingProgramCode);

            List<Assignment> listAssignment = await _repositoryContext.Assignments.ToListAsync();
            List<Score> listScore = await _repositoryContext.Scores.ToListAsync();

            var resultListAssignmentStudent = from score in listScore
                                        join ass in listAssignment
                                        on score.AssignmentId equals ass.AssignmentId
                                        where score.StudentId.Equals(reservedClass.StudentId)
                                        && ass.ModuleId.Equals(objModule.ModuleId)
                                        select score;

            foreach (var i in resultListAssignmentStudent)
            {
                var objAssignment = await  _assignmentRepository.GetAssignmentByAssignmentId(i.AssignmentId);
                AssignmentStudentDTO assignmentStudentDTO = new AssignmentStudentDTO
                {
                    AssignmentId = i.AssignmentId,
                    AssignmentName = objAssignment.AssignmentName,
                    AssignmentScore = i.Score1,
                    StudentId = i.StudentId
                };
                
                listAssignmentStudentDTO.Add(assignmentStudentDTO);
            }
            return listAssignmentStudentDTO;
        }

        [HttpGet("GetReClassDialogInfo/{reservedClassId}", Name = "GetReClassDialogInfo")]
        public async Task<ReClassDialogDTO> GetReClassDialogInfo (string reservedClassId)
        {
            var reservedClass = await _reservedClassRepository.GetReservedClassByReservedClassId(reservedClassId);
            var objClass = await _classRepository.GetClassByClassId(reservedClass.ClassId);
            var objModule = await _moduleRepository.GetModuleByTrainingProgramId(objClass.TrainingProgramCode);

            ReClassDialogDTO reClassDialogDTO = new ReClassDialogDTO
            {
                StudentId = reservedClass.StudentId,
                ClassId = objClass.ClassId,
                ClassName = objClass.ClassName,
                ClassCode = objClass.ClassCode,
                ClassStartDate = objClass.StartDate.ToString("dd/MM/yyyy"),
                ClassEndDate = objClass.EndDate.ToString("dd/MM/yyyy"),
                ModuleId = objModule.ModuleId,
                ModuleName = objModule.ModuleName,
                ReservedStartDate = reservedClass.StartDate.ToString("dd/MM/yyyy"),
                ReservedEndDate = reservedClass.EndDate.ToString("dd/MM/yyyy"),
                Reason = reservedClass.Reason
            };

            return reClassDialogDTO;
        }

        [HttpGet("GetReClassPossibilities/{reservedClassId}", Name = "GetReClassPossibilies")]
        public async Task<List<ReClassDTO>> GetReClassPossibilies(string reservedClassId)
        {
            var reservedClass = await _reservedClassRepository.GetReservedClassByReservedClassId(reservedClassId);
            var objClass = await _classRepository.GetClassByClassId(reservedClass.ClassId);

            /*lay ra tat ca syllabus cua training program cua lop da bao luu*/
            List<TrainingProgramSyllabus> listSyllabus = await _repositoryContext.TrainingProgramSyllabi
                .Where(c => c.TrainingProgramCode == objClass.TrainingProgramCode)
                .ToListAsync();
            /*Lay ra toan bo bang TrainingProgramSyllabus*/
            List<TrainingProgramSyllabus> listAllSyllabus = await _repositoryContext.TrainingProgramSyllabi.ToListAsync();
            /*Lay ra toan bo training program co chua cac syllabus cua lop da bao luu*/
            List<TrainingProgramSyllabus> listTrainingProgramBySyllabus = new List<TrainingProgramSyllabus>();
            foreach (var i in listSyllabus)
            {
                foreach (var j in listAllSyllabus)
                {
                    if (i.SyllabusId == j.SyllabusId)
                    {
                            if (!listTrainingProgramBySyllabus.Any(c => c.TrainingProgramCode == j.TrainingProgramCode))
                            {
                                listTrainingProgramBySyllabus.Add(j);
                            }
                    }
                }
            }

            /*lấy tất cả các class có training program giống trên*/
            List<ReClassDTO> listClassByTrainingProgram = new List<ReClassDTO>();

            foreach (var i in listTrainingProgramBySyllabus)
            {
                var classDb = await _repositoryContext.Classes
                     .Where(c => c.TrainingProgramCode == i.TrainingProgramCode).ToListAsync();
                foreach (var j in classDb)
                {
                    var checkStudentInClass = await _classRepository.CheckStudentAlreadyInClass(reservedClass.StudentId, j.ClassId);
                    if (!checkStudentInClass)
                    {
                        var objModule = await _moduleRepository.GetModuleByTrainingProgramId(j.TrainingProgramCode);

                        if (j.ClassId != reservedClass.ClassId)
                        {
                            if (listClassByTrainingProgram.Count <= 5)
                            {
                                if (!j.ClassStatus.Equals("Finished") && !j.ClassStatus.Equals("Closed") && !j.ClassStatus.Equals("Deactive"))
                                {
                                    ReClassDTO reClassDTO = new ReClassDTO
                                    {
                                        ClassId = j.ClassId,
                                        ClassStatus = j.ClassStatus,
                                        ClassName = j.ClassName,
                                        ClassCode = j.ClassCode,
                                        StartDate = j.StartDate.ToString("dd/MM/yyyy"),
                                        EndDate = j.EndDate.ToString("dd/MM/yyyy"),
                                        TrainingProgramCode = j.TrainingProgramCode,
                                        ModuleName = objModule.ModuleName
                                    };
                                    listClassByTrainingProgram.Add(reClassDTO);
                                }
                            }
                        }
                    }
                }
                
            }
            return listClassByTrainingProgram;
        }


        [HttpGet("GetMock/{reservedClassId}", Name = "GetMock")]
        public async Task<MockDTO> GetMock (string reservedClassId)
        {
            var reservedClass = await _reservedClassRepository.GetReservedClassByReservedClassId(reservedClassId);
            var student = await _repositoryContext.Students.Where(c => c.StudentId == reservedClass.StudentId).FirstOrDefaultAsync();
            var objClass = await _classRepository.GetClassByClassId(reservedClass.ClassId);
            var objModule = await _moduleRepository.GetModuleByTrainingProgramId(objClass.TrainingProgramCode);
            var objStudentModule = await _repositoryContext.StudentModules.Where(c => c.ModuleId == objModule.ModuleId &&
            c.StudentId == reservedClass.StudentId).FirstOrDefaultAsync();
            MockDTO mockDTO = new MockDTO()
            {
                Mock = student.Mock,
                ModuleScoreFinal = objStudentModule.ModuleScore,
                GPA = student.Gpa,
                ModuleLevel = objStudentModule.ModuleLevel
            };
            return mockDTO;
        }

        [HttpGet("GetNextClassModuleList/{reservedClassId}/{reClassId}", Name = "GetNextClassModuleList")]
        public async Task<List<NextClassModuleDTO>> GetNextClassModuleList(string reservedClassId, string reClassId)
        {
            var reservedClass = await _reservedClassRepository.GetReservedClassByReservedClassId(reservedClassId);
            var objClass = await _classRepository.GetClassByClassId(reservedClass.ClassId);

            /*lay ra tat ca syllabus cua training program cua lop da bao luu*/
            List<TrainingProgramSyllabus> listSyllabus = await _repositoryContext.TrainingProgramSyllabi
                .Where(c => c.TrainingProgramCode == objClass.TrainingProgramCode)
                .ToListAsync();
            /*Lay ra toan bo bang TrainingProgramSyllabus*/
            List<TrainingProgramSyllabus> listAllSyllabus = await _repositoryContext.TrainingProgramSyllabi.ToListAsync();
            /*Lay ra toan bo training program co chua cac syllabus cua lop da bao luu*/
            List<TrainingProgramSyllabus> listTrainingProgramBySyllabus = new List<TrainingProgramSyllabus>();
            foreach (var i in listSyllabus)
            {
                foreach (var j in listAllSyllabus)
                {
                    if (i.SyllabusId == j.SyllabusId)
                    {
                        if (!listTrainingProgramBySyllabus.Any(c => c.TrainingProgramCode == j.TrainingProgramCode))
                        {
                            listTrainingProgramBySyllabus.Add(j);
                        }
                    }
                }
            }

            /*lấy ra 5 class có training program giống trên*/
            List<ReClassDTO> listClassByTrainingProgram = new List<ReClassDTO>();

            foreach (var i in listTrainingProgramBySyllabus)
            {
                var classDb = await _repositoryContext.Classes
                     .Where(c => c.TrainingProgramCode == i.TrainingProgramCode).ToListAsync();
                foreach (var j in classDb)
                {
                    //check nếu student đã học lớp đó với status in class thì sẽ k hiện ra
                    var checkStudentInClass = await _classRepository.CheckStudentAlreadyInClass(reservedClass.StudentId, j.ClassId);
                    if(!checkStudentInClass)
                    {
                        var objModule = await _moduleRepository.GetModuleByTrainingProgramId(j.TrainingProgramCode);

                        if (j.ClassId != reservedClass.ClassId)
                        {
                            if (listClassByTrainingProgram.Count <= 5)
                            {
                                if (!j.Equals("Completed"))
                                {
                                    ReClassDTO reClassDTO = new ReClassDTO
                                    {
                                        ClassId = j.ClassId,
                                        ClassStatus = j.ClassStatus,
                                        ClassName = j.ClassName,
                                        ClassCode = j.ClassCode,
                                        StartDate = j.StartDate.ToString("dd/MM/yyyy"),
                                        EndDate = j.EndDate.ToString("dd/MM/yyyy"),
                                        TrainingProgramCode = j.TrainingProgramCode,
                                        ModuleName = objModule.ModuleName
                                    };
                                    listClassByTrainingProgram.Add(reClassDTO);
                                }
                            }
                        }
                    }
                    
                }

            }

            List<NextClassModuleDTO> nextClassModuleListDTO = new List<NextClassModuleDTO>();
            foreach(var i in listClassByTrainingProgram)
            {
                if(i.ClassId != reClassId)
                {
                    var objModule = await _moduleRepository.GetModuleByTrainingProgramId(i.TrainingProgramCode);
                    NextClassModuleDTO nextClassModule = new NextClassModuleDTO
                    {
                        ClassId = i.ClassId,
                        ClassName = i.ClassName,
                        ClassCode = i.ClassCode,
                        ModuleId = objModule.ModuleId,
                        ModuleName = objModule.ModuleName,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                    };
                    nextClassModuleListDTO.Add(nextClassModule);
                }
            }
            return nextClassModuleListDTO;
        }

        [HttpPost("BackToClass/{reservedClassId}/{reClassId}")]
        public async Task<IActionResult> BackToClass(string reservedClassId, string reClassId)
        {
            try
            {
                var reservedClass = await _reservedClassRepository.GetReservedClassByReservedClassId(reservedClassId);
                await _studentRepository.AddStudentBackToClass(reClassId, reservedClass.StudentId);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            return Ok();
        }

    }
}
