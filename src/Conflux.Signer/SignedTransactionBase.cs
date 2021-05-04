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
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] bytes = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, bytes, offset, array.Length);
                offset += array.Length;
            }
            return bytes;
        }
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

        public byte[] Value { get; set; }

        public byte[] To { get; set; }

        public byte[] GasPrice { get; set; }

        public byte[] Gas { get; set; }

        public byte[] Storage { get; set; }

        public byte[] Data { set; get; }

        public byte[] Epoch { get; set; }
        public byte[] ChainId { get; set; }



        public EthECDSASignature Signature => SimpleRlpSigner.Signature;

        public abstract CfxECKey Key { get; }


        public byte[] GetRLPEncoded()
        {
            return SimpleRlpSigner.GetRLPEncoded();
        }


        public string signV2(byte[] privateKey)
        {
 


            byte[][] raw = {this.Nonce,
                this.GasPrice,
                this.Gas,
                this.To,
                this.Value,
                this.Storage,
                this.Epoch,
                this.ChainId,
                this.Data };

            byte[] rlpRaw = rlpEncode(raw);
            byte[] rlpRawHash = sha3Keccack.CalculateHash(rlpRaw);      //sha3
            CfxECKey ethECKey = new CfxECKey(privateKey, true);
            EthECDSASignature signature = ethECKey.SignAndCalculateV(rlpRawHash);
            string dataWithSign = rlpEncode(rlpRaw, signature).ToHex(true);
            return dataWithSign;
        }

        public byte[] rlpEncode(byte[] bytes) => encodeBuffer(bytes);
        public byte[] rlpEncode(params byte[][] byteArray) => encodeArray(byteArray);

        public byte[] rlpEncode(byte[] rlpRaw, EthECDSASignature signature)
        {
            byte[] byteMerged = Tool.Combine(rlpRaw, rlpEncode(signature.V), rlpEncode(signature.R), rlpEncode(signature.S));
            return Tool.Combine(encodeLength(byteMerged.Length, ARRAY_OFFSET), byteMerged);
        }

        public byte[] encodeArray(byte[][] array)
        {
            byte[] byteMerged = Tool.Combine(array.Select(p => rlpEncode(p)).ToArray());
            return Tool.Combine(encodeLength(byteMerged.Length, ARRAY_OFFSET), byteMerged);
        }

        public byte[] encodeBuffer(byte[] buffer)
        {
            if (buffer.Length == 1 && buffer[0] == 0)
                buffer = Array.Empty<byte>();
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
                byte[] lengthBuffer = length.ToBytesForRLPEncoding();
                byte[] firstPart = (offset + SHORT_RANGE + lengthBuffer.Length).ToBytesForRLPEncoding();
                return Tool.Combine(firstPart, lengthBuffer);
            }
        }

        public int BUFFER_OFFSET = 128;
        public int SHORT_RANGE = 55;
        public int ARRAY_OFFSET = 0xc0;
        public byte[] GetRLPEncodedRaw()
        {
            return SimpleRlpSigner.GetRLPEncodedRaw();
        }

        public virtual void Sign(CfxECKey key)
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