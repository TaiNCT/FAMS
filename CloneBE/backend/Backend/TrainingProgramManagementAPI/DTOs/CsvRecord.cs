using CsvHelper.Configuration.Attributes;

namespace TrainingProgramManagementAPI.DTOs
{
    public class CsvRecord
    {
        [Name("Id")]
        public string Id { get; set; } = string.Empty;

        [Name("Name")]
        public string Name { get; set; } = string.Empty;

        [Name("Information")]
        public string Information { get; set; } = string.Empty;

        [Name("List Syllabus")]
        public string ListSyllabus { get; set; } = string.Empty;
    }
}
