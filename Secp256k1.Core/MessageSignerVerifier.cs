using System;
using System.Numerics;

namespace Secp256k1
{
    public class MessageSignerVerifier
    {
        private readonly ECDsaSigner signer = new ECDsaSigner();

        public bool Verify(SignedMessage message)
        {
            var r = new byte[32];
            var s = new byte[32];
            Buffer.BlockCopy(message.SignatureBytes, 1, r, 0, 32);
            Buffer.BlockCopy(message.SignatureBytes, 33, s, 0, 32);
            var recId = message.SignatureBytes[0] - 27;

            var magicData = ("Bitcoin Signed Message:\n").GetVarString();
            var messageData = message.Message.GetVarString();

            var data = new byte[magicData.Length + messageData.Length];
            Buffer.BlockCopy(magicData, 0, data, 0, magicData.Length);
            Buffer.BlockCopy(messageData, 0, data, magicData.Length, messageData.Length);

            var hash = SHA256.DoubleHash(data);

            var point = signer.RecoverFromSignature(hash, r.ToBigIntegerUnsigned(true), s.ToBigIntegerUnsigned(true), recId);

            var pubKeyHash = Hash160.Hash(point.EncodePoint(false));
            var addressBytes = new byte[pubKeyHash.Length + 1];
            Buffer.BlockCopy(pubKeyHash, 0, addressBytes, 1, pubKeyHash.Length);

            var address = Base58.EncodeWithCheckSum(addressBytes);

            if (address == message.Address)
                return true;
            return false;
        }

        public SignedMessage Sign(BigInteger privateKey, string message)
        {
            var magicData = ("Bitcoin Signed Message:\n").GetVarString();
            var messageData = message.GetVarString();

            var data = new byte[magicData.Length + messageData.Length];
            Buffer.BlockCopy(magicData, 0, data, 0, magicData.Length);
            Buffer.BlockCopy(messageData, 0, data, magicData.Length, messageData.Length);

            var hash = SHA256.DoubleHash(data);
            var signature = signer.GenerateSignature(privateKey, hash);

            var recId = -1;
            var publicKey = Secp256k1.G.Multiply(privateKey);

            for (var i = 0; i < 4; i++)
            {
                var Q = signer.RecoverFromSignature(hash, signature[0], signature[1], i);

                if (Q.X == publicKey.X && Q.Y == publicKey.Y)
                {
                    recId = i;
                    break;
                }
            }
            if (recId == -1) throw new Exception("Did not find proper recid");

            var signatureBytes = new byte[65];

            signatureBytes[0] = (byte) (27 + recId);
            var rByteArray = signature[0].ToByteArrayUnsigned(true);
            var sByteArray = signature[1].ToByteArrayUnsigned(true);

            Buffer.BlockCopy(rByteArray, 0, signatureBytes, 1 + (32 - rByteArray.Length), rByteArray.Length);
            Buffer.BlockCopy(sByteArray, 0, signatureBytes, 33 + (32 - sByteArray.Length), sByteArray.Length);

            var signedMessage = new SignedMessage(message, publicKey.GetBitcoinAddress(), signatureBytes);

            return signedMessage;
        }
    }
}