namespace ReservationManagementAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IStudentRepository Student { get; }
        IClassRepository Class { get; }

        IModuleRepository Module { get; }
        void Save();
    }
}
