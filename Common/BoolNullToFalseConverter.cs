using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClockNest.Common
{
    public class BoolNullToFalseConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return false; // default value
            }
            return reader.GetBoolean();
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }
}
