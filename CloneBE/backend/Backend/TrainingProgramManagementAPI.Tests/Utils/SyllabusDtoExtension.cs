using System.CodeDom;
using TrainingProgramManagementAPI.DTOs;

namespace TrainingProgramManagementAPITests.Utils;

public static class SyllabusDtoExtension
{
    public static void AssertObjEquals(this SyllabusDto dto, object? obj)
    {
        // Check obj exist
        Assert.NotNull(obj);

        // Compare typeof obj 
        Assert.Equal(obj.GetType(), dto.GetType());

        // Convert to Dto instance
        var other = (SyllabusDto)obj;
        
        // Compare data
        var result = 
            // dto.TrainingProgramCode == other.TrainingProgramCode &&
            dto.SyllabusId == other.SyllabusId &&
            dto.Days == other.Days &&
            dto.CreatedBy == other.CreatedBy &&
            dto.CreatedDate == other.CreatedDate &&
            dto.ModifiedDate == other.ModifiedDate &&
            dto.ModifiedBy == other.ModifiedBy && 
            dto.TopicCode == other.TopicCode &&
            dto.TopicName == other.TopicName && 
            dto.AttendeeNumber == other.AttendeeNumber && 
            dto.Hours == other.Hours && 
            dto.Level == other.Level && 
            dto.Version == other.Version && 
            dto.TechnicalRequirement == other.TechnicalRequirement;

        // Is Equal
        Assert.True(result);
    }

}