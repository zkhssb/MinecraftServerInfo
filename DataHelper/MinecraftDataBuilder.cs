using System.Text;

namespace MinecraftServerInfo.DataHelper
{
    public class MinecraftDataBuilder
    {
        private readonly List<object> _data = new List<object>();
        public MinecraftDataBuilder()
        {

        }
        public MinecraftDataBuilder(List<object> data)
        {
            _data = data;
        }
        public byte[] Build()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Build(ms);
                return ms.ToArray();
            }
        }
        public void Build(MemoryStream ms)
        {
            foreach (var data in _data)
            {
                if (data.GetType() == typeof(int))
                {
                    ms.Write(VarInt.GetBytes((int)data));
                }
                else if (data.GetType() == typeof(string))
                {
                    ms.Write(Encoding.UTF8.GetBytes((string)data));
                }
                else if (data.GetType() == typeof(short))
                {
                    ms.Write(BitConverter.GetBytes((short)data)
                        .Reverse().ToArray());
                }
                else if (data.GetType() == typeof(byte[]))
                {
                    ms.Write((byte[])data);
                }
                else if (data.GetType() == typeof(byte))
                {
                    ms.WriteByte((byte)data);
                }
            }
        }
        public void BuildAndPack(MemoryStream ms)
        {
            Build(ms);
            MinecraftDataHeader.Pack(ms);
        }
    }
}
