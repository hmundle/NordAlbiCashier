namespace Nac.Lib.Utilities;

public static class StringExtensions
{
    public static string RemoveController(this string original)
        => original.Replace("Controller", "", StringComparison.OrdinalIgnoreCase);
    public static string RemoveNavigation(this string original)
        => original.Replace("Navigation", "", StringComparison.OrdinalIgnoreCase);
    public static string RemoveAsyncPostfix(this string original)
        => original.Replace("Async", "", StringComparison.OrdinalIgnoreCase);

    public static string? PrefixTruncate(this string? value, int maxLength)
    {
        return value?.Length > maxLength
            ? string.Concat("…", value.AsSpan(value.Length - maxLength))
            : value;
    }

    public static string? ArtificialBreak(this string? value, int maxTotalLength, int maxSegmentLength)
    {
        if (value?.Length > maxTotalLength)
        {
            List<string> segments = new();
            for (int i = 0; i < value.Length; i += maxSegmentLength)
            {
                segments.Add(value.Substring(i, Math.Min(maxSegmentLength, value.Length - i)));
            }
            return string.Join(" ", segments);
        }
        else
        {
            return value;
        }
    }

}
