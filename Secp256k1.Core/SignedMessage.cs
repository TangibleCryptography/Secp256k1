using System;

namespace Secp256k1
{
    public class SignedMessage
    {
        private string _message;

        public string Message
        {
            get { return _message; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
        }

        private byte[] _signatureBytes;

        public byte[] SignatureBytes
        {
            get { return _signatureBytes; }
        }

        private string _signature = null;

        public string Signature
        {
            get
            {
                if (_signature == null)
                {
                    _signature = Convert.ToBase64String(SignatureBytes);
                }

                return _signature;
            }
        }

        private string _formattedMessage = null;
        public string FormattedMessage
        {
            get
            {
                if (_formattedMessage == null)
                {
                    _formattedMessage = "-----BEGIN BITCOIN SIGNED MESSAGE-----\n";
                    _formattedMessage += Message;
                    _formattedMessage += "\n-----BEGIN SIGNATURE-----\n";
                    _formattedMessage += Address;
                    _formattedMessage += "\n";
                    _formattedMessage += Signature;
                    _formattedMessage += "\n-----END BITCOIN SIGNED MESSAGE-----";
                }

                return _formattedMessage;
            }
        }

        public SignedMessage(string message, string address, byte[] signatureBytes)
        {
            this._address = address;
            this._message = message;
            this._signatureBytes = signatureBytes;
        }
    }
}
