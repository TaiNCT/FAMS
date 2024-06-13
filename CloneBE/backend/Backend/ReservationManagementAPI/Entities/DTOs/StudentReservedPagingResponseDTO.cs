namespace ReservationManagementAPI.Entities.DTOs
{
    public class StudentReservedPagingResponseDTO
    {
        public List<StudentReservedDTO> StudentReservedList { get; set; } = new List<StudentReservedDTO>();
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int ItemCount { get; set; }

    }
}
