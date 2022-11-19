using System.Text.Json.Serialization;

namespace MinecraftServerInfo.Model
{
    public class ServerGameVersion
    {
        /// <summary>
        /// 游戏版本名称 (如:1.19.2 ,22w19a)
        /// </summary>
        [JsonPropertyName("name")]
        public string VersionName { get; set; } = string.Empty;
        /// <summary>
        /// 协议ID
        /// </summary>
        [JsonPropertyName("protocol")]
        public int ProtocolId { get; set; } = 0;
    }
}
