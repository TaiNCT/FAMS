using Entities.Context;
using Entities.Models;
using ExcelDataReader;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Repository;
using System.Data;
using System.Text;

namespace ScoreManagementAPI.DAO
{
    public class ImportExcelScoreDAO

    {
        private readonly FamsContext _context;
        public IStudentRepository _studentrepo;
        public ImportExcelScoreDAO(FamsContext context)
        {
            _context = context;
        }

        public class IRecord
        {

            public Student? student { get; set; } = null;
            public string username { get; set; }
            public string FAAccount { get; set; }

            // Score section
            public double html { get; set; }
            public double css { get; set; }
            public double quiz3 { get; set; }
            public double quiz4 { get; set; }
            public double quiz5 { get; set; }
            public double quiz6 { get; set; }
            public double average_1 { get; set; }

            // Assignments / Practices section
            public double practice1 { get; set; }
            public double practice2 { get; set; }
            public double practice3 { get; set; }
            public double average_2 { get; set; }

            // Third section in light green
            public double quiz_final { get; set; }
            public double audit { get; set; }
            public double practice_final { get; set; }
            public double final_module { get; set; }
            public double gpa { get; set; }
            public double level { get; set; }
            public bool status { get; set; }

            // Last section
            public double mock { get; set; }
        }

        public class ISCoreResult
        {
            public List<IRecord> duplicates = new List<IRecord>();
            public List<IRecord> non_duplicates = new List<IRecord>();
        }
        public ISCoreResult DuplicateAmount(List<IRecord> scores, string moduleid)
        {
            // Check for duplicated amount based on the given "scores" object
            ISCoreResult ret = new ISCoreResult();
            foreach (IRecord record in scores)
            {
                if (_context.StudentModules.Any(o => o.StudentId.Equals(record.student.StudentId)))
                    ret.duplicates.Add(record);
                else
                    ret.non_duplicates.Add(record);
            }
            return ret;
        }
        public string GetModuleID(ImportExcelScoreRequest request, Stream stream)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
            DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                UseColumnDataType = false,
                ConfigureDataTable = (DataTableConfig) => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = false
                }
            });

            string moduleName = dataset.Tables[0].Rows[1].ItemArray[3].ToString().Trim();

            // Check if moduleName is null or empty
            if (string.IsNullOrEmpty(moduleName))
            {
                throw new Exception("Enter your module name");
            }

            // Check if the module exists in the database
            var moduleExists = _context.Modules.Any(s => s.ModuleName == moduleName);
            if (!moduleExists)
            {
                throw new Exception ("Module name none exists in the database");
            }

            // If the module exists, fetch the ModuleId
            var moduleId = _context.Modules.Where(s => s.ModuleName == moduleName).Select(s => s.ModuleId).FirstOrDefault();
            return moduleId.ToString();
        }

        // This is the route
        public ImportExcelScoreResponse AddExcelWithListScore(ImportExcelScoreRequest request, Stream stream, string option)
        {
            // Extract data from Excel file
            List<IRecord> ScoresFromExcel = GetListFromExcel(request, stream);
            string op = option.Trim();
            if (ScoresFromExcel.Count == 0)
                throw new Exception("Empty data");

            // grab Module ID

            string modid = GetModuleID(request, stream);


            // Check for duplicated records
            ISCoreResult ret = DuplicateAmount(ScoresFromExcel, modid);

                if (ret.duplicates.Count > 0 && op.Length == 0)
                    throw new Exception("DUP");
             

            // Checking options then perform operations such as SKIP, ALLOW, REPLACE
            switch (op)
            {
                case "SKIP":
                    // Allow non-duplicates, ignore duplicates
                    AddOptions(ret.non_duplicates, modid);
                    break;
                case "REPLACE":
                    // Replace duplicated with existing records
                    AddOptions(ret.non_duplicates, modid);
                    ReplaceOptions(ret.duplicates, modid);
                    break;
                default:
                    AddOptions(ret.non_duplicates, modid);
                    AddOptions(ret.duplicates, modid);
                    break;
            }
            return new ImportExcelScoreResponse
            {
                IsSuccess = true,
                Message = "Successfully"
            };
        }


        public void ReplaceOptions(List<IRecord> records, string modid)
        {

            foreach (IRecord record in records)
            {
                // Step 1 :  Update StudentModule
                StudentModule stdmod = _context.StudentModules.SingleOrDefault(s => s.StudentId.Equals(record.student.StudentId) && s.ModuleId.Equals(modid));
                stdmod.ModuleLevel = (int)record.level;
                stdmod.ModuleScore = (int)record.final_module;


                // replace QuizStudent stuffs
                QuizStudent html = _context.QuizStudents.Where(s => s.QuizId.Equals(_studentrepo.html) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                QuizStudent css = _context.QuizStudents.Where(s => s.QuizId.Equals(_studentrepo.css) && s.StudentId     .Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                QuizStudent quiz3 = _context.QuizStudents.Where(s => s.QuizId.Equals(_studentrepo.quiz3) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                QuizStudent quiz4 = _context.QuizStudents.Where(s => s.QuizId.Equals(_studentrepo.quiz4) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                QuizStudent quiz5 = _context.QuizStudents.Where(s => s.QuizId.Equals(_studentrepo.quiz5) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                QuizStudent quiz6 = _context.QuizStudents.Where(s => s.QuizId.Equals(_studentrepo.quiz6) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                QuizStudent quizfinal = _context.QuizStudents.Where(s => s.QuizId.Equals(_studentrepo.quizfinal) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                html.Score = (decimal)record.html;
                css.Score = (decimal)record.css;
                quiz3.Score =  (decimal)record.quiz3;
                quiz4.Score =  (decimal)record.quiz4;
                quiz5.Score =  (decimal)record.quiz5;
                quiz6.Score = (decimal)record.quiz6;
                quizfinal.Score = (decimal)record.quiz_final;

                // Replace Score data
                Score practice1 = _context.Scores.Where(s => s.AssignmentId.Equals(_studentrepo.asm1) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                Score practice2 = _context.Scores.Where(s => s.AssignmentId.Equals(_studentrepo.asm2) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                Score practice3 = _context.Scores.Where(s => s.AssignmentId.Equals(_studentrepo.asm3) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                Score practicef = _context.Scores.Where(s => s.AssignmentId.Equals(_studentrepo.asmf) && s.StudentId.Equals(record.student.StudentId)).OrderByDescending(s => s.Id).FirstOrDefault();
                practice1.Score1 = (decimal)record.practice1;
                practice2.Score1 = (decimal)record.practice2;
                practice3.Score1 = (decimal)record.practice3;
                practicef.Score1 = (decimal)record.practice_final;

                _context.SaveChanges();
            }

        }

        public void AddOptions(List<IRecord> records, string modid)
        {

            foreach (IRecord record in records)
            {
                // Step 1: Add to StudentModule table if not existed yet
                if (!_context.StudentModules.Any(o => o.StudentId.Equals(record.student.StudentId) && o.ModuleId.Equals(modid)))
                {
                    _context.StudentModules.Add(new StudentModule
                    {
                        StudentId = record.student.StudentId,
                        ModuleId = modid,
                        ModuleScore= (decimal)record.final_module,
                        ModuleLevel = (int)record.level
                    });
                }

                // Add new to the QuizStudent table
                _context.QuizStudents.AddRange(new List<QuizStudent>
                {
                    new QuizStudent{ QuizId = _studentrepo.html, StudentId= record.student.StudentId,  Score=(decimal)record.html, SubmissionDate=DateTime.Now },
                    new QuizStudent{ QuizId = _studentrepo.css, StudentId= record.student.StudentId,   Score=(decimal)record.css, SubmissionDate=DateTime.Now },
                    new QuizStudent{ QuizId = _studentrepo.quiz3, StudentId= record.student.StudentId, Score=(decimal)record.quiz3, SubmissionDate=DateTime.Now },
                    new QuizStudent{ QuizId = _studentrepo.quiz4, StudentId= record.student.StudentId, Score=(decimal)record.quiz4, SubmissionDate=DateTime.Now },
                    new QuizStudent{ QuizId = _studentrepo.quiz5, StudentId= record.student.StudentId, Score=(decimal)record.quiz5, SubmissionDate=DateTime.Now },
                    new QuizStudent{ QuizId = _studentrepo.quiz6, StudentId= record.student.StudentId, Score=(decimal)record.quiz6, SubmissionDate=DateTime.Now },
                    new QuizStudent{ QuizId = _studentrepo.quizfinal, StudentId= record.student.StudentId, Score=(decimal)record.quiz_final, SubmissionDate=DateTime.Now },
                });

                // Add to Assignment table
                _context.Scores.AddRange(new List<Score>
                {
                    new Score{AssignmentId=_studentrepo.asm1, StudentId=record.student.StudentId, Score1=(decimal)record.practice1, SubmissionDate=DateTime.Now },
                    new Score{AssignmentId=_studentrepo.asm2, StudentId=record.student.StudentId, Score1=(decimal)record.practice2, SubmissionDate=DateTime.Now },
                    new Score{AssignmentId=_studentrepo.asm3, StudentId=record.student.StudentId, Score1=(decimal)record.practice3, SubmissionDate=DateTime.Now },
                    new Score{AssignmentId=_studentrepo.asmf, StudentId=record.student.StudentId, Score1=(decimal)record.practice_final, SubmissionDate=DateTime.Now },
                });

            }
            _context.SaveChanges();

        }

        public List<IRecord> GetListFromExcel(ImportExcelScoreRequest request, Stream stream)
        {
            HashSet<string> uniqueAccounts = new HashSet<string>();
            // List of records which will be returned later
            List<IRecord> records = new List<IRecord>();

            // Starting to extract data from the request
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
            DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                UseColumnDataType = false,
                ConfigureDataTable = (DataTableConfig) => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = false
                }
            });
            DataTable dataTable = dataset.Tables[0];
            if (dataTable.Columns.Count != 26)
            {
                throw new ArgumentException("The file format is incorrect");
            }

            // Looping through each rows and extract data then put into a IRecord object
            for (int i = 5; i < dataTable.Rows.Count; i++)
            {
              
                // Data of a row will be stored in here
                var row = dataTable.Rows[i].ItemArray;
                // Extract student
                Student student = _context.Students.SingleOrDefault(s => s.Faaccount.Equals(row[1].ToString()));
                if (!uniqueAccounts.Add(row[1].ToString()))throw new Exception($"Faaccount {row[1]} is match with other in excel file");
                if (student==null) throw new Exception($"Faaccount {row[1]} is not exit in database.");
                if (student.Status.Equals("Active")) throw new Exception($"Faaccount {row[1]} is active");
                if (!student.Status.Equals("Active"))
                    

                // Extract from index 3 to index 21
                try
                {
                    records.Add(new IRecord
                    {

                        student = student,

                        username = row[0].ToString(),
                        FAAccount = row[1].ToString(),

                        html = Double.Parse(row[3].ToString()),
                        css = Double.Parse(row[4].ToString()),
                        quiz3 = Double.Parse(row[5].ToString()),
                        quiz4 = Double.Parse(row[6].ToString()),
                        quiz5 = Double.Parse(row[7].ToString()),
                        quiz6 = Double.Parse(row[8].ToString()),
                        average_1 = Double.Parse(row[9].ToString()),

                        practice1 = Double.Parse(row[10].ToString()),
                        practice2 = Double.Parse(row[11].ToString()),
                        practice3 = Double.Parse(row[12].ToString()),
                        average_2 = Double.Parse(row[13].ToString()),

                        quiz_final = Double.Parse(row[14].ToString()),
                        audit = Double.Parse(row[15].ToString()),
                        final_module = Double.Parse(row[17].ToString()),
                        practice_final = Double.Parse(row[16].ToString()),
                        gpa = Double.Parse(row[18].ToString()),
                        level = Double.Parse(row[19].ToString()),
                        status = row[20].ToString().Equals("Passed") ? true : false,

                        mock = Double.Parse(row[21].ToString()),
                    });
                }catch(Exception ex)
                {
                    throw new Exception($"Error at row {i}: {ex.Message}");
                }
            }
     

            return records;
        }


       

    }
}
