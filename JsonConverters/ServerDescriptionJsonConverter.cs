using MinecraftServerInfo.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinecraftServerInfo.JsonConverters
{
    /// <summary>
    /// 服务器描述构建器 (在早期版本中,的Description字段是直接返回的string类型,但在更高版本(例如1.19)支持了描述字段使用Json文本,所以需要使用此类来构建下)
    /// </summary>
    public class ServerDescriptionJsonConverter: JsonConverter<ServerDescription>
    {
        public override ServerDescription? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return new ServerDescription()
                    {
                        Text = reader.GetString() ?? string.Empty
                    };
                default:
                    return JsonDocument.ParseValue(ref reader).RootElement.Clone().Deserialize<ServerDescription>();
            }
        }
        public override void Write(Utf8JsonWriter writer, ServerDescription objectToWrite, JsonSerializerOptions options) => JsonSerializer.Serialize(writer, objectToWrite, objectToWrite.GetType(), options);
    }
}
