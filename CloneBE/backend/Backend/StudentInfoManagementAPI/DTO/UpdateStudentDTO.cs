using System.ComponentModel.DataAnnotations;

namespace StudentInfoManagementAPI.DTO
{
    public enum Gender
    {
        Male,
        Female
    }

    public class UpdateStudentDTO
    {
        // Required information about the status of the certificate request
        [Required]
        public string classid { get; set; }

        [Required]
        public string studentid { get; set; }

        // Dedicated to checking the JSON body syntax of /api/cert/update/<id>

        [Required]
        public string sid { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public string gender { get; set; }

        [Required]
        public DateTime dob { get; set; }

        [Required]
        public string status { get; set; } // true = Finished, false = In Class

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required]
        public string permanentResidence { get; set; }

        [Required]
        public string location { get; set; }

        [Required]
        public bool certificateStatus { get; set; }

        [Required]
        public DateTime certificateDate { get; set; }
    }
}
