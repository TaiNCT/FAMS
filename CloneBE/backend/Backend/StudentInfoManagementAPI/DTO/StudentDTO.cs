namespace StudentInfoManagementAPI.DTO
{
    public class StudentDTO
    {
        public StudentInfoDTO StudentInfoDTO { get; set; }
        public MajorDTO? MajorDTO { get; set; }
        public IEnumerable<StudentClassDTO>? StudentClassDTOs { get; set; }

    }
}
