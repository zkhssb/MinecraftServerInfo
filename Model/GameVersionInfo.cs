using System.Text.Json.Serialization;

namespace MinecraftServerInfo.Model
{
    internal struct GameVersionInfo
    {
        [JsonPropertyName("name")]
        public string VersionName { get; set; }
        [JsonPropertyName("protocol_id")]
        public int ProtocolId { get; set; }
    }
}
