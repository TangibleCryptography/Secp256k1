using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Secp256k1.Test
{
    [TestFixture]
    public class RipeMd160Tests
    {
        // Test Vectors partially taken from http://homes.esat.kuleuven.be/~bosselae/ripemd160.html

        [Test]
        public void TestHashOfTextInput()
        {
            var textTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "9c1185a5c5e9fc54612808977ee8f548b2258d31"},
                {"a","0bdc9d2d256b3ee9daae347be6f4dc835a467ffe"},
                {"abc", "8eb208f7e05d987a9b044a8e98c6b087f15a0bfc"},
                {"abcdefghijklmnopqrstuvwxyz","f71c27109c692c1b56bbdceb5b9d2865b3708dbc"},
                {"abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", "12a053384a9c0c88e405a06c27dcf49ada62eb2b"},
                {"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789","b0e20b6e3116640286ed3a87a5713079b21f5189"},
                {"The quick brown fox jumps over the lazy dog","37f332f68db77bd9d7edd4969571ad671cf9dd3b"},
                {"The quick brown fox jumps over the lazy dog.","fc850169b1f2ce72e3f8aa0aeb5ca87d6f8519c6"}
            };

            //test hex string overload.  RIPEMD160.Hash(string hexData)
            foreach (var testString in textTestVectors)
            {
                var hex = Hex.AsciiToHex(testString.Key);
                var hashBytes = RIPEMD160.Hash(hex);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }

            //test byte array overload.  RIPEMD160.Hash(byte[] data)
            foreach (var testString in textTestVectors)
            {
                var bytes = Encoding.UTF8.GetBytes(testString.Key);
                var hashBytes = RIPEMD160.Hash(bytes);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }


        }

        [Test]
        public void TestHashOfHexInput()
        {
            var hexTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "9c1185a5c5e9fc54612808977ee8f548b2258d31"}, //empty string
                {"61","0bdc9d2d256b3ee9daae347be6f4dc835a467ffe"}, //"a"
                {"616263", "8eb208f7e05d987a9b044a8e98c6b087f15a0bfc"}, //"abc" 
                {"6162636465666768696a6b6c6d6e6f707172737475767778797a","f71c27109c692c1b56bbdceb5b9d2865b3708dbc"}, //"abcdefghijklmnopqrstuvwxyz"
                {"6162636462636465636465666465666765666768666768696768696a68696a6b696a6b6c6a6b6c6d6b6c6d6e6c6d6e6f6d6e6f706e6f7071", "12a053384a9c0c88e405a06c27dcf49ada62eb2b"}, //"abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"
                {"4142434445464748494a4b4c4d4e4f505152535455565758595a6162636465666768696a6b6c6d6e6f707172737475767778797a30313233343536373839","b0e20b6e3116640286ed3a87a5713079b21f5189"}, //"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f67","37f332f68db77bd9d7edd4969571ad671cf9dd3b"}, //"The quick brown fox jumps over the lazy dog"
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f672e","fc850169b1f2ce72e3f8aa0aeb5ca87d6f8519c6"} //"The quick brown fox jumps over the lazy dog."
            };

            foreach (var testString in hexTestVectors)
            {
                var hashBytes = RIPEMD160.Hash(testString.Key);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }
        }
    }
}
