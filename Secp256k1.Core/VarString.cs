using System;
using System.IO;
using System.Text;

namespace Secp256k1
{
    class VarString
    {
        public string Value { get; private set; }

        public int Length 
        {
            get { return Value.Length; }
        }

        public VarInt VarLength
        {
            get { return new VarInt((ulong)Length);}
        }

        public VarString(string str)
        {
            Value = str;
        }

        public VarString(byte[] buff, int offset = 0):this(Decode(buff, offset)){ }

        public byte[] GetBytes()
        {
            return Encode(Value);
        }

        public static byte[] Encode(string str)
        {
            if (str == null)
                return new byte[] { 0 };

            // strings of more than 2,147,483,647 bytes are not possible due to length property being of type (signed) Int32
            var varIntBytes = ((uint)str.Length).GetVarIntBytes();
            var stringBytes = new byte[varIntBytes.Length + str.Length]; // Make byte array large enough to hold length & string
            varIntBytes.CopyTo(stringBytes, 0); // VarInt of up to 9 bytes
            Encoding.UTF8.GetBytes(str, 0, str.Length, stringBytes, varIntBytes.Length); // string contents
            return stringBytes;
        }

        public static string Decode(byte[] buff, int offset = 0)
        {
            var varLength = new VarInt(buff, offset);

            if (varLength.Value > Int32.MaxValue)
                throw new NotImplementedException("Encoded length of string is greater than Int32.MaxValue");

            return Encoding.UTF8.GetString(buff, offset + varLength.SizeInBytes, (int)varLength.Value);
        }
    }
}
