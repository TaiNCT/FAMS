using System.ComponentModel.DataAnnotations;
using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Payloads.Requests;

public class UploadMaterialRequest
{
    [Required]
    public Guid SyllabusId { get; set; } = Guid.Empty;

    [Required]
    public int DayNo { get; set; }

    [Required]
    public int UnitNo { get; set; }

    [Required]
    public int ChapterNo { get; set; }

    [Required]
    public IFormFile File { get; set; } = null!;

      [Required]
    public String CreatedBy { get; set; } = string.Empty;
}

public static class UploadMaterialRequestExtension
{
    public static TrainingMaterial ToTrainingMaterial(this UploadMaterialRequest reqObj, 
        string bucketName, string url, string unitChapterId)
    {
        // Get file extension 
        var fileExtension = Path.GetExtension(reqObj.File.FileName);

        Console.WriteLine("FILENAME: " + reqObj.File.FileName.Substring(0, fileExtension.Length - 1));

        return new TrainingMaterial
        {
            CreatedDate = DateTime.Now,
            FileName = reqObj.File.FileName,
            IsFile = true,
            IsDeleted = false,
            Name = reqObj.File.FileName.Substring(0, fileExtension.Length - 1),
            Url = bucketName + "/" + url + $"/{reqObj.File.FileName}",
            UnitChapterId = unitChapterId,
            CreatedBy = reqObj.CreatedBy
        };
    }
}