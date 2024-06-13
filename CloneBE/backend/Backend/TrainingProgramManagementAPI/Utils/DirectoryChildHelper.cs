namespace TrainingProgramManagementAPI.Utils;

public static class DirectoryChildHelper
{
    public static string[] SplitSubDirectory(string directoryPath)
    {
        return directoryPath.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
    }
}