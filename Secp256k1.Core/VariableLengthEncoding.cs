using System;
using System.Text;

namespace Secp256k1
{
    public class VariableLengthEncoding
    {
        public static byte[] GetBytes(byte length)
        {
            return GetBytes((ulong)length);
        }

        public static byte[] GetBytes(ushort length)
        {
            return GetBytes((ulong)length);
        }

        public static byte[] GetBytes(short length)
        {
            return GetBytes((ulong)length);
        }

        public static byte[] GetBytes(uint length)
        {
            return GetBytes((ulong)length);
        }

        public static byte[] GetBytes(int length)
        {
            return GetBytes((ulong)length);
        }

        public static byte[] GetBytes(long length)
        {
            return GetBytes((ulong)length);
        }

        public static byte[] GetBytes(ulong length)
        {
            byte[] bytes;

            if (length <= 0xfc)
            {
                bytes = new byte[1];

                bytes[0] = (byte)length;
            }
            else if (length <= 0xffff)
            {
                bytes = new byte[3];

                bytes[0] = 0xfd;

                bytes[1] = (byte)(length);
                bytes[2] = (byte)(length >> 8);
            }
            else if (length <= 0xffffffff)
            {
                bytes = new byte[5];

                bytes[0] = 0xfe;

                bytes[1] = (byte)(length);
                bytes[2] = (byte)(length >> 8);
                bytes[3] = (byte)(length >> 16);
                bytes[4] = (byte)(length >> 24);
            }
            else
            {
                bytes = new byte[9];

                bytes[0] = 0xff;

                bytes[1] = (byte)(length);
                bytes[2] = (byte)(length >> 8);
                bytes[3] = (byte)(length >> 16);
                bytes[4] = (byte)(length >> 24);
                bytes[5] = (byte)(length >> 32);
                bytes[6] = (byte)(length >> 40);
                bytes[7] = (byte)(length >> 48);
                bytes[8] = (byte)(length >> 56);
            }

            return bytes;
        }

        public static byte[] GetVariableLengthStringBytes(string value)
        {
            byte[] bytes;
            if (value == null)
            {
                return new byte[] { 0 };
            }

            if (value.Length <= 0xfc)
            {
                bytes = new byte[1 + value.Length];

                bytes[0] = (byte)value.Length;

                Encoding.UTF8.GetBytes(value, 0, value.Length, bytes, 1);
            }
            else if (value.Length <= 0xffff)
            {
                bytes = new byte[3 + value.Length];

                bytes[0] = 0xfd;

                bytes[1] = (byte)(value.Length);
                bytes[2] = (byte)(value.Length >> 8);

                Encoding.UTF8.GetBytes(value, 0, value.Length, bytes, 3);
            }
            else if (value.Length <= 0x7fffffff)
            {
                bytes = new byte[5 + value.Length];

                bytes[0] = 0xfe;

                bytes[1] = (byte)(value.Length);
                bytes[2] = (byte)(value.Length >> 8);
                bytes[3] = (byte)(value.Length >> 16);
                bytes[4] = (byte)(value.Length >> 24);

                Encoding.UTF8.GetBytes(value, 0, value.Length, bytes, 5);
            }
            else
            {
                throw new ArgumentException("The length of value should never exceed x7fffffff as it is an int");
            }

            return bytes;
        }
    }
}
