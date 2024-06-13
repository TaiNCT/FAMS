using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StudentManagement
{

    public enum Gender
    {
        Male,
        Female,
        Others
    }

    public enum Status
    {
        InClass,
        DropOut,
        Finish,
        Reserve
    }

    public class GeneralStudentUpdated
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
        [EnumDataType(typeof(Status))]
        public string status { get; set; }

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

        public bool certificateStatus { get; set; }

        public DateTime? certificateDate { get; set; }
    }
}
