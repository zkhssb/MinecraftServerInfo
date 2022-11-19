using System.Text.Json.Serialization;

namespace MinecraftServerInfo.Model
{
    /// <summary>
    /// 玩家信息
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// 正版玩家UUID
        /// </summary>
        [JsonPropertyName("id")]
        public string Uuid { get; set; } = string.Empty;
        /// <summary>
        /// 玩家ID
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}