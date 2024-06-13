namespace ReservationManagementAPI.Exceptions
{
    public sealed record Message(string Code, string Description)
    {
        public static readonly Message None = new(string.Empty, string.Empty);
    }
}
