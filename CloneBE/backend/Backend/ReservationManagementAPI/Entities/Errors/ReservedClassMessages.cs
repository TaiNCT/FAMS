using ReservationManagementAPI.Exceptions;

namespace ReservationManagementAPI.Entities.Errors
{
    public class ReservedClassMessages
    {
        public static readonly Message ReservedClassIdIsNull = new("STU-400", "ReservedClass's Id is null");
    }
}
