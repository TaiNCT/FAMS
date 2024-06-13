namespace UserManagementAPI.Models.DTO
{
    public class UserCalendarDTO
    {
        public string ClassStatus { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string ClassName { get; set; } = null!;
        public string? FsuName { get; set; }
        public string? Address { get; set; }
    }
}
