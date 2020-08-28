using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EZSubmitApp.Core.JsonConverters
{
    /// <summary>
    /// Allow a conversion of empty string to a null DateTime?.
    /// </summary>
    public class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return default;
            }

            string value = reader.GetString();
            if (value == string.Empty)
            {
                return default;
            }

            //return DateTime.ParseExact(value, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            var test = DateTime.Parse(value);
            var test2 = DateTime.TryParseExact(value, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);
            var parsedDateTime = DateTime.ParseExact(value, "yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"));
            return parsedDateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.Value.ToString("yyyy/MM/dd HH:mm:ss"));
            }
        }
    }
}
