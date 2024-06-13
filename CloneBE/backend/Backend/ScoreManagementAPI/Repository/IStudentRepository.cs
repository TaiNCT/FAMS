using Entities.Context;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Interfaces;
using Entities.Models;

namespace ScoreManagementAPI.Repository
{
    public interface IStudentRepository
    {

        // Getting the ID of quizes
        public string html { get; set; }
        public string css { get; set; }
        public string quiz3{ get; set;}
        public string quiz4{ get; set;}
        public string quiz5{ get; set; }
        public string quiz6 { get; set; }
        public string quizfinal { get; set; }


        // getting the ID of Assignments
        public string asm1{ get; set;}
        public string asm2{ get; set;}
        public string asm3{ get; set; }
        public string asmf { get; set; }

        IEnumerable<Student> GetStudents();
        public IQuiz getQuizByStudentID(string id);

        public IAsm getPracticesByStudentID(string id);

        StudentDTO GetStudentByID(string StudentId);

        void UpdateStudentBasedOnJSON(Student student, IStudentUpdate data);

    }
}
