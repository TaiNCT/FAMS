using System.ComponentModel.DataAnnotations;

namespace ScoreManagementAPI.DTO
{
    public class StudentCertDTO
    {
        public int id { get; set; }
        public string sid { get; set; }
        public int? cid { get; set; } = null;
        public string name { get; set; }
        public DateTime dob { get; set; }

        public string email { get; set; }
        public string phone { get; set; }
        public DateTime? certificateDate { get; set; } = null;
        public string address { get; set; }
        public string status { get; set; }
        public string gender { get; set; }
        public bool certificateStatus { get; set; }

        // Others section
        public string university { get; set; }
        public decimal gpa { get; set; }
        public string major { get; set; }
        public string recer { get; set; }
        public DateTime gradtime { get; set; }
    }
}
