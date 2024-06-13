namespace SyllabusManagementAPI.Entities.Exceptions.NotFoundException
{
    public sealed class SyllabusNotFoundException : NotFoundException
    {
        public SyllabusNotFoundException(string syllabusId) : base($"Syllabus with ID: '{syllabusId}' not found.") { }
    }
}