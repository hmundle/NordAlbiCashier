using System.Text.Json;

namespace Nac.Models.Utilities;

/// <summary>
/// JSON Converter for DateTime, considering always UTC, avoid any local times
/// copy of 
/// https://raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/Value/DateTimeConverter.cs
/// </summary>
public sealed class UtcDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dt = reader.GetDateTime();
        if (dt.Kind == DateTimeKind.Unspecified)
        {
            // do not accept unspecified timestamps, the would be handled implicitly as local time,
            // which may be the local time of the server and not of the client
            throw new JsonException($"Cannot convert DateTime with 'DateTimeKind.Unspecified', timestamp is: {dt:s}.");
        }
        return dt.ToUniversalTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime());
    }

    //internal override DateTime ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //{
    //    return reader.GetDateTimeNoValidation();
    //}

    //internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
    //{
    //    writer.WritePropertyName(value);
    //}
}

