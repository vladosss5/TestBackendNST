namespace App.Core.Extensions;

public static class ModelExtensions
{
    public static bool HasDuplicates<T>(this IEnumerable<T> sequence)
    {
        var set = new HashSet<T>();
        foreach (T item in sequence)
        {
            if (set.Contains(item))
                return true;
            set.Add(item);
        }
        return false;
    }
}