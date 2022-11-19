using System;

namespace MinecraftServerInfo.DataHelper
{
    public class VarInt
    {
        private static int _getLen(int @int)
        {
            if ((@int & (0xffffffff << 7)) == 0)
            {
                return 1;
            }
            if ((@int & (0xffffffff << 14)) == 0)
            {
                return 2;
            }
            if ((@int & (0xffffffff << 21)) == 0)
            {
                return 3;
            }
            if ((@int & (0xffffffff << 28)) == 0)
            {
                return 4;
            }
            return 5;
        }
        public static byte[] GetBytes(int @int)
        {
            return GetBytes(@int, _getLen(@int));
        }
        public static byte[] GetBytes(int @int, int len)
        {
            byte[] varIntBuffer = new byte[len];
            int index = 0;
            while (true)
            {
                if ((@int & ~0x7f) == 0)
                {
                    varIntBuffer[index] = (byte)(@int & 0x7f);
                    break;
                }
                else
                {
                    varIntBuffer[index] = (byte)((@int & 0x7f) | 0x80);
                    @int = @int >> 7;
                }
                index++;
            }
            return varIntBuffer;
        }
        public static bool IsVarintEnd(byte value)
        {
            return (value & 0x80) == 0;
        }
        public static int GetInt32(byte[] bytes)
        {
            int result = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte value = bytes[i];
                if ((value & 0xff) == 0)
                {
                    break;
                }
                result = result | (value & 127) << (7 * i);
            }
            return result;
        }
    }
}
