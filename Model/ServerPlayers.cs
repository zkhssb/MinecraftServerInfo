using System.Text;
using System.Text.Json.Serialization;

namespace MinecraftServerInfo.Model
{
    /// <summary>
    /// 服务器玩家列表
    /// </summary>
    public class ServerPlayers
    {
        /// <summary>
        /// 最大玩家数
        /// </summary>
        [JsonPropertyName("max")]
        public int MaxPlayer { get; set; } = 0;
        /// <summary>
        /// 在线玩家数
        /// </summary>
        [JsonPropertyName("online")]
        public int Online { get; set; } = 0;
        /// <summary>
        /// 玩家列表
        /// </summary>
        [JsonPropertyName("sample")]
        public List<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"There are {Online} of a max of {MaxPlayer} players online:");
            foreach (var player in Players)
            {
                sb.Append(player.Name);
                if(player != Players.Last())
                    sb.Append(",");
            }
            return sb.ToString();
        }
    }
}
