using System;

namespace Secp256k1
{
    /// <summary>
    /// </summary>
    public class VarInt
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        public VarInt(ulong value)
        {
            Value = value;
        }

        public VarInt(long value) : this ((ulong)value){ }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        public VarInt(byte[] buffer)
            : this(buffer, 0) {}

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public VarInt(byte[] buffer, int offset)
        {
            Value = Decode(buffer, offset);
        }

        /// <summary>
        /// </summary>
        public ulong Value { get; private set; }

        /// <summary>
        /// </summary>
        public int SizeInBytes
        {
            get
            {
                if (Value <= 252) //0xfc
                    return 1; //directly stored as a single byte (0x00 to 0xfc)
                if (Value <= ushort.MaxValue)
                    return 3; // 1 byte prefix (0xfd) + value as uint16 (2 bytes)
                if (Value <= uint.MaxValue)
                    return 5; // 1 byte prefix (0xfe) + value as unit32 (4 bytes)
                //else
                return 9; // 1 byte prefix (0xff) + value as unit64 (8 bytes)
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            byte[] varIntBytes;

            if (Value <= 252) //Values up to 0xfc stored directly without prefix (0xfd, 0xfe, and 0xff in first byte reserved as prefixes)
            {
                varIntBytes = new byte[1];
                // no prefix indicates value stored as byte directly
                varIntBytes[0] = (byte) Value;
            }
            else if (Value <= UInt16.MaxValue)
            {
                varIntBytes = new byte[3];
                varIntBytes[0] = 0xfd; // leading byte prefix indicates uint16 follows
                BitConverter.GetBytes((ushort) Value).CopyTo(varIntBytes, 1);
            }
            else if (Value <= UInt32.MaxValue)
            {
                varIntBytes = new byte[5];
                varIntBytes[0] = 0xfe; // leading byte prefix indicates uint32 follows
                BitConverter.GetBytes((uint) Value).CopyTo(varIntBytes, 1);
            }
            else
            {
                varIntBytes = new byte[9];
                varIntBytes[0] = 0xff; // leading byte prefix indicates uint64 follows
                BitConverter.GetBytes(Value).CopyTo(varIntBytes, 1);
            }
            return varIntBytes;
        }

        public static ulong Decode(byte[] buffer, int offset=0)
        {
            switch (buffer[offset]) //check first byte for prefix value (0xfd, 0xfe, or 0xff)
            {
                case 253: //0xfd
                    return BitConverter.ToUInt16(buffer, offset + 1); // value stored as prefix + uint16

                case 254: //0xfe
                    return BitConverter.ToUInt32(buffer, offset + 1); // value stored as prefix + uint32

                case 255: //0xff
                    return BitConverter.ToUInt64(buffer, offset + 1); // value stored as prefix + uint64

                default: //0x00 to 0xfc
                    return buffer[offset]; // value stored as direct byte without a prefix
            }
        }

        public static byte[] Encode(ulong value)
        {
            return new VarInt(value).ToByteArray();
        }
    }
}