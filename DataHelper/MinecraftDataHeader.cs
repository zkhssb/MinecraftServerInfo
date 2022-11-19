namespace MinecraftServerInfo.DataHelper
{
    public class MinecraftDataHeader
    {
        public static void Pack(MemoryStream ms)
        {
            using(MemoryStream header = new MemoryStream())
            {
                header.Write(VarInt.GetBytes((int)ms.Length));
                header.Write(ms.ToArray());
                ms.Position = 0;
                ms.Write(header.ToArray());
            }
        }
    }
}
