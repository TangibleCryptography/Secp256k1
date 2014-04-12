using System;
using System.Numerics;

namespace Secp256k1
{
    public class ECPoint : ICloneable
    {
        private BigInteger _x;
        private BigInteger _y;

        public BigInteger X
        {
            get
            {
                return _x;
            }
        }

        public BigInteger Y
        {
            get
            {
                return _y;
            }
        }

        public static ECPoint Infinity
        {
            get
            {
                return new ECPoint(true);
            }
        }

        private bool _isInfinity = false;
        public bool IsInfinity
        {
            get
            {
                return _isInfinity;
            }
        }

        private ECPoint(bool infinity)
        {
            if (!infinity)
            {
                throw new ArgumentException("This constructor is only for creating the point Infinity");
            }

            _isInfinity = true;
        }

        public ECPoint(BigInteger x, BigInteger y)
        {
            _x = x;
            _y = y;
        }

        public byte[] EncodePoint(bool compressed)
        {
            if (IsInfinity)
            {
                return new byte[1];
            }

            byte[] x = X.ToByteArrayUnsigned(true);
            byte[] encoded;
            if (!compressed)
            {
                byte[] y = Y.ToByteArrayUnsigned(true);
                encoded = new byte[65];
                encoded[0] = 0x04;
                Buffer.BlockCopy(y, 0, encoded, 33 + (32 - y.Length), y.Length);
            }
            else
            {
                encoded = new byte[33];
                encoded[0] = (byte)(Y.TestBit(0) ? 0x03 : 0x02);
            }

            Buffer.BlockCopy(x, 0, encoded, 1 + (32 - x.Length), x.Length);
            return encoded;
        }

        public static ECPoint DecodePoint(byte[] encoded)
        {
            if (encoded == null || (encoded.Length != 33 && encoded.Length != 65))
            {
                throw new FormatException("Invalid encoded point");
            }

            if (encoded[0] == 0x04)
            {
                // uncompressed
                byte[] unsigned = new byte[32];
                
                Buffer.BlockCopy(encoded, 1, unsigned, 0, 32);
                
                var x = unsigned.ToBigIntegerUnsigned(true);

                Buffer.BlockCopy(encoded, 33, unsigned, 0, 32);
                var y = unsigned.ToBigIntegerUnsigned(true);

                return new ECPoint(x, y);
            }
            else if (encoded[0] == 0x02 || encoded[0] == 0x03)
            {
                // compressed
                byte[] unsigned = new byte[32];

                Buffer.BlockCopy(encoded, 1, unsigned, 0, 32);
                var x = unsigned.ToBigIntegerUnsigned(true);

                // solve y
                var y = ((x * x * x + 7) % Secp256k1.P).ShanksSqrt(Secp256k1.P);

                bool negate = false;
                if (y.TestBit(0))
                {
                    if (encoded[0] == 0x02)
                    {
                        negate = true;
                    }
                }
                else
                {
                    if (encoded[0] == 0x03)
                    {
                        negate = true;
                    }
                }
                
                if (negate)
                {
                    // negate
                    y = -y + Secp256k1.P;
                }

                return new ECPoint(x, y);
            }
            else
            {
                throw new FormatException("Invalid encoded point");
            }
        }

        public ECPoint Negate()
        {
            ECPoint r = (ECPoint)Clone();
            r._y = -r._y + Secp256k1.P;
            return r;
        }

        public ECPoint Subtract(ECPoint b)
        {
            return Add(b.Negate());
        }

        public ECPoint Add(ECPoint b)
        {
            BigInteger m;
            BigInteger r = 0;

            if (this.IsInfinity)
            {
                return b;
            }
            if (b.IsInfinity)
            {
                return this;
            }

            if (X - b.X == 0)
            {
                if (Y - b.Y == 0)
                {
                    m = 3 * X * X * (2 * Y).ModInverse(Secp256k1.P);
                }
                else
                {
                    return ECPoint.Infinity;
                }
            }
            else
            {
                var mx = (X - b.X);
                if (mx < 0)
                {
                    mx += Secp256k1.P;
                }
                m = (Y - b.Y) * mx.ModInverse(Secp256k1.P);
            }

            m = m % Secp256k1.P;

            var v = Y - m * X;
            var x3 = (m * m - X - b.X);
            x3 = x3 % Secp256k1.P;
            if (x3 < 0)
            {
                x3 += Secp256k1.P;
            }
            var y3 = -(m * x3 + v);
            y3 = y3 % Secp256k1.P;
            if (y3 < 0)
            {
                y3 += Secp256k1.P;
            }

            return new ECPoint(x3, y3);
        }

        public ECPoint Twice()
        {
            return Add(this);
        }

        public ECPoint Multiply(BigInteger b)
        {
            if (b.Sign == -1)
            {
                throw new FormatException("The multiplicator cannot be negative");
            }

            b = b % Secp256k1.N;

            ECPoint result = ECPoint.Infinity;
            ECPoint temp =  null;

            int bit = 0;
            do
            {
                if (temp == null)
                {
                    temp = this;
                }
                else
                {
                    temp = temp.Twice();
                }

                if (!b.IsEven)
                {
                    if (result.IsInfinity)
                    {
                        result = temp;
                    }
                    else
                    {
                        result = result.Add(temp);
                    }
                }
                bit++;
            } while ((b >>= 1) != 0);

            return result;
        }

        public ECPoint(BigInteger x, BigInteger y, bool isInfinity)
        {
            _x = x;
            _y = y;
            _isInfinity = isInfinity;
        }

        public object Clone()
        {
            return new ECPoint(_x, _y, _isInfinity);
        }
    }
}
