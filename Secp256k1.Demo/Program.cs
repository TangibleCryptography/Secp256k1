using System;
using System.Numerics;
using System.Text;

namespace Secp256k1.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Key Generation
            const string privKeyHex = "E9873D79C6D87DC0FB6A5778633389F4453213303DA61F20BD67FC233AA33262";
            BigInteger privateKey = Hex.HexToBigInteger(privKeyHex);
            ECPoint publicKey = Secp256k1.G.Multiply(privateKey);
            string bitcoinAddressUncompressed = publicKey.GetBitcoinAddress(compressed: false);
            string bitcoinAddressCompressed = publicKey.GetBitcoinAddress(compressed: true);

            Console.WriteLine("PrivateKey (Hex): {0}", privateKey.ToHex());
            Console.WriteLine("Address (Uncompressed): {0}", bitcoinAddressUncompressed);
            Console.WriteLine("Address (Compressed): {0}", bitcoinAddressCompressed);

            // encryption
            ECEncryption encryption = new ECEncryption();
            const string message = "This is my encrypted message";
            byte[] encrypted = encryption.Encrypt(publicKey, message);
            byte[] decrypted = encryption.Decrypt(privateKey, encrypted);
            string decryptedMessage = Encoding.UTF8.GetString(decrypted);

            // signing
            MessageSignerVerifier messageSigner = new MessageSignerVerifier();
            SignedMessage signedMessage = messageSigner.Sign(privateKey, "Test Message to sign, you can verify this on http://brainwallet.org/#verify");
            bool verified = messageSigner.Verify(signedMessage);

            Console.WriteLine("Press Any Key ...");
            Console.ReadKey();
        }
    }
}
