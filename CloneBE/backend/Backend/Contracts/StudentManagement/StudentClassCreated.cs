using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StudentManagement
{
    public class StudentClassCreated
    {
        public string? StudentClassId { get; set; } = null!;

        public int? Id { get; set; }

        public string StudentId { get; set; } = null!;

        public string ClassId { get; set; } = null!;

        public string AttendingStatus { get; set; }

        public int Result { get; set; }

        public decimal FinalScore { get; set; }

        public int Gpalevel { get; set; }

        public int CertificationStatus { get; set; }

        public DateTime? CertificationDate { get; set; }

        public int Method { get; set; }
    }
}
