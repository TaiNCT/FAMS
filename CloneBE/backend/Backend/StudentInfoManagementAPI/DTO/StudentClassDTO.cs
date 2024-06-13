namespace StudentInfoManagementAPI.DTO
{
    public class StudentClassDTO
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

        public ClassDTO? Class { get; set; } = null!;

        /*public StudentInfoDTO Student { get; set; } = null!;*/
    }
}
