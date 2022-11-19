using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerInfo.Exceptions
{
    internal class GameNotFoundException:Exception
    {
        public GameNotFoundException(string version) : base($"找不到游戏 {version} 对应的协议ID")
        {

        }
    }
}
