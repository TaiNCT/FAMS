namespace ScoreManagementAPI.DTO
{
    public class ScoreFromExcelDTO
    {
        public string Name { get; set; }
        public string FAAccount { get; set; }
        public Dictionary<string, double> ListQuizScore = new Dictionary<string, double>();
        public Dictionary<string, double> ListAssignmentScore = new Dictionary<string, double>();
        public Dictionary<string, double> ListModuleScore = new Dictionary<string, double>();
        public double QuizFinal { get; set; }

        public double Mock  { get; set; }

        public int Audit { get; set; }

        public double PracticalFinal { get; set; }

        public int ModuleLevel {  get; set; }
        public string ModuleName { get; set; }
        public double ModuleScore {  get; set; }

    }
}
