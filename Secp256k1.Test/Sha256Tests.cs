using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Secp256k1.Test
{
    [TestFixture]
    public class ShaTests
    {
        // Test Vectors partially taken from https://www.dlitz.net/crypto/shad256-test-vectors/SHAd256_Test_Vectors.txt

        [Test]
        public void TestHashOfTextInput()
        {
            var textTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"}, //empty string
                {"abc", "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"},
                {"abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", "248d6a61d20638b8e5c026930c3e6039a33ce45964ff2167f6ecedd419db06c1"},
                {"The quick brown fox jumps over the lazy dog","d7a8fbb307d7809469ca9abcb0082e4f8d5651e46d3cdb762d02d0bf37c9e592"},
                {"The quick brown fox jumps over the lazy dog.","ef537f25c895bfa782526529a9b63d97aa631564d5d789c2b765448c8635fb6c"}
            };

            //test hex string overload.  SHA256.Hash(string hexData)
            foreach (var testString in textTestVectors)
            {
                var hex = Hex.AsciiToHex(testString.Key);
                var hashBytes = SHA256.Hash(hex);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }

            //test byte array overload.  SHA256.Hash(byte[] data)
            foreach (var testString in textTestVectors)
            {
                var bytes = Encoding.UTF8.GetBytes(testString.Key);
                var hashBytes = SHA256.Hash(bytes);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }


        }

        [Test]
        public void TestHashOfHexInput()
        {
            var hexTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"}, //empty string
                {"616263", "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"},
                {"6162636462636465636465666465666765666768666768696768696a68696a6b696a6b6c6a6b6c6d6b6c6d6e6c6d6e6f6d6e6f706e6f7071", "248d6a61d20638b8e5c026930c3e6039a33ce45964ff2167f6ecedd419db06c1"},
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f67","d7a8fbb307d7809469ca9abcb0082e4f8d5651e46d3cdb762d02d0bf37c9e592"},
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f672e","ef537f25c895bfa782526529a9b63d97aa631564d5d789c2b765448c8635fb6c"},
            };

            foreach (var testString in hexTestVectors)
            {
                var hashBytes = SHA256.Hash(testString.Key);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }
        }


        [Test]
        public void TestDoubleHashOfTextInput()
        {
            var textTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "5df6e0e2761359d30a8275058e299fcc0381534545f55cf43e41983f5d4c9456"}, //empty string
                {"abc", "4f8b42c22dd3729b519ba6f68d2da7cc5b2d606d05daed5ad5128cc03e6c6358"},
                {"abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq","0cffe17f68954dac3a84fb1458bd5ec99209449749b2b308b7cb55812f9563af"},
                {"The quick brown fox jumps over the lazy dog","6d37795021e544d82b41850edf7aabab9a0ebe274e54a519840c4666f35b3937"},
                {"The quick brown fox jumps over the lazy dog.","a51a910ecba8a599555b32133bf1829455d55fe576677b49cb561d874077385c"}
            };

            //test hex string overload.  SHA256.DoubleHash(string hexData)
            foreach (var testString in textTestVectors)
            {
                var hex = Hex.AsciiToHex(testString.Key);
                var hashBytes = SHA256.DoubleHash(hex);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }

            //test byte array overload.  SHA256.DoubleHash(byte[] data)
            foreach (var testString in textTestVectors)
            {
                var bytes = Encoding.UTF8.GetBytes(testString.Key);
                var hashBytes = SHA256.DoubleHash(bytes);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }
        }

        [Test]
        public void TestDoubleHashOfHexInput()
        {
            var hexTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "5df6e0e2761359d30a8275058e299fcc0381534545f55cf43e41983f5d4c9456"},
                {"616263", "4f8b42c22dd3729b519ba6f68d2da7cc5b2d606d05daed5ad5128cc03e6c6358"},
                {"6162636462636465636465666465666765666768666768696768696a68696a6b696a6b6c6a6b6c6d6b6c6d6e6c6d6e6f6d6e6f706e6f7071", "0cffe17f68954dac3a84fb1458bd5ec99209449749b2b308b7cb55812f9563af"},
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f67","6d37795021e544d82b41850edf7aabab9a0ebe274e54a519840c4666f35b3937"},
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f672e","a51a910ecba8a599555b32133bf1829455d55fe576677b49cb561d874077385c"},
            };

            foreach (var testString in hexTestVectors)
            {
                var hashBytes = SHA256.DoubleHash(testString.Key);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }
        }

        [Test]
        public void TestDoubleHashCheckSumOfTextInput()
        {
            var textTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "5df6e0e2"},
                {"abc", "4f8b42c2"},
                {"abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq","0cffe17f"},
                {"The quick brown fox jumps over the lazy dog","6d377950"},
                {"The quick brown fox jumps over the lazy dog.","a51a910e"}
            };

            //test hex string overload.  SHA256.DoubleHash(string hexData)
            foreach (var testString in textTestVectors)
            {
                var hex = Hex.AsciiToHex(testString.Key);
                var hashBytes = SHA256.DoubleHashCheckSum(hex);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }

            //test byte array overload.  SHA256.DoubleHash(byte[] data)
            foreach (var testString in textTestVectors)
            {
                var bytes = Encoding.UTF8.GetBytes(testString.Key);
                var hashBytes = SHA256.DoubleHashCheckSum(bytes);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }
        }

        [Test]
        public void TestDoubleHashCheckSumOfHexInput()
        {
            var hexTestVectors = new Dictionary<string, string>
            {
                {string.Empty, "5df6e0e2"},
                {"616263", "4f8b42c2"},
                {"6162636462636465636465666465666765666768666768696768696a68696a6b696a6b6c6a6b6c6d6b6c6d6e6c6d6e6f6d6e6f706e6f7071", "0cffe17f"},
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f67","6d377950"},
                {"54686520717569636b2062726f776e20666f78206a756d7073206f76657220746865206c617a7920646f672e","a51a910e"},
            };

            foreach (var testString in hexTestVectors)
            {
                var hashBytes = SHA256.DoubleHashCheckSum(testString.Key);
                Assert.AreEqual(testString.Value, hashBytes.ToHex());
            }
        }
    }
}
