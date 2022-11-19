namespace MinecraftServerInfo.Exceptions
{
    internal class ServerResultException:Exception
    {
        public ServerResultException() : base("服务器返回了错误的数据包")
        {

        }
    }
}
