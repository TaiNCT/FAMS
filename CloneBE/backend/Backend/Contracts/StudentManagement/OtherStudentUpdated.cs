using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StudentManagement
{
    public class OtherStudentUpdated
    {
        [Required]
        public string classid { get; set; }

        [Required]
        public string studentid { get; set; }
        [Required]
        public string university { get; set; }

        [Required]
        public decimal gpa { get; set; }

        [Required]
        public string major { get; set; }

        [Required]
        public string recer { get; set; }

        [Required]
        public DateTime gradtime { get; set; }
    }
}
