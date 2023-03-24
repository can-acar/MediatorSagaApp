using System.Text.Json;
using System.Text.Json.Serialization;

namespace MessageQueueService;



public class Message
{
    public string Channel { get; set; }
    public object Content { get; set; }
    
    public string TypeAssemblyQualifiedName { get; set; }

    [JsonConverter(typeof(JsonMessageConverter))]
    public class JsonMessageConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonDocument = JsonDocument.ParseValue(ref reader);
            var typeString = jsonDocument.RootElement.GetProperty("$type").GetString();
            var type = Type.GetType(typeString);
            var content = JsonSerializer.Deserialize(jsonDocument.RootElement.GetProperty("Content").GetRawText(), type, options);
            return content;
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("$type", value.GetType().AssemblyQualifiedName);
            writer.WritePropertyName("Content");
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
            writer.WriteEndObject();
        }
    }
}
