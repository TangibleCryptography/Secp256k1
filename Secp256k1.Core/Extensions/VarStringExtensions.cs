using System;
using System.IO;
using System.Text;

namespace Secp256k1
{
    /// <summary>
    /// </summary>
    public static class VarStringExtensions
    {
        /// <summary>
        ///     Extension Method for string type
        ///     Returns a byte array with string length as a VarInt followed by string contents
        ///     Is a Bitcoin Protocol specific structure
        ///     https://en.bitcoin.it/wiki/Protocol_specification#Variable_length_string
        ///     Limitation: The Bitcoin protocol supports string up to 2^64 -1 (unit64) bytes in length.
        ///     The C# built in type "string" only supports strings up to 2^31-1 (singed int32) bytes in length.
        ///     To strictly comply with Bitcoin spec would require a new class capable of longer strings. Although
        ///     as a practical matter no existing messages support string lengths beyond the Int32 limit.
        /// </summary>
        /// <param name="str">Instance of the string class being extended.</param>
        /// <returns>Byte Array containing the variable length of the string.</returns>
        public static byte[] GetVarString(this string str)
        {
            return VarString.Encode(str);
        }
    }
}