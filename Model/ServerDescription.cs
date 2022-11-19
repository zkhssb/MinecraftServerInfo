using System.Text;
using System.Text.Json.Serialization;

namespace MinecraftServerInfo.Model
{
    /// <summary>
    /// 服务器描述字段
    /// </summary>
    public class ServerDescription
    {
        /// <summary>
        /// 服务器描述字段中的富文本 (类似于指令 title tellraw中的Json文本) 目前只在1.19之后的版本见到过一次
        /// </summary>
        [JsonPropertyName("extra")]
        public List<Extra> Extras { get; set; } = new List<Extra>();
        /// <summary>
        /// 服务器普通描述字段
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// 使用此方法来获取Text或Extras中的文本 (有的高版本服务器貌似会采用extra的方式,因此不使用此方法获取文本时需要判断Extras的长度是否为0)
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            if(Extras.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var extra in Extras)
                {
                    sb.Append(extra.Text);
                }
                return sb.ToString();
            }
            else
            {
                return Text;
            }
        }
    }
}
