namespace SyllabusManagementAPI.Entities.Exceptions.NotFoundException
{
    public sealed class AssessmentSchemeNotFoundException : NotFoundException
    {
        public AssessmentSchemeNotFoundException(string syllabusId) : base($"Assessment Scheme with syllabus id: '{syllabusId}' not found.") { }
    }
}