using System.Collections.Generic;
using NUnit.Framework;

namespace Secp256k1.Test
{
    [TestFixture]
    class HexStringTests
    {
        private readonly Dictionary<string, byte[]> _testStrings = new Dictionary<string, byte[]>
        {
            {
                "0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8",
                new byte[]
                {
                    4, 121, 190, 102, 126, 249, 220, 187, 172, 85, 160, 98, 149, 206, 135, 11, 7, 2, 155, 252, 219, 45, 206, 40, 217, 89, 242, 129, 91,
                    22, 248, 23, 152, 72, 58, 218, 119, 38, 163, 196, 101, 93, 164, 251, 252, 14, 17, 8, 168, 253, 23, 180, 72, 166, 133, 84, 25, 156,
                    71, 208, 143, 251, 16, 212, 184
                }
            },
            {
                "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141",
                new byte[]
                {
                    0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 254, 186, 174, 220, 230, 175, 72, 160, 59, 191, 210, 94, 140, 208, 54, 65, 65
                }
            },
            {
                "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F",
                new byte[]
                {
                    0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 254, 255, 255, 252, 47
                }
            }
        };

        [Test]
        public void TestHexToBigint()
        {
            foreach (var testString in _testStrings)
                Assert.AreEqual(testString.Value.ToBigIntegerUnsigned(true), Hex.HexToBigInteger(testString.Key));
        }

    }
}
