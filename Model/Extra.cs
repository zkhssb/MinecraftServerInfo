using System.Text.Json.Serialization;

namespace MinecraftServerInfo.Model
{
    /// <summary>
    /// MinecraftJson文本字段 (和 title tellraw 中表示的字段几乎一致) 该字段似乎只会出现在高版本的Minecraft
    /// </summary>
    public class Extra
    {
        /// <summary>
        /// 是否粗体
        /// </summary>
        [JsonPropertyName("bold")]
        public bool Bold { get; set; } = false;
        /// <summary>
        /// 是否斜体
        /// </summary>
        [JsonPropertyName("italic")]
        public bool Italic { get; set; } = false;
        /// <summary>
        /// 是否有下划线 (类似超链接下面的线条
        /// </summary>
        [JsonPropertyName("underlined")]

        public bool Underlined { get; set; } = false;
        /// <summary>
        /// 是否有删除线
        /// </summary>
        [JsonPropertyName("strikethrough")]
        public bool Strikethrough { get; set; } = false;
        /// <summary>
        /// 是否乱码
        /// </summary>
        [JsonPropertyName("obfuscated")]
        public bool Obfuscated { get; set; } = false;
        /// <summary>
        /// 当前字体颜色
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; } = "white";
        /// <summary>
        /// 文本内容
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}
