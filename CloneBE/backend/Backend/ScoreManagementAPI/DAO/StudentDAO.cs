using ScoreManagementAPI.DTO;
using Newtonsoft.Json.Linq;
using Entities.Context;
using Entities.Models;

namespace ScoreManagementAPI.DAO
{
    public class StudentDAO
    {
        private readonly ILogger<StudentDAO> _logger;
        private FamsContext context;

        public StudentDAO(ILogger<StudentDAO> logger)
        {
            _logger = logger;
        }

        public StudentDAO(FamsContext context)
        {
            this.context = context;
        }


        public StudentDTO GetStudentByID(string studentId)
        {
            return FromStudentToStudentDTO(GetStudentList().SingleOrDefault(s => s.StudentId.Equals(studentId)));
        }

        public IEnumerable<Student> GetStudentList()
        {
            return context.Students.ToList();
        }

        public IEnumerable<QuizDTO> GetQuizzesTakenByStudent(string studentId)
        {
            var quizDTOs = context.QuizStudents
            .Where(qs => qs.StudentId == studentId)
            .Select(qs => FromQuizToQuizDTO(qs.Quiz, studentId))
            .ToList();
            return quizDTOs;
        }

        public IEnumerable<AssignmentDTO> GetAsmsTakenByStudent(string studentId)
        {
            var quizDTOs = context.Scores
            .Where(qs => qs.StudentId == studentId)
            .Select(qs => FromAsmToAsmmDTO(qs.Assignment, studentId))
            .ToList();
            return quizDTOs;
        }

        public IEnumerable<AssignmentDTO> GetModuleOfStudent(string studentId)
        {
            var quizDTOs = context.Scores
            .Where(qs => qs.StudentId == studentId)
            .Select(qs => FromAsmToAsmmDTO(qs.Assignment, studentId))
            .ToList();
            return quizDTOs;
        }

        public string GetMajorById(string majorId)
        {
            return context.Majors
            .FirstOrDefault(qs => qs.MajorId == majorId).Name;
        }

        public QuizDTO FromQuizToQuizDTO(Quiz quiz, string studentId)
        {
            QuizDTO quizDTO = new QuizDTO();
            quizDTO.Id = quiz.Id;
            quizDTO.QuizId = context.QuizStudents.FirstOrDefault(qs => qs.QuizId == quiz.QuizId && qs.StudentId == studentId)?.QuizId;
            quizDTO.UpdatedBy = quiz.UpdatedBy;
            quizDTO.UpdatedDate = quiz.UpdatedDate;
            quizDTO.CreateDate = quiz.CreateDate;
            quizDTO.QuizName = quiz.QuizName;
            quizDTO.QuizStudentDTO = context.QuizStudents.FirstOrDefault(qs => qs.QuizId == quiz.QuizId && qs.StudentId == studentId)?.QuizStudentId;
            quizDTO.QuizId = context.QuizStudents.FirstOrDefault(qs => qs.QuizId == quiz.QuizId && qs.StudentId == studentId)?.QuizId;
            quizDTO.QuizScore = (double?)context.QuizStudents.FirstOrDefault(qs => qs.QuizId == quiz.QuizId && qs.StudentId == studentId)?.Score ?? 0;
            return quizDTO;
        }

        public AssignmentDTO FromAsmToAsmmDTO(Assignment asm, string studentId)
        {
            AssignmentDTO AsmDTO = new AssignmentDTO();
            AsmDTO.AssignmentId = context.Scores.FirstOrDefault(qs => qs.AssignmentId == asm.AssignmentId && qs.StudentId == studentId)?.AssignmentId;
            AsmDTO.AssignmentName = asm.AssignmentName;
            AsmDTO.Id = asm.Id;

            AsmDTO.AssignmentScore = (double?)context.Scores.FirstOrDefault(qs => qs.AssignmentId == asm.AssignmentId && qs.StudentId == studentId)?.Score1 ?? 0;

            return AsmDTO;

        }

        /*        public IEnumerable<StudentDTO> FromStudentsToStudentDTOs(IEnumerable<Student> students)
                {
                    List<StudentDTO> list = new List<StudentDTO>();
                    foreach (var student in students)
                    {
                        list.Add(FromStudentToStudentDTO(student));
                    }
                    return list;
                }
        */


        public StudentDTO FromStudentToStudentDTO(Student student)
        {
            StudentDTO studentDTO = new StudentDTO();
            studentDTO.Status = student.Status;
            studentDTO.StudentId = student.StudentId;
            studentDTO.Address = student.Address;
            studentDTO.Email = student.Email;
            studentDTO.Phone = student.Phone;
            studentDTO.FullName = student.FullName;
            studentDTO.Area = student.Area;
            studentDTO.Major = GetMajorById(student.MajorId);
            studentDTO.Gpa = (double)student.Gpa;
            studentDTO.GraduatedDate = student.GraduatedDate.ToString("MM/dd/yyyy");
            studentDTO.Dob = student.Dob;
            studentDTO.Id = student.MutatableStudentId;
            studentDTO.Faaccount = student.Faaccount;
            studentDTO.Gender = student.Gender ;
            studentDTO.University = student.University;
            studentDTO.RECer = student.Recer;
            studentDTO.Location = student.Address;
            studentDTO.Mock = student.Mock;
            studentDTO.Audit = student.Audit;
            return studentDTO;
        }
    }
}
