using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CheckerService.Converters
{
    internal class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var colorName = reader.GetString();
            if (colorName == null)
                throw new JsonException("Empty color");
            return Color.FromName(colorName);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}
