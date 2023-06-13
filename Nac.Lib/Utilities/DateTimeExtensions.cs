namespace Nac.Lib.Utilities;

public static class DateTimeExtensions
{
    /// <summary>
    /// for whatever reason, format "u" does not use ISO "T", and format "o" show too many milli seconds
    /// so do a replace hack here :-(
    /// </summary>
    public static string ToIsoUtcNoSubSec(this DateTime dateTime)
        => dateTime.ToString("u").Replace(' ', 'T');

    /// <summary>
    /// for whatever reason, format "u" does not use ISO "T", and format "o" show too many milli seconds
    /// luckily we want to have the space here :-)
    /// </summary>
    public static string ToUtcBreakableNoSubSec(this DateTime dateTime)
        => dateTime.ToString("u");

    /// <summary>
    /// round-trip format with subseconds, remove ':' to avoid issues with file system
    /// </summary>
    public static string ToIsoUtcFileSystem(this DateTime dateTime)
        => dateTime.ToString("o").Replace(":", null);

    /// <summary>
    /// ISO 8601 without spacer, subseconds and zone
    /// </summary>
    public static string ToShortIsoIgnoreTimezone(this DateTime dateTime)
        => dateTime.ToString("yyyymmddThhmmss");

}
