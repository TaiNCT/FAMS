using Entities.Models;

namespace ClassManagementAPI.Dto.ClassDTO
{
    public class UpdatedClassDto
    {
        public string ClassId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int AcceptedAttendee { get; set; }
        public int ActualAttendee { get; set; }
        public int PlannedAttendee { get; set; }
            
    }   

}


