using System.CodeDom;
using TrainingProgramManagementAPI.DTOs;

namespace TrainingProgramManagementAPITests.Utils;

public static class TrainingProgramDtoExtension
{
    public static void AssertObjEquals(this TrainingProgramDto dto, object? obj)
    {
        // Check obj exist
        Assert.NotNull(obj);

        // Compare typeof obj 
        Assert.Equal(obj.GetType(), dto.GetType());

        // Convert to Dto instance
        var other = (TrainingProgramDto)obj;
        
        // Compare data
        var result = 
            // dto.TrainingProgramCode == other.TrainingProgramCode &&
            dto.Name == other.Name &&
            dto.StartTime == other.StartTime &&
            dto.CreatedBy == other.CreatedBy &&
            dto.CreatedDate == other.CreatedDate &&
            dto.UpdatedBy == other.UpdatedBy &&
            dto.UpdatedDate == other.UpdatedDate;

        // Is Equal
        Assert.True(result);
    }

}