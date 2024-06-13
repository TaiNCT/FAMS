namespace ReservationManagementAPI.Entities.DTOs
{
    public class ReClassDialogDTO
    {
        public string StudentId { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public string ClassStartDate { get; set; }
        public string ClassEndDate { get; set; }
        public string ModuleId{ get; set; }
        public string ModuleName { get; set; }
        public string ReservedStartDate { get; set; }
        public string ReservedEndDate { get; set; }
        public string Reason { get; set; }
    }
}
