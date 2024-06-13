using Elasticsearch.Net;
using System.ComponentModel.DataAnnotations;

namespace EmailInformAPI.DTO
{

    public class Score
    {

        // Quiz
        public double? html { get; set; }
        public double? css { get; set; }
        public double? quiz3 { get; set; }
        public double? quiz4 { get; set; }
        public double? quiz5 { get; set; }
        public double? quiz6 { get; set; }
        public double? quiz_ave { get; set; }

        // ASM
        public double? practice1 { get; set; }
        public double? practice2 { get; set; }
        public double? practice3 { get; set; }
        public double? asm_ave { get; set; }

        // Extra
        public double? quizfinal { get; set; }
        public double? audit { get; set; }
        public double? practicefinal { get; set; }
        public bool? status { get; set; }

        // Mock
        public double? mock { get; set; }
        public double? gpa { get; set; }
    }

    public class ExtraOption
    {
        public bool isaudit { get; set; }
        public bool isgpa { get; set; }
        public bool isfinalstatus { get; set; }
    }

    public class EmailDTO
    {

        public string? lastmsg { get; set; }
        [Required]
        public string firstmsg { get; set; }
        [Required]
        public string subject { get; set; }
        public Score? body { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string recipient { get; set; }

        // Options to pick
        public ExtraOption options {  get; set; }
    }
}
