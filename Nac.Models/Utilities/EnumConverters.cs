using System.Reflection;

namespace Nac.Models.Utilities;

public static class EnumConverters
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()?
                        .GetMember(enumValue.ToString())?
                        .First()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .Name
                        ?? enumValue.ToString();
    }

    public static string? GetJsonName(this Enum enumValue)
    {
        return enumValue.GetType()?
                        .GetMember(enumValue.ToString())?
                        .First()?
                        .GetCustomAttribute<JsonPropertyNameAttribute>()?
                        .Name
                        ?? enumValue.ToString();
    }

}
