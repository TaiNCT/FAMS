namespace ScoreManagementAPI.DTO
{
    public class StudentClassesDTO
    {
        public int id { get; set; }
        public string sid {  get; set; }
        public int cid {  get; set; }
        public string name {  get; set; }
        public DateTime dob { get; set; }
        public string email {  get; set; }
        public string phone {  get; set; }
        public DateTime certificateDate { get; set; }
        public int certificateStatus {  get; set; }
        public Dictionary<string, string> location {  get; set; }
        public bool status {  get; set; }
        public string gender {  get; set; }
    }
}
