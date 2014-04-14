namespace Secp256k1
{
    /// <summary>
    /// </summary>
    public static class VarIntExtensions
    {
        /// <summary>
        ///     Returns a VarInt as a variable length byte array representing the unsigned byte (uint8_t).
        ///     VarInt is a Bitcoin Protocol specific structure used for efficient transmission of integrals over the wire.
        ///     https://en.bitcoin.it/wiki/Protocol_specification#Variable_length_string
        /// </summary>
        /// <param name="value">byte on which the extension method is called</param>
        /// <returns>Byte Array of one to nine bytes containing unsigned integral value in Bitcoin VarInt format</returns>
        public static byte[] GetVarIntBytes(this byte value)
        {
            return new VarInt(value).ToByteArray();
        }

        /// <summary>
        ///     Returns a VarInt as a variable length byte array representing the unsigned short (uint16_t).
        ///     VarInt is a Bitcoin Protocol specific structure used for efficient transmission of integrals over the wire.
        ///     https://en.bitcoin.it/wiki/Protocol_specification#Variable_length_string
        /// </summary>
        /// <param name="value">ushort on which the extension method is called</param>
        /// <returns>Byte Array of one to nine bytes containing unsigned integral value in Bitcoin VarInt format</returns>
        public static byte[] GetVarIntBytes(this ushort value)
        {
            return new VarInt(value).ToByteArray();
        }

        /// <summary>
        ///     Returns a VarInt as a variable length byte array representing the unsigned int (uint32_t).
        ///     VarInt is a Bitcoin Protocol specific structure used for efficient transmission of integrals over the wire.
        ///     https://en.bitcoin.it/wiki/Protocol_specification#Variable_length_string
        /// </summary>
        /// <param name="value">uint on which the extension method is called</param>
        /// <returns>Byte Array of one to nine bytes containing unsigned integral value in Bitcoin VarInt format</returns>
        public static byte[] GetVarIntBytes(this uint value)
        {
            return new VarInt(value).ToByteArray();
        }

        /// <summary>
        ///     Returns a VarInt as a variable length byte array representing the unsigned long (uint64_t).
        ///     VarInt is a Bitcoin Protocol specific structure used for efficient transmission of integrals over the wire.
        ///     https://en.bitcoin.it/wiki/Protocol_specification#Variable_length_string
        /// </summary>
        /// <param name="value">ulong on which the extension method is called</param>
        /// <returns>Byte Array of one to nine bytes containing unsigned integral value in Bitcoin VarInt format</returns>
        public static byte[] GetVarIntBytes(this ulong value)
        {
            return new VarInt(value).ToByteArray();
        }
    }
}