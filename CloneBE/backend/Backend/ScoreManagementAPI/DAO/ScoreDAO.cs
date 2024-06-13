using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.DTO;

namespace ScoreManagementAPI.DAO
{
    public class ScoreDAO
    {
        public static ScoreDTO GetScoreDTOs(Student student)
        {/*
            , IEnumerable<QuizStudent> quizStudents, IEnumerable< Score > scores, IEnumerable<StudentModule>  studentModules*/

            ScoreDTO ret = new ScoreDTO();


            FamsContext db = new FamsContext();

            // Generate data and put in the "ret" object



            return ret;


            /*  List<QuizStudentDTO> quizStudentDTOs = new List<QuizStudentDTO>();
              // Generate data and put in the "ret" object



              return ret;


            /*  List<QuizStudentDTO> quizStudentDTOs = new List<QuizStudentDTO>();
              List<AssignmentStudentDTO> assignmentStudentDTOs = new List<AssignmentStudentDTO>();
              List<ModuleStudentDTO> moduleStudentDTOs = new List<ModuleStudentDTO>();


              // convert
              foreach (var quizStudent in quizStudents)
              {

              {

                  quizStudentDTOs.Add(Convert_QuizStudents_To_QuizStudentsDTO(quizStudent));
              }
              foreach (var score in scores)
              {
                  assignmentStudentDTOs.Add(Convert_Score_To_AssignmentStudentsDTO(score));
              }
              foreach (var studentModule in studentModules)
              {
                  moduleStudentDTOs.Add(Convert_StudentModule_To_ModuleStudentDTO(studentModule));
              }*/


            /*       return SetScoreDTOs(student, quizStudentDTOs, assignmentStudentDTOs, moduleStudentDTOs);*/
            /*       return SetScoreDTOs(student, quizStudentDTOs, assignmentStudentDTOs, moduleStudentDTOs);*/
        }

        private static QuizStudentDTO Convert_QuizStudents_To_QuizStudentsDTO(QuizStudent quizStudent)
        {
            QuizStudentDTO quizStudentDTO = new QuizStudentDTO();
            quizStudentDTO.QuizStudentId = quizStudent.QuizStudentId;
            quizStudentDTO.QuizName = new FamsContext().Quizzes.Where(x => x.QuizId == quizStudent.QuizId).FirstOrDefault().QuizName;
            quizStudentDTO.QuizName = new FamsContext().Quizzes.Where(x => x.QuizId == quizStudent.QuizId).FirstOrDefault().QuizName;
            quizStudentDTO.StudentId = quizStudent.StudentId;
            quizStudentDTO.QuizId = quizStudent.QuizId;
            quizStudentDTO.SubmissionDate = quizStudent.SubmissionDate;
            quizStudentDTO.Score = (double)quizStudent.Score;
            quizStudentDTO.Id = quizStudent.Id;

            return quizStudentDTO;
        }
        private static AssignmentStudentDTO Convert_Score_To_AssignmentStudentsDTO(Score score)
        {
            AssignmentStudentDTO assignmentStudentDTO = new AssignmentStudentDTO();
            assignmentStudentDTO.AssignmentStudentId = score.ScoreId;
            assignmentStudentDTO.AssignmentName = new FamsContext().Assignments.Where(x => x.AssignmentId == score.AssignmentId).FirstOrDefault().AssignmentName;

            assignmentStudentDTO.AssignmentName = new FamsContext().Assignments.Where(x => x.AssignmentId == score.AssignmentId).FirstOrDefault().AssignmentName;

            assignmentStudentDTO.StudentId = score.StudentId;
            assignmentStudentDTO.AssignmentId = score.AssignmentId;
            assignmentStudentDTO.Score = (double)score.Score1;
            assignmentStudentDTO.Id = score.Id;
            assignmentStudentDTO.SubmissionDate = score.SubmissionDate;

            return assignmentStudentDTO;
        }
        private static ModuleStudentDTO Convert_StudentModule_To_ModuleStudentDTO(StudentModule studentModule)
        {
            ModuleStudentDTO moduleStudentDTO = new ModuleStudentDTO();
            moduleStudentDTO.ModuleStudentId = studentModule.StudentModuleId;
            moduleStudentDTO.ModuleName = new FamsContext().Modules.Where(x => x.ModuleId == studentModule.ModuleId).FirstOrDefault().ModuleName;

            moduleStudentDTO.ModuleName = new FamsContext().Modules.Where(x => x.ModuleId == studentModule.ModuleId).FirstOrDefault().ModuleName;

            moduleStudentDTO.StudentId = studentModule.StudentId;
            moduleStudentDTO.ModuleId = studentModule.ModuleId;
            moduleStudentDTO.ModuleScore = (double)studentModule.ModuleScore;
            moduleStudentDTO.ModuleLevel = studentModule.ModuleLevel;
            moduleStudentDTO.Id = studentModule.Id;

            return moduleStudentDTO;
        }
        private static ScoreDTO SetScoreDTOs(Student student, List<QuizStudentDTO> quizStudentDTO
            , List<AssignmentStudentDTO> assignmentStudentDTOs, List<ModuleStudentDTO> moduleStudentDTOs)
        {
            ScoreDTO scoreDTO = new ScoreDTO();

            scoreDTO.FullName = student.FullName;
            scoreDTO.Faaccount = student.Faaccount;
            /*            scoreDTO.QuizStudent = quizStudentDTO;
                        scoreDTO.AssignmentStudent = assignmentStudentDTOs;*/
            scoreDTO.audit = student.Audit;
            scoreDTO.mock = student.Mock;
            /*            scoreDTO.gpa = student.Gpa;
                        scoreDTO.ModuleStudents = moduleStudentDTOs;*/
            /*            scoreDTO.QuizStudent = quizStudentDTO;
                        scoreDTO.AssignmentStudent = assignmentStudentDTOs;*/
            scoreDTO.audit = student.Audit;
            scoreDTO.mock = student.Mock;
            /*            scoreDTO.gpa = student.Gpa;
                        scoreDTO.ModuleStudents = moduleStudentDTOs;*/
            return scoreDTO;
        }
        public static IActionResult UpdateScoreDTO(ScoreUpdateDTO scoreUpdateDTO)
        {
            using (var context = new FamsContext())
            {
                List<QuizStudent> quizStudents = context.QuizStudents.Where(q => q.StudentId == scoreUpdateDTO.StudentId).ToList();
                List<Score> scoreAssignmentStudents = context.Scores.Where(q => q.StudentId == scoreUpdateDTO.StudentId).ToList();
                if (quizStudents != null)
                {
                    // Iterating over each quiz student
                    foreach (var quizStudent in quizStudents)
                    {
                        // DTO
                        var matchingQuiz = scoreUpdateDTO.QuizStudentList
                                                        .FirstOrDefault(q => q.QuizStudentId == quizStudent.QuizStudentId);


                        // update score
                        if (matchingQuiz != null)
                        {
                            quizStudent.Score = (decimal)matchingQuiz.Score;

                        }
                    }
                }

                if (scoreAssignmentStudents != null)
                {
                    // Iterating over each quiz student
                    foreach (var scoreAssStudent in scoreAssignmentStudents)
                    {
                        // DTO
                        var matchingQuiz = scoreUpdateDTO.AssignmentStudentList
                                                        .FirstOrDefault(q => q.ScoreId == scoreAssStudent.ScoreId);


                        // update score
                        if (matchingQuiz != null)
                        {
                            scoreAssStudent.Score1 = (decimal)matchingQuiz.Score1;

                        }
                    }
                }
                context.SaveChanges();

            }


            return new OkObjectResult("Updated!");
        }

    }
}
