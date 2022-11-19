using MinecraftServerInfo.DataHelper;
using MinecraftServerInfo.Exceptions;
using MinecraftServerInfo.Model;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace MinecraftServerInfo
{
    public class MinecraftServer
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        private readonly string _host;
        /// <summary>
        /// 服务器端口
        /// </summary>
        private readonly short _port = 25565;
        /// <summary>
        /// 协议版本
        /// </summary>
        private readonly int _protocol = 0;
        public MinecraftServer(string host, int protocol = 0)
        {
            _host = host;
            _protocol = protocol;
        }
        public MinecraftServer(string host, MinecraftProtocolVersion protocolVersion)
        {
            _host = host;
            _protocol = protocolVersion._version;
        }
        public MinecraftServer(string host, short port, int protocol = 0)
        {
            _host = host;
            _port = port;
            _protocol = protocol;
        }
        public MinecraftServer(string host, short port, MinecraftProtocolVersion protocolVersion)
        {
            _host = host;
            _port = port;
            _protocol = protocolVersion._version;
        }

        /// <summary>
        /// 获取服务器信息
        /// https://mineplugin.org/Protocol#.E6.8F.A1.E6.89.8B.E5.8D.8F.E8.AE.AE_2
        /// </summary>
        /// <param name="timeOut">超时时间 (单位为秒)</param>
        /// <returns></returns>
        public async Task<ServerInfo> PingAsync(CancellationToken cancellationToken)
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(_host, _port, cancellationToken);
                NetworkStream networkStream = client.GetStream();

                // Ping
                using (MemoryStream packet = new MemoryStream())
                {
                    byte[] host = Encoding.UTF8.GetBytes(_host);
                    MinecraftDataBuilder handshakPing = new MinecraftDataBuilder(new List<object>()
                    {
                        0x00, // Packet ID
                        _protocol, // 协议版本
                        host.Length,// 服务器地址长度
                        host,// 服务器地址
                        _port,// 端口
                        1,// 下一阶段 (1 for status, 2 for login)
                    });
                    handshakPing.BuildAndPack(packet);
                    using (MemoryStream packetConfirm = new MemoryStream())
                    {
                        MinecraftDataBuilder handshakConfirm = new MinecraftDataBuilder(new List<object>()
                        {
                            0x00, // 表示确认
                        });
                        handshakConfirm.BuildAndPack(packetConfirm);

                        // 先发送获取信息的数据
                        networkStream.Write(packet.ToArray());
                        networkStream.Flush();
                        // 然后发送确认数据
                        networkStream.Write(packetConfirm.ToArray());
                        networkStream.Flush();
                        // 开始读取数据
                        byte[] resultBytes;
                        using (MemoryStream result = new MemoryStream())
                        {
                            // 当前读取长度
                            int len = 0;
                            // 数据总长度
                            int packageLen = 0;
                            // 缓存
                            byte[] buffer = new byte[1024];

                            // 第一次先做一个读取
                            while (len == 0)
                            {
                                cancellationToken.ThrowIfCancellationRequested();
                                len = await networkStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                            }
                            if (len < 2)
                                throw new ServerResultException();
                            result.Write(buffer, 0, len);
                            packageLen = VarInt.GetInt32(buffer.Take(2).ToArray());

                            while (true)
                            {
                                if (result.Length >= packageLen)
                                    break;
                                cancellationToken.ThrowIfCancellationRequested();
                                len = await networkStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                                if (len > 0)
                                {
                                    result.Write(buffer, 0, len);
                                }
                            }
                            resultBytes = result.ToArray();
                        }
                        // 获取实际数据长度
                        int packedDataLen = VarInt.GetInt32(resultBytes.Skip(3).Take(2).ToArray());

                        // 开始解析数据
                        return JsonSerializer.Deserialize<ServerInfo>(
                            Encoding.UTF8.GetString(resultBytes.Skip(5).Take(packedDataLen).ToArray()))!;
                    }
                }
            }
        }
    }
}
