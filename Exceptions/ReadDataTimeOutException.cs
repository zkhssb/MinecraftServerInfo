using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerInfo.Exceptions
{
    internal class ReadDataTimeOutException:Exception
    {
        public ReadDataTimeOutException():base("读取数据超时"){}
    }
}
