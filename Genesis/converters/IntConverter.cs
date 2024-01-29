using System.Text.Json;
using System.Text.Json.Serialization;

namespace Genesis.converters
{
    public class IntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && options.NumberHandling == JsonNumberHandling.AllowReadingFromString)
            {
                return int.TryParse(reader.GetString(), out int result) ? result : -1;
            }

            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
