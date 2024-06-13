namespace TrainingProgramManagementAPITests.Utils;

public static class SortingValidHelper
{
    public static bool IsAscending<T>(List<T> list) where T : IComparable<T>
    {
        if (list == null || list.Count <= 1) return true;

        for (int i = 1; i < list.Count; ++i)
        {
            if (list[i].CompareTo(list[i - 1]) < 0)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsDescending<T>(List<T> list) where T : IComparable<T>
    {
        if (list == null || list.Count <= 1) return true;

        for (int i = 1; i < list.Count; ++i)
        {
            if (list[i].CompareTo(list[i - 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }
}