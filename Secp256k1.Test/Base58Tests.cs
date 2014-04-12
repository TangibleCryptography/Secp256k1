using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Secp256k1.Test
{
    internal delegate byte[] Base58DecodeDelegate(string encodedString);
    
    class Base58Tests
    {
        [TestFixture]
        public class Base58Test
        {
            private readonly Dictionary<string, string> _testStrings = new Dictionary<string, string>
                {
                    {string.Empty, ""},
                    {"61", "2g"},
                    {"626262", "a3gV"},
                    {"636363", "aPEr"},
                    {"73696d706c792061206c6f6e6720737472696e67", "2cFupjhnEsSn59qHXstmK2ffpLv2"},
                    {"00eb15231dfceb60925886b67d065299925915aeb172c06647", "1NS17iag9jJgTHD1VXjvLCEnZuQ3rJDE9L"},
                    {"516b6fcd0f", "ABnLTmg"},
                    {"bf4f89001e670274dd", "3SEo3LWLoPntC"},
                    {"572e4794", "3EFU7m"},
                    {"ecac89cad93923c02321", "EJDM8drfXA6uyA"},
                    {"10c8511e", "Rt5zm"},
                    {"00000000000000000000", "1111111111"}
                };
                
            [Test]
            public void TestEncode()
            {
                foreach (var testString in _testStrings)
                {
                    var testBytes = Hex.HexToBytes(testString.Key);
                    Assert.AreEqual(testString.Value, Base58.Encode(testBytes));
                }
            }

            [Test]
            public void TestDecode()
            {
                foreach (var testString in _testStrings)
                {
                    var testBytes = Base58.Decode(testString.Value);
                    Assert.AreEqual(testBytes.ToHex(), testString.Key);
                }
            }

            [Test]
            public void TestInvalid()
            {
                Assert.Throws<FormatException>(delegate { Base58.Decode("This isn't valid base58"); });
                Assert.Throws<FormatException>(delegate { Base58.Decode(" "); });
            }
        }

    }
}
