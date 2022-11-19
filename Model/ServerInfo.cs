using System.Text.Json.Serialization;

namespace MinecraftServerInfo.Model
{
    public class ServerInfo
    {
        /// <summary>
        /// 服务器是否启用启用聊天预览功能 (仅在22w19a(1.19最后一个快照)之后的服务器中出现)
        /// </summary>
        [JsonPropertyName("previewsChat")]
        public bool PreviewsChat { get; set; } = false;
        /// <summary>
        /// 服务器是否强制执行安全聊天 (仅在22w19a(1.19最后一个快照)之后的服务器中出现)
        /// </summary>
        [JsonPropertyName("enforcesSecureChat")]
        public bool EnforcesSecureChat { get; set; } = false;
        /// <summary>
        /// 服务器描述
        /// </summary>
        [JsonPropertyName("description")]
        public ServerDescription Description { get; set; } = new ServerDescription();
        /// <summary>
        /// 服务器玩家信息
        /// </summary>
        [JsonPropertyName("players")]
        public ServerPlayers PlayerInfo { get; set; } = new ServerPlayers();
        /// <summary>
        /// 游戏版本信息
        /// </summary>
        [JsonPropertyName("version")]
        public ServerGameVersion Version { get; set; } = new ServerGameVersion();
        /// <summary>
        /// 服务器图标 (采用base64图片编码)
        /// </summary>
        [JsonPropertyName("favicon")]
        public string? Favicon { get; set; } = string.Empty;
    }
}
