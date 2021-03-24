using System.Numerics;
using System.Threading.Tasks;
using Conflux.Model;
using Conflux.Hex.HexConvertors.Extensions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Conflux.Util;
using Conflux.RLP;

namespace Conflux.Signer
{
    public static class Tool
    {
        public static List<string> ToHex(this List<byte[]> raw)
        {
            List<string> resultList = new List<string>();
            foreach (var r in raw)
            {
                resultList.Add(r.ToHex());
            }
            return resultList;
        }
        public static byte[] Add(this byte[] currentByte, byte[] newByte)
        {
            var currentList = currentByte.ToList();
            var newList = newByte.ToList();
            currentList.AddRange(newList);
            return currentList.ToArray();
        }
        public static byte[] Clear(this byte[] rawBytes)
        {
            //0xff0000 
            var tmpBytes = rawBytes.ToList();
            tmpBytes.Reverse();

            var validIndex = 0;
            for (int i = 0; i < tmpBytes.Count; i++)
            {
                if (tmpBytes[i] != 0)
                {
                    validIndex = i;
                    break;
                }
            }
            var rawBytesList = rawBytes.ToList();
            rawBytesList.RemoveRange(rawBytes.Length - validIndex, validIndex);
            rawBytesList.Reverse();
            return rawBytesList.ToArray();
        }
    }
    public abstract class SignedTransactionBase
    {
        private readonly Sha3Keccack sha3Keccack = new Sha3Keccack();
        public static RLPSigner CreateDefaultRLPSigner(byte[] rawData)
        {
            return new RLPSigner(rawData, NUMBER_ENCODING_ELEMENTS);
        }

        //Number of encoding elements (output for transaction)
        public const int NUMBER_ENCODING_ELEMENTS = 9;
        public static readonly BigInteger DEFAULT_GAS_PRICE = BigInteger.Parse("1");
        public static readonly BigInteger DEFAULT_GAS_LIMIT = BigInteger.Parse("21000");
        public static readonly BigInteger DEFAULT_STORAGE_LIMIT = BigInteger.Parse("0");


        protected RLPSigner SimpleRlpSigner { get; set; }

        public byte[] RawHash => SimpleRlpSigner.RawHash;

        /// <summary>
        ///     The counter used to make sure each transaction can only be processed once, you may need to regenerate the
        ///     transaction if is too low or too high, simples way is to get the number of transacations
        /// </summary>
        public byte[] Nonce { get; set; }

        public byte[] Value => SimpleRlpSigner.Data[4] ?? DefaultValues.ZERO_BYTE_ARRAY;

        public byte[] to { get; set; }

        public byte[] GasPrice => SimpleRlpSigner.Data[1] ?? DefaultValues.ZERO_BYTE_ARRAY;

        public byte[] Gas { get; set; }

        public byte[] Storage { get; set; }

        public byte[] Data { set; get; }

        public EthECDSASignature Signature => SimpleRlpSigner.Signature;

        public abstract EthECKey Key { get; }


        public byte[] GetRLPEncoded()
        {
            return SimpleRlpSigner.GetRLPEncoded();
        }
        #region conflux code

        #endregion
        public byte[] storageLimit { get; set; }
        public byte[] _epochHeight { get; set; }
        public byte[] _nonce { get; set; }
        public byte[] chianId { get; set; }


        protected BigInteger __amount;
        protected BigInteger __nonce;
        protected BigInteger __gasPrice;
        protected BigInteger __gasLimit;
        protected BigInteger __storageLimit;
        protected BigInteger __epoch;
        protected BigInteger __chainId;

        public string signV2(byte[] privateKey)
        {

            this.chianId = __chainId.ToBytesForRLPEncoding();
            this.Gas = __gasLimit.ToByteArray();
            this._nonce = __nonce.ToByteArray();

            List<byte[]> raw = new List<byte[]> { this.__nonce.ToBytesForRLPEncoding(),this.__gasPrice.ToBytesForRLPEncoding(),this.__gasLimit.ToBytesForRLPEncoding(),this.to,
            this.Value,this.__storageLimit.ToBytesForRLPEncoding(),this.__epoch.ToBytesForRLPEncoding(),this.__chainId.ToBytesForRLPEncoding(),this.Data};

            var x1 = rlpEncode(raw).ToHex();
            var x2 = sha3Keccack.CalculateHash(rlpEncode(raw));//sha3
            var k = new EthECKey(privateKey, true);
            var x3 = k.SignAndCalculateV(x2);

        //   x3.V = new byte[] { 0x00 };


            List<object> rawWithRSV = new List<object> { raw, x3.V, x3.R, x3.S };
            var x4 = rlpEncode(rawWithRSV).ToHex();
            return "0x" + x4;
        }

        public string sign(byte[] privateKey, dynamic chainId)
        {
            var xxx22 = this.GasPrice;
            this.storageLimit = BitConverter.GetBytes(1).Clear();

            this.chianId = BitConverter.GetBytes(1).Clear();
            this.Gas = BitConverter.GetBytes(42035).Clear();

            List<byte[]> raw = new List<byte[]> { this.__nonce.ToBytesForRLPEncoding(),this.GasPrice,this.Gas,this.to,
            this.Value,this.storageLimit,this.__epoch.ToBytesForRLPEncoding(),this.chianId,this.Data};

            var x1 = rlpEncode(raw).ToHex();
            var x2 = sha3Keccack.CalculateHash(rlpEncode(raw));//sha3
            var k = new EthECKey("0x" + privateKey.ToHex());
            var x3 = k.Sign(x2);
            x3.V = new byte[] { 0x01 };

            List<object> rawWithRSV = new List<object> { raw, x3.V, x3.R, x3.S };
            var x4 = rlpEncode(rawWithRSV).ToHex();
            return "0x" + x4;
        }

        public object encode(bool includeSignature)
        {
            return rlpEncode(this);
        }
        public byte[] rlpEncode(object value)
        {
            //Array
            if (value.GetType().Name.Contains("List"))
            {
                return encodeArray((IEnumerable<object>)value);
            }
            else
            {
                return encodeBuffer((byte[])value);
            }
        }

        //List<byte[]>   List<byte[]>,,
        public byte[] encodeArray(IEnumerable<object> array)
        {
            List<byte> byteMerged = new List<byte>();
            foreach (var a in array)
            {
                byteMerged.AddRange(rlpEncode(a));
            }
            return encodeLength(byteMerged.Count, ARRAY_OFFSET).Add(byteMerged.ToArray()).ToArray();
        }
        public byte[] encodeBuffer(byte[] buffer)
        {
            if (buffer.Length == 1 && buffer[0] == 0)
            {
                buffer = new byte[] { };
            }
            return buffer.Length == 1 && buffer[0] < BUFFER_OFFSET ? buffer :
                encodeLength(buffer.Length, BUFFER_OFFSET).Add(buffer);
        }


        public byte[] encodeLength(int length, int offset)
        {
            if (length <= SHORT_RANGE)
            {
                return BitConverter.GetBytes(length + offset).Clear();
            }
            else
            {
                var lengthBuffer = BitConverter.GetBytes(length).Clear();
                var firstPart = BitConverter.GetBytes(offset + SHORT_RANGE + lengthBuffer.Length).Clear().ToList();
                firstPart.AddRange(lengthBuffer.ToList());
                return firstPart.ToArray();
            }
        }

        public int BUFFER_OFFSET = 128;
        public int SHORT_RANGE = 55;
        public int ARRAY_OFFSET = 0xc0;
        public byte[] GetRLPEncodedRaw()
        {
            return SimpleRlpSigner.GetRLPEncodedRaw();
        }

        public virtual void Sign(EthECKey key)
        {
            SimpleRlpSigner.Sign(key);
        }

        public void SetSignature(EthECDSASignature signature)
        {
            SimpleRlpSigner.SetSignature(signature);
        }

        protected static string ToHex(byte[] x)
        {
            if (x == null) return "0x";
            return x.ToHex();
        }
#if !DOTNET35
        public abstract Task SignExternallyAsync(IEthExternalSigner externalSigner);
#endif
    }
}