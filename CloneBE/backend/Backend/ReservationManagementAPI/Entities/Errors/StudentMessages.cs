using ReservationManagementAPI.Exceptions;

namespace ReservationManagementAPI.Entities.Errors
{
    public class StudentMessages
    {
        public static readonly Message StudentNotFound = new("STU-404", "There is no student with this id");
        public static readonly Message StudentAlreadyReserved = new("STU-400", "Student already reserved");
        public static readonly Message StudentNotReserved = new("STU-400", "Status must be reserve");
        public static readonly Message StudentNotUpdated = new("STU-400", "Student not updated");
        public static readonly Message StudentIdIsNull = new("STU-400", "Student's Id is null");
        public static readonly Message StudentUpdateSuccessful = new("STU-400", "Update successfully");
    }
}
