using Microsoft.AspNetCore.Http;

namespace TrainingProgramManagementAPITests.Utils;

public static class FileHelper
{
    public static IFormFile ConvertToIFormFile(string filePath)
    {
        // Read the file content into a byte array
        byte[] fileBytes = File.ReadAllBytes(filePath);

        // Create an instance of FormFile using the byte array
        var fileStream = new MemoryStream(fileBytes);
        var formFile = new FormFile(fileStream, 0, fileBytes.Length, null!, Path.GetFileName(filePath))
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/octet-stream" // Set content type explicitly
        };

        return formFile;
    }
}