using Entities.Models;

namespace ReservationManagementAPI.Entities.DTOs
{
    public class ReservedClassDTO
    {

        public string StudentId { get; set; }

        public string ClassId { get; set; }

        public string Reason { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } 
    }
}
