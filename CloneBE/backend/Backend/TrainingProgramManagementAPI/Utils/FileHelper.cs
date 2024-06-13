using System.Text.RegularExpressions;

namespace TrainingProgramManagementAPI.Utils;

public class FileHelper
{
    public static string CombineWithRegex(
        string[] patterns, // Regex pattern  
        string replacePattern, // Replacement pattern
        params string[] roots) // Root paths
    {
        var pathCombined = ""; // Final combined path
        foreach (var subPath in roots) // Combine each subPath to final Path
        {
            if (string.IsNullOrEmpty(pathCombined)) // Check is root path
            {
                pathCombined = subPath;
            }
            else // Adding subPath
            {
                pathCombined = Path.Combine(pathCombined, subPath);
            }
        }

        // Combine multiple pattern into a single regular expression
        string combinedPattern = string.Join("|", patterns);

        // Replace pattern with new replacement using regular expression
        pathCombined = Regex.Replace(pathCombined, combinedPattern, replacePattern);

        return pathCombined;
    }

    public static string Combine(params string[] roots)
    {
        var pathCombined = ""; // Final combined path
        foreach (var subPath in roots) // Combine each subPath to final Path
        {
            if (string.IsNullOrEmpty(pathCombined)) // Check is root path
            {
                pathCombined = subPath;
            }
            else // Adding subPath
            {
                pathCombined = Path.Combine(pathCombined, subPath);
            }
        }

         // Replace pattern with new replacement using regular expression
        return Regex.Replace(pathCombined, @"\\", "/");
    }

    public static string GetFileNameWithoutExtension(string fileName)
    {
        var lastIndex = fileName.LastIndexOf(".");

        if (lastIndex == -1) return fileName;

        return fileName.Substring(0, lastIndex);
    }
}