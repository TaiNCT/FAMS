namespace TrainingProgramManagementAPI.Utils
{
    public class AppSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string DateTimeFormat { get; set;} = string.Empty;
    }
}