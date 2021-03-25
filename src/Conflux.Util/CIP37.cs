using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Conflux.Util
{
    public static class CIP37
    {
        private const string b32Array = "abcdefghjkmnprstuvwxyz0123456789";
        private const string hexAlphabet = "0123456789abcdef";
        private static IReadOnlyDictionary<char, byte> dictB32Invers = b32Array.Select((p, index) => (p, idx: (byte)index)).ToDictionary(p => p.p, p => p.idx);

        public static bool IsHex40Address(string address)
        {
            return address.Length == (address.StartsWith("0x") ? 42 : 40);
        }

        public static string Hex40ToCIP37(string hex40Addr, uint? chainId = null) => Hex40ToCIP37(hex40Addr, GetPrefixByChainId(chainId));

        public static string Hex40ToCIP37(string hex40Addr, string networkPrefix, bool withAddressType = false)
        {
            GetPrefixBytes(networkPrefix);
            string hexPubAddr = hex40Addr.Substring(2);
            byte[] pubAddrBytes = StringToBytes(hexPubAddr);
            string addrType = GetAddressType(pubAddrBytes);

            byte[] preloadStrBytes = ConcatArrays(new byte[] { 0x0 }, pubAddrBytes);
            byte[] preloadBytes = BytesToB32Bytes(preloadStrBytes);
            byte[] preloadBytesRev = B32BytesToBytes(preloadBytes);

            byte[] checkSumPreload = ConcatArrays(networkPrefix.Select(p => (byte)(p & 0x1f)).ToArray(), new byte[] { 0x0 }, preloadBytes, new byte[8]);
            ulong checkSum = PolyMode(checkSumPreload);
            byte[] checkSumB32 = GetCheckSumB32(checkSum);
            byte[] addressB32 = ConcatArrays(preloadBytes, checkSumB32);
            string B32AddressString = B32BytesToString(addressB32);
            return withAddressType ? $"{networkPrefix}:{addrType}:{B32AddressString}" : $"{networkPrefix}:{B32AddressString}";
        }

        public static byte[] CIP37ToRawBytes(string cip37Addr)
        {
            string[] parts = cip37Addr.ToLower().Split(":");
            if (parts.Length < 2)
                throw new FormatException("Input format not correct");
            string networkPrefix = parts[0];
            GetPrefixBytes(networkPrefix);
            string payloadRaw = parts[parts.Length - 1];
            byte[] bytePayload = StringToB32Bytes(payloadRaw);
            byte[] checkPayloadRaw = ConcatArrays(networkPrefix.Select(p => (byte)(p & 0x1f)).ToArray(), new byte[] { 0x0 }, bytePayload);
            ulong checkSum = PolyMode(checkPayloadRaw);
            if (checkSum != 0)
                throw new Exception("Checksum validation failed");
            byte[] addressB32 = new byte[34];
            Array.Copy(bytePayload, addressB32, addressB32.Length);
            byte[] address = B32BytesToBytes(addressB32);
            byte[] addressByte = new byte[20];
            Buffer.BlockCopy(address, 1, addressByte, 0, 20);
            return addressByte;
        }

        public static byte[] CIP37ToEncodedBytes(string cip37Addr)
        {
            byte[] address = CIP37ToRawBytes(cip37Addr);
            byte[] addressByte = new byte[32];
            Buffer.BlockCopy(address, 0, addressByte, 12, 20);
            return addressByte;
        } 

        public static string CIP37ToHex40(string cip37Addr)
        {
            byte[] hex40 = CIP37ToRawBytes(cip37Addr); 
            return $"0x{BytesToString(hex40)}";
        }

        private static Regex regexNetN = new Regex("^net(\\d+)$");

        public static string GetPrefixByChainId(BigInteger? chainId)
        {
            if (!chainId.HasValue || chainId == 1029)
                return "cfx";
            if (chainId == 1)
                return "cfxtest";
            return $"net{chainId}";
        }

        private static byte[] GetPrefixBytes(string prefix)
        {
            uint ret;
            if (string.Equals("cfx", prefix))
                ret = 1029;
            else if (string.Equals("cfxtest", prefix))
                ret = 1;
            else
            {
                Match match = regexNetN.Match(prefix);
                if (match.Success)
                {
                    if (!uint.TryParse(match.Groups[1].Value, out ret))
                        throw new Exception("Private Conflux network id not parsable");
                    if (ret == 1 || ret == 1029)
                        throw new Exception("Private Conflux network id cannot be 1 or 1029");
                }
                else throw new Exception("Conflux network prefix inacceptable");
            }
            return BitConverter.GetBytes(ret);
        }

        private static string GetAddressType(byte[] pubAddr)
        {
            switch ((pubAddr[0] & 0xf0) >> 4)
            {
                case 0: return "type.builtin";
                case 1: return "type.user";
                case 8: return "type.contract";
                default: throw new Exception("Unknow address type");
            }
        }

        private static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }

        private static byte[] GetCheckSumB32(ulong checksum)
        {
            byte[] ret = new byte[8];
            for (int index = 0; index < ret.Length; ++index)
                ret[ret.Length - index - 1] = (byte)(0x1f & (checksum >> (index * 5)));
            return ret;
        }

        private static string B32BytesToString(byte[] bytes)
        {
            StringBuilder sBuilder = new StringBuilder();
            foreach (byte oneByte in bytes)
                sBuilder.Append(b32Array[oneByte]);
            return sBuilder.ToString();
        }

        private static byte[] StringToB32Bytes(string source)
        {
            byte[] b32Bytes = new byte[source.Length];
            for (int index = 0; index < source.Length; ++index)
                b32Bytes[index] = dictB32Invers[source[index]];
            return b32Bytes;
        }

        private static byte[] BytesToB32Bytes(byte[] bytes)
        {
            List<byte> results = new List<byte>();
            for (int bitIndex = 0; bitIndex < bytes.Length * 8; bitIndex += 5)
            {
                int dualbyte = bytes[bitIndex / 8] << 8;
                if (bitIndex / 8 + 1 < bytes.Length)
                    dualbyte |= bytes[bitIndex / 8 + 1];
                dualbyte = 0x1f & (dualbyte >> (16 - bitIndex % 8 - 5));
                results.Add((byte)dualbyte);
            }
            return results.ToArray();
        }

        private static byte[] B32BytesToBytes(byte[] base32)
        {
            List<byte> output = new List<byte>();
            for (int bitIndex = 0; bitIndex < base32.Length * 5; bitIndex += 8)
            {
                int dualbyte = base32[bitIndex / 5] << 10;
                if (bitIndex / 5 + 1 < base32.Length)
                    dualbyte |= base32[bitIndex / 5 + 1] << 5;
                if (bitIndex / 5 + 2 < base32.Length)
                    dualbyte |= base32[bitIndex / 5 + 2];
                dualbyte = 0xff & (dualbyte >> (15 - bitIndex % 5 - 8));
                output.Add((byte)(dualbyte));
            }
            return output.ToArray();
        }

        private static ulong PolyMode(byte[] data)
        {
            ulong c = 1;
            foreach (byte oneByte in data)
            {
                byte c0 = (byte)(c >> 35);
                c = ((c & 0x07ffffffff) << 5) ^ oneByte;
                if ((c0 & 0x01) != 0) c ^= 0x98f2bc8e61;
                if ((c0 & 0x02) != 0) c ^= 0x79b76d99e2;
                if ((c0 & 0x04) != 0) c ^= 0xf33e5fb3c4;
                if ((c0 & 0x08) != 0) c ^= 0xae2eabe2a8;
                if ((c0 & 0x10) != 0) c ^= 0x1e4f43e470;
            }
            return c ^ 1;
        }

        private static string BytesToString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            foreach (byte B in Bytes)
            {
                Result.Append(hexAlphabet[B >> 4]);
                Result.Append(hexAlphabet[B & 0xF]);
            }
            return Result.ToString();
        }


        private static byte[] StringToBytes(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");
            byte[] arr = new byte[hex.Length >> 1];
            for (int i = 0; i < hex.Length >> 1; ++i)
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            return arr;
        }

        private static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }
}
