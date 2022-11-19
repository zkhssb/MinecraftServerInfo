using MinecraftServerInfo.Exceptions;
using MinecraftServerInfo.Model;
using System.Text.Json;

namespace MinecraftServerInfo
{
    public class MinecraftProtocolVersion
    {
        internal readonly int _version = 0;
        private MinecraftProtocolVersion(int version)
        {
            _version = version;
        }
        /// <summary>
        /// 从MinecraftWiki官网映射协议版本,理论上支持所有MC版本,包括测试版(除非官方懒得更新这个ID表了(划掉) (每次调用都会发送一次Http请求)
        /// </summary>
        /// <param name="name">版本名称 如:1.19.2、22w45a</param>
        /// <returns></returns>
        public static MinecraftProtocolVersion FromGameVersion(string name)
        {
            Uri uri = new Uri("https://gitlab.bixilon.de/bixilon/minosoft/-/raw/master/src/main/resources/assets/minosoft/mapping/versions.json?inline=false");
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                JsonDocument document = JsonDocument.Parse(client.GetAsync(string.Empty).Result
                    .Content.ReadAsStringAsync().Result);

                /*
                 * 网页返回类型 { key:{} }
                 * 数组内容
                 "829": {
                    "name": "1.18.2",
                    "protocol_id": 758,
                    "packets": 798,
                    "type": "release"
                },
                 * 我们需要枚举所有key 然后找到name = $name的结构,并且返回protocol_id
                 */

                JsonProperty[] resultElements = document.RootElement.EnumerateObject().Where(element =>
                {
                    if(element.Value.TryGetProperty("name", out JsonElement result))
                    {
                        return result.GetString()?.ToLower() == name.ToLower();
                    }
                    return false;
                }).ToArray();

                if (0 == resultElements.Length)
                    throw new GameNotFoundException(name.ToLower());

                GameVersionInfo info = resultElements.First().Value.Deserialize<GameVersionInfo>();
                return new MinecraftProtocolVersion(info.ProtocolId);
            }
        }
    }
}
