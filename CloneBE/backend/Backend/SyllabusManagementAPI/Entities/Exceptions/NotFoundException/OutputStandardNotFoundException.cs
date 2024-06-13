namespace SyllabusManagementAPI.Entities.Exceptions.NotFoundException
{
    public sealed class OutputStandardNotFoundException : NotFoundException
    {
        public OutputStandardNotFoundException(string outputStandardId) : base($"Output Standard with ID: '{outputStandardId}' not found.") { }
    }
}