using Newtonsoft.Json;
using Entities.Context;
using ScoreManagementAPI.DAO;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Interfaces;
using Entities.Models;
using ScoreManagementAPI.Singleton;

namespace ScoreManagementAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly FamsContext _context;


        // Getting the ID of quizes
        public string html { get; set; }
        public string css { get; set; }
        public string quiz3 { get; set; }
        public string quiz4 { get; set; }
        public string quiz5 { get; set; }
        public string quiz6 { get; set; }
        public string quizfinal { get; set; }


        // getting the ID of Assignments
        public string asm1 { get; set; }
        public string asm2 { get; set; }
        public string asm3 { get; set; }
        public string asmf { get; set; }

        private static readonly HttpClient client = new HttpClient();

        private Store _store;

        private string template;

        public StudentRepository(FamsContext context, Store store)
        {

            // Grabbing the email template
            this.template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Template", "email.html"));

            _context = context;
            this.studentDAO = new StudentDAO(context);

            _store = store;

            // Extracting ID from the database
            this.html = _context.Quizzes.SingleOrDefault(s => s.QuizName.Equals("HTML")).QuizId;
            this.css = _context.Quizzes.SingleOrDefault(s => s.QuizName.Equals("CSS")).QuizId;
            this.quiz3 = _context.Quizzes.SingleOrDefault(s => s.QuizName.Equals("Quiz 3")).QuizId;
            this.quiz4 = _context.Quizzes.SingleOrDefault(s => s.QuizName.Equals("Quiz 4")).QuizId;
            this.quiz5 = _context.Quizzes.SingleOrDefault(s => s.QuizName.Equals("Quiz 5")).QuizId;
            this.quiz6 = _context.Quizzes.SingleOrDefault(s => s.QuizName.Equals("Quiz 6")).QuizId;
            this.quizfinal = _context.Quizzes.SingleOrDefault(s => s.QuizName.Equals("Quiz Final")).QuizId;

            // Extracting the ID of Assignments
            this.asm1 = _context.Assignments.SingleOrDefault(s => s.AssignmentName.Equals("Practice 1")).AssignmentId;
            this.asm2 = _context.Assignments.SingleOrDefault(s => s.AssignmentName.Equals("Practice 2")).AssignmentId;
            this.asm3 = _context.Assignments.SingleOrDefault(s => s.AssignmentName.Equals("Practice 3")).AssignmentId;
            this.asmf = _context.Assignments.SingleOrDefault(s => s.AssignmentName.Equals("Practice Final")).AssignmentId;
        }

        public StudentDAO studentDAO;

        public StudentDTO GetStudentByID(string StudentId)
        {

            Student std = _context.Students.SingleOrDefault(s => s.StudentId.Equals(StudentId));
            return studentDAO.FromStudentToStudentDTO(std);
        }


        public IQuiz getQuizByStudentID(string id)
        {

            QuizStudent htmlo = _context.QuizStudents.Where(s => s.QuizId.Equals(html) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            QuizStudent csso = _context.QuizStudents.Where(s => s.QuizId.Equals(css) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            QuizStudent quiz3o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz3) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            QuizStudent quiz4o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz4) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            QuizStudent quiz5o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz5) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            QuizStudent quiz6o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz6) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            QuizStudent quizfinalo = _context.QuizStudents.Where(s => s.QuizId.Equals(quizfinal) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            return new IQuiz
            {
                html = htmlo == null ? null : (double)htmlo.Score,
                css = csso == null ? null : (double)csso.Score,
                quiz3 = quiz3o == null ? null : (double)quiz3o.Score,
                quiz4 = quiz4o == null ? null : (double)quiz4o.Score,
                quiz5 = quiz5o == null ? null : (double)quiz5o.Score,
                quiz6 = quiz6o == null ? null : (double)quiz6o.Score,
                quizfinal = quizfinalo == null ? null : (double)quizfinalo.Score
            };
        }


        public IAsm getPracticesByStudentID(string id)
        {

            Score asm1o = _context.Scores.Where(s => s.AssignmentId.Equals(asm1) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            Score asm2o = _context.Scores.Where(s => s.AssignmentId.Equals(asm2) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            Score asm3o = _context.Scores.Where(s => s.AssignmentId.Equals(asm3) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            Score asm = _context.Scores.Where(s => s.AssignmentId.Equals(asmf) && s.StudentId.Equals(id)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();

            return new IAsm
            {
                practice1 = asm1o == null ? null : (double)asm1o.Score1,
                practice2 = asm2o == null ? null : (double)asm2o.Score1,
                practice3 = asm3o == null ? null : (double)asm3o.Score1,
                practice_final = asm == null ? null : (double)asm.Score1,
            };
        }

        public void UpdateStudentBasedOnJSON(Student student, IStudentUpdate data)
        {

            student.Mock = data.mock;

            // Updating quizs
            var htmlo = _context.QuizStudents.Where(s => s.QuizId.Equals(html) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var csso = _context.QuizStudents.Where(s => s.QuizId.Equals(css) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var quiz3o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz3) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var quiz4o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz4) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var quiz5o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz5) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var quiz6o = _context.QuizStudents.Where(s => s.QuizId.Equals(quiz6) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var quizfinalo = _context.QuizStudents.Where(s => s.QuizId.Equals(quizfinal) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();

            if (htmlo != null)
            {
                htmlo.Score = (decimal?)data.html;
            }
            else
            {
                var newHtmlScore = new QuizStudent
                {
                    QuizId = html,
                    StudentId = student.StudentId,
                    Score = (decimal?)data.html
                };
                _context.QuizStudents.Add(newHtmlScore);
            }
            if (csso != null)
            {
                csso.Score = (decimal?)data.css;
            }
            else
            {
                var newCssScore = new QuizStudent
                {
                    QuizId = css,
                    StudentId = student.StudentId,
                    Score = (decimal?)data.css
                };
                _context.QuizStudents.Add(newCssScore);
            }

            if (quiz3o != null)
            {
                quiz3o.Score = (decimal?)data.quiz3;
            }
            else
            {
                var newQuiz3Score = new QuizStudent
                {
                    QuizId = quiz3,
                    StudentId = student.StudentId,
                    Score = (decimal?)data.quiz3
                };
                _context.QuizStudents.Add(newQuiz3Score);
            }
            if (quiz4o != null)
            {
                quiz4o.Score = (decimal?)data.quiz4;
            }
            else
            {
                var newQuiz4Score = new QuizStudent
                {
                    QuizId = quiz4,
                    StudentId = student.StudentId,
                    Score = (decimal?)data.quiz4
                };
                _context.QuizStudents.Add(newQuiz4Score);
            }

            if (quiz5o != null)
            {
                quiz5o.Score = (decimal?)data.quiz5;
            }
            else
            {
                var newQuiz5Score = new QuizStudent
                {
                    QuizId = quiz5,
                    StudentId = student.StudentId,
                    Score = (decimal?)data.quiz5
                };
                _context.QuizStudents.Add(newQuiz5Score);
            }

            if (quiz6o != null)
            {
                quiz6o.Score = (decimal?)data.quiz6;
            }
            else
            {
                var newQuiz6Score = new QuizStudent
                {
                    QuizId = quiz6,
                    StudentId = student.StudentId,
                    Score = (decimal?)data.quiz6
                };
                _context.QuizStudents.Add(newQuiz6Score);
            }

            if (quizfinalo != null)
            {
                quizfinalo.Score = (decimal?)data.final;
            }
            else
            {
                var newQuizFinalScore = new QuizStudent
                {
                    QuizId = quizfinal,
                    StudentId = student.StudentId,
                    Score = 1
                };
                _context.QuizStudents.Add(newQuizFinalScore);
            }


            // Updating assignments
            var asm1o = _context.Scores.Where(s => s.AssignmentId.Equals(asm1) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var asm2o = _context.Scores.Where(s => s.AssignmentId.Equals(asm2) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();
            var asm3o = _context.Scores.Where(s => s.AssignmentId.Equals(asm3) && s.StudentId.Equals(student.StudentId)).OrderByDescending(s => s.SubmissionDate).FirstOrDefault();

            // var asmfinalo = _context.Scores.Where(s => s.AssignmentId.Equals(asmfinal) && s.StudentId.Equals(student.StudentId)).FirstOrDefault();
            if (asm1o != null)
            {
                asm1o.Score1 = (decimal?)data.practice1;
            }
            else
            {
                var newAsm1Score = new Score
                {
                    AssignmentId = asm1,
                    StudentId = student.StudentId,
                    Score1 = (decimal?)data.practice1
                };
                _context.Scores.Add(newAsm1Score);
            }

            if (asm2o != null)
            {
                asm2o.Score1 = (decimal?)data.practice2;
            }
            else
            {
                var newAsm2Score = new Score
                {
                    AssignmentId = asm2,
                    StudentId = student.StudentId,
                    Score1 = (decimal?)data.practice2
                };
                _context.Scores.Add(newAsm2Score);
            }

            if (asm3o != null)
            {
                asm3o.Score1 = (decimal?)data.practice3;
            }
            else
            {
                var newAsm3Score = new Score
                {
                    AssignmentId = asm3,
                    StudentId = student.StudentId,
                    Score1 = (decimal?)data.practice3
                };
                _context.Scores.Add(newAsm3Score);
            }


            _context.SaveChanges();

            //Check if the student has passed all subjects, if yes then send email
            if (
               ((data.html + data.css + data.quiz3 + data.quiz4 + data.quiz5 + data.quiz6) / 6) >= 50 && // 1st condition : ave score of all quizs has to be bigger than 60
               ((data.practice1 + data.practice2 + data.practice3) / 3) >= 50 && // 2nd condition : ave score of all asm has to be bigger thaan 60
               (student.Gpa >= 50) &&
               (data.mock >= 50) &&
               (data.final > 50))
            {
                Console.WriteLine("/* Sent graduation email");
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    this.SendMail(data.email, data, student);
                }).Start();
            }

        }


        private void SendMail(string email, IStudentUpdate data, Student student)
        {
            // Crafting JSON data to send to the other endpoint
            var data_ = System.Text.Json.JsonSerializer.Serialize(new
            {
                lastmsg = "<p>Best regards,</p><p><em>Admin</em></p>", // this.template.Replace("STUDENTNAME", fullname),
                firstmsg = "<h2>Congratulation Letter </h2><p><br></p><p>I am thrilled to inform you that you have successfully completed all courses and modules required for your program. Your dedication and hard work have paid off, and it's time to celebrate your achievement! You are now eligible to graduate. Attached, please find detailed tables outlining your scores and performance throughout the program. Your commitment to excellence has been evident, and I have no doubt that you will continue to excel in your future endeavors.</p><p>Once again, congratulations on this significant accomplishment. Please let me know if you have any questions or need further assistance.</p><p><br></p><p>The following tables will summarize all of your scores</p>",
                subject = "Congratulations on Your Completion!",
                title = "FAMS - Congratulation on your completion",
                body = new
                {
                    html = data.html,
                    css = data.css,
                    quiz3 = data.quiz3,
                    quiz4 = data.quiz4,
                    quiz5 = data.quiz5,
                    quiz6 = data.quiz6,
                    quiz_ave = Double.Parse(((data.html + data.css + data.quiz3 + data.quiz4 + data.quiz5 + data.quiz6) / 6).ToString("0.00")),
                    practice1 = data.practice1,
                    practice2 = data.practice2,
                    practice3 = data.practice3,
                    asm_ave = Double.Parse(((data.practice3 + data.practice2 + data.practice1) / 3).ToString("0.00")),
                    quizfinal = data.final,
                    audit = student.Audit,
                    practicefinal = data.final,
                    status = true,
                    mock = student.Mock,
                    gpa = student.Gpa
                },
                recipient = email,
                options = new
                {
                    isaudit = true,
                    isgpa = true,
                    isfinalstatus = true
                }
            });

            // Turn it into the JSON
            var content = new StringContent(data_, System.Text.Encoding.UTF8, "application/json");

            // Send the response
            var resp = client.PostAsync($"{_store.emailhost}/api/Send/email", content).GetAwaiter().GetResult();

            Console.WriteLine("/* Finished sending email");
        }

        public Student FilterStudentById(string StudentId)
        {
            return _context.Students.SingleOrDefault(s => s.StudentId.Equals(StudentId));
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Students.ToList();
        }
    }
}
