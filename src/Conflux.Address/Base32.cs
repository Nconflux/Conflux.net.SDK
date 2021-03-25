// <copyright file="Base32.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Conflux.Address
{
    public class OldAddress
    {
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Network ID
        /// </summary>
        public string NetworkID { get; set; }
        /// <summary>
        /// Is Valid
        /// </summary>
        public bool IsValid { get; set; }
    }
    public enum NetworkType
    {
        cfx,
        cfxtest,
    }
    /// <summary>
    /// Base32 encoding/decoding functions.
    /// </summary>
    public sealed class Base32 : IBaseEncoder, IBaseStreamEncoder, INonAllocatingBaseEncoder
    {
        private const int bitsPerByte = 8;
        private const int bitsPerChar = 5;

        private static readonly Lazy<Base32> crockford = new Lazy<Base32>(() => new Base32(Base32Alphabet.Crockford));
        private static readonly Lazy<Base32> rfc4648 = new Lazy<Base32>(() => new Base32(Base32Alphabet.Rfc4648));
        private static readonly Lazy<Base32> extendedHex = new Lazy<Base32>(() => new Base32(Base32Alphabet.ExtendedHex));
        private static readonly Lazy<Base32> zBase32 = new Lazy<Base32>(() => new Base32(Base32Alphabet.ZBase32));
        private static readonly Lazy<Base32> geohash = new Lazy<Base32>(() => new Base32(Base32Alphabet.Geohash));

        /// <summary>
        /// Initializes a new instance of the <see cref="Base32"/> class with a
        /// custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base32(Base32Alphabet alphabet)
        {
            Alphabet = alphabet;
        }

        /// <summary>
        /// Gets Douglas Crockford's Base32 flavor with substitution characters.
        /// </summary>
        public static Base32 Crockford => crockford.Value;

        /// <summary>
        /// Gets RFC 4648 variant of Base32 coder.
        /// </summary>
        public static Base32 Rfc4648 => rfc4648.Value;

        /// <summary>
        /// Gets Extended Hex variant of Base32 coder.
        /// </summary>
        /// <remarks>Also from RFC 4648.</remarks>
        public static Base32 ExtendedHex => extendedHex.Value;

        /// <summary>
        /// Gets z-base-32 variant of Base32 coder.
        /// </summary>
        /// <remarks>This variant is used in Mnet, ZRTP and Tahoe-LAFS.</remarks>
        public static Base32 ZBase32 => zBase32.Value;

        /// <summary>
        /// Gets Geohash variant of Base32 coder.
        /// </summary>
        public static Base32 Geohash => geohash.Value;

        /// <summary>
        /// Gets the encoding alphabet.
        /// </summary>
        public Base32Alphabet Alphabet { get; }

        /// <inheritdoc/>
        public int GetSafeByteCountForDecoding(ReadOnlySpan<char> text)
        {
            return getAllocationByteCountForDecoding(text.Length - getPaddingCharCount(text));
        }

        /// <inheritdoc/>
        public int GetSafeCharCountForEncoding(ReadOnlySpan<byte> buffer)
        {
            return (((buffer.Length - 1) / bitsPerChar) + 1) * bitsPerByte;
        }

        /// <summary>
        /// Encode a byte array into a Base32 string without padding.
        /// </summary>
        /// <param name="bytes">Buffer to be encoded.</param>
        /// <returns>Encoded string.</returns>
        public string Encode(ReadOnlySpan<byte> bytes)
        {
            return Encode(bytes, padding: false);
        }

        /// <summary>
        /// Encode a byte array into a Base32 string.
        /// </summary>
        /// <param name="bytes">Buffer to be encoded.</param>
        /// <param name="padding">Append padding characters in the output.</param>
        /// <returns>Encoded string.</returns>
        public unsafe string Encode(ReadOnlySpan<byte> bytes, bool padding)
        {
            int bytesLen = bytes.Length;
            if (bytesLen == 0)
            {
                return string.Empty;
            }

            // we are ok with slightly larger buffer since the output string will always
            // have the exact length of the output produced.
            int outputLen = GetSafeCharCountForEncoding(bytes);
            string output = new string('\0', outputLen);
            fixed (byte* inputPtr = bytes)
            fixed (char* outputPtr = output)
            {
#pragma warning disable IDE0046 // Convert to conditional expression - prefer clarity
                if (!internalEncode(
                    inputPtr,
                    bytesLen,
                    outputPtr,
                    outputLen,
                    padding,
                    out int numCharsWritten))
                {
                    throw new InvalidOperationException("Internal error: couldn't calculate proper output buffer size for input");
                }

                return output[..numCharsWritten];
#pragma warning restore IDE0046 // Convert to conditional expression
            }
        }

        public unsafe List<byte> EncodeWithBytes(ReadOnlySpan<byte> bytes, bool padding)
        {
            Hashtable ht = new Hashtable();
            ht["a"] = 0x00;
            ht["b"] = 0x01;
            ht["c"] = 0x02;
            ht["d"] = 0x03;
            ht["e"] = 0x04;
            ht["f"] = 0x05;
            ht["g"] = 0x06;
            ht["h"] = 0x07;
            ht["j"] = 0x08;
            ht["k"] = 0x09;
            ht["m"] = 0x0a;
            ht["n"] = 0x0b;
            ht["p"] = 0x0c;
            ht["r"] = 0x0d;
            ht["s"] = 0x0e;
            ht["t"] = 0x0f;
            ht["u"] = 0x10;
            ht["v"] = 0x11;
            ht["w"] = 0x12;
            ht["x"] = 0x13;
            ht["y"] = 0x14;
            ht["z"] = 0x15;
            ht["0"] = 0x16;
            ht["1"] = 0x17;
            ht["2"] = 0x18;
            ht["3"] = 0x19;
            ht["4"] = 0x1a;
            ht["5"] = 0x1b;
            ht["6"] = 0x1c;
            ht["7"] = 0x1d;
            ht["8"] = 0x1e;
            ht["9"] = 0x1f;

            var str = Encode(bytes, false);
            List<byte> retList = new List<byte>();
            foreach (var s in str)
            {
                retList.Add(Convert.ToByte(ht[s.ToString()]));
            }
            return retList;
        }
        /// <summary>
        /// Decode a Base32 encoded string into a byte array.
        /// </summary>
        /// <param name="text">Encoded Base32 string.</param>
        /// <returns>Decoded byte array.</returns>
        public unsafe Span<byte> Decode(ReadOnlySpan<char> text)
        {
            int textLen = text.Length - getPaddingCharCount(text);
            int outputLen = getAllocationByteCountForDecoding(textLen);
            if (outputLen == 0)
            {
                return Array.Empty<byte>();
            }

            var outputBuffer = new byte[outputLen];

            fixed (byte* outputPtr = outputBuffer)
            fixed (char* inputPtr = text)
            {
                if (!internalDecode(inputPtr, textLen, outputPtr, outputLen, out _))
                {
                    throw new ArgumentException("Invalid input or output", nameof(text));
                }
            }

            return outputBuffer;
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream without padding.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        public void Encode(Stream input, TextWriter output)
        {
            Encode(input, output, padding: false);
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        /// <param name="padding">Whether to use padding at the end of the output.</param>
        public void Encode(Stream input, TextWriter output, bool padding)
        {
            StreamHelper.Encode(input, output, (buffer, lastBlock) =>
            {
                bool usePadding = lastBlock && padding;
                return Encode(buffer.Span, usePadding);
            });
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream without padding.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task EncodeAsync(Stream input, TextWriter output)
        {
            return EncodeAsync(input, output, padding: false);
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        /// <param name="padding">Whether to use padding at the end of the output.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task EncodeAsync(Stream input, TextWriter output, bool padding)
        {
            await StreamHelper.EncodeAsync(input, output, (buffer, lastBlock) =>
            {
                bool usePadding = lastBlock && padding;
                return Encode(buffer.Span, usePadding);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Decode a text stream into a binary stream.
        /// </summary>
        /// <param name="input">TextReader open on the stream.</param>
        /// <param name="output">Binary output stream.</param>
        public void Decode(TextReader input, Stream output)
        {
            StreamHelper.Decode(input, output, buffer => Decode(buffer.Span).ToArray());
        }

        /// <summary>
        /// Decode a text stream into a binary stream.
        /// </summary>
        /// <param name="input">TextReader open on the stream.</param>
        /// <param name="output">Binary output stream.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DecodeAsync(TextReader input, Stream output)
        {
            await StreamHelper.DecodeAsync(input, output, buffer => Decode(buffer.Span).ToArray())
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public bool TryEncode(ReadOnlySpan<byte> bytes, Span<char> output, out int numCharsWritten)
        {
            return TryEncode(bytes, output, padding: false, out numCharsWritten);
        }

        /// <summary>
        /// Encode to the given preallocated buffer.
        /// </summary>
        /// <param name="bytes">Input bytes.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="padding">Whether to use padding characters at the end.</param>
        /// <param name="numCharsWritten">Number of characters written to the output.</param>
        /// <returns>True if encoding is successful, false if the output is invalid.</returns>
        public unsafe bool TryEncode(
            ReadOnlySpan<byte> bytes,
            Span<char> output,
            bool padding,
            out int numCharsWritten)
        {
            int bytesLen = bytes.Length;
            if (bytesLen == 0)
            {
                numCharsWritten = 0;
                return true;
            }

            int outputLen = output.Length;

            fixed (byte* inputPtr = bytes)
            fixed (char* outputPtr = output)
            {
                return internalEncode(inputPtr, bytesLen, outputPtr, outputLen, padding, out numCharsWritten);
            }
        }

        /// <inheritdoc/>
        public unsafe bool TryDecode(ReadOnlySpan<char> input, Span<byte> output, out int numBytesWritten)
        {
            int inputLen = input.Length - getPaddingCharCount(input);
            if (inputLen == 0)
            {
                numBytesWritten = 0;
                return true;
            }

            int outputLen = output.Length;
            if (outputLen == 0)
            {
                numBytesWritten = 0;
                return false;
            }

            fixed (char* inputPtr = input)
            fixed (byte* outputPtr = output)
            {
                return internalDecode(inputPtr, inputLen, outputPtr, outputLen, out numBytesWritten);
            }
        }

        private unsafe bool internalEncode(
           byte* inputPtr,
           int bytesLen,
           char* outputPtr,
           int outputLen,
           bool padding,
           out int numCharsWritten)
        {
            string table = Alphabet.Value;
            char* pOutput = outputPtr;
            char* pOutputEnd = outputPtr + outputLen;
            byte* pInput = inputPtr;
            byte* pInputEnd = pInput + bytesLen;

            for (int bitsLeft = bitsPerByte, currentByte = *pInput, outputPad; pInput != pInputEnd;)
            {
                if (bitsLeft > bitsPerChar)
                {
                    bitsLeft -= bitsPerChar;
                    outputPad = currentByte >> bitsLeft;
                    *pOutput++ = table[outputPad];
                    if (pOutput > pOutputEnd)
                    {
                        goto Overflow;
                    }

                    currentByte &= (1 << bitsLeft) - 1;
                }

                int nextBits = bitsPerChar - bitsLeft;
                bitsLeft = bitsPerByte - nextBits;
                outputPad = currentByte << nextBits;
                if (++pInput != pInputEnd)
                {
                    currentByte = *pInput;
                    outputPad |= currentByte >> bitsLeft;
                    currentByte &= (1 << bitsLeft) - 1;
                }

                *pOutput++ = table[outputPad];
                if (pOutput > pOutputEnd)
                {
                    goto Overflow;
                }
            }

            if (padding)
            {
                char paddingChar = Alphabet.PaddingChar;
                while (pOutput != pOutputEnd)
                {
                    *pOutput++ = paddingChar;
                    if (pOutput > pOutputEnd)
                    {
                        goto Overflow;
                    }
                }
            }

            numCharsWritten = (int)(pOutput - outputPtr);
            return true;
            Overflow:
            numCharsWritten = (int)(pOutput - outputPtr);
            return false;
        }

        private static int getAllocationByteCountForDecoding(int textLenWithoutPadding)
        {
            return textLenWithoutPadding * bitsPerChar / bitsPerByte;
        }

        private int getPaddingCharCount(ReadOnlySpan<char> text)
        {
            char paddingChar = Alphabet.PaddingChar;
            int result = 0;
            int textLen = text.Length;
            while (textLen > 0 && text[--textLen] == paddingChar)
            {
                result++;
            }

            return result;
        }

        private unsafe bool internalDecode(
            char* inputPtr,
            int textLen,
            byte* outputPtr,
            int outputLen,
            out int numBytesWritten)
        {
            var table = Alphabet.ReverseLookupTable;
            int outputPad = 0;
            int bitsLeft = bitsPerByte;

            byte* pOutput = outputPtr;
            byte* pOutputEnd = pOutput + outputLen;
            char* pInput = inputPtr;
            char* pEnd = inputPtr + textLen;
            numBytesWritten = 0;
            while (pInput != pEnd)
            {
                char c = *pInput++;
                int b = table[c] - 1;
                if (b < 0)
                {
                    numBytesWritten = (int)(pOutput - outputPtr);
                    return false;
                }

                if (bitsLeft > bitsPerChar)
                {
                    bitsLeft -= bitsPerChar;
                    outputPad |= b << bitsLeft;
                    continue;
                }

                int shiftBits = bitsPerChar - bitsLeft;
                outputPad |= b >> shiftBits;
                if (pOutput >= pOutputEnd)
                {
                    Debug.WriteLine("Base32.internalDecode: output overflow");
                    return false;
                }

                *pOutput++ = (byte)outputPad;
                numBytesWritten++;
                b &= (1 << shiftBits) - 1;
                bitsLeft = bitsPerByte - shiftBits;
                outputPad = b << bitsLeft;
            }

            return true;
        }
        public static List<byte> StringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
            {
                hex = hex + "0";
            }
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToList();
        }
        public static string ByteArrayToString(Span<byte> bytes)
        {
            var afterBytes = new List<byte>();
            foreach (var b in bytes)
            {
                afterBytes.Add(b);
            }
            var str = BitConverter.ToString(afterBytes.ToArray()).Replace("-", "");
            return $@"0x{str.Substring(2, 40)}".ToLower();
        }

        //refer https://forum.conflux.fun/t/topic/4745
        public static OldAddress Decode(string address)
        {
            //remove description
            var tmpAddressList=address.ToLower().Split(":");
            if (tmpAddressList.Length>=2)
            {
                address = tmpAddressList[0] + ":" + address.ToLower().Split(":")[tmpAddressList.Length - 1];
            }
            OldAddress oldAddress = new OldAddress { IsValid = true };
            if (Regex.IsMatch(address, "[A-Z]") && Regex.IsMatch(address, "[a-z]"))
            {
                oldAddress.IsValid = false;
                //return oldAddress;
            }

            var arrAddress = address.Split(":");
            if (arrAddress.Length < 2)
            {
                oldAddress.IsValid = false;
                //return oldAddress;
            }

            switch (arrAddress[0].ToLower())
            {
                case "cfx":
                    oldAddress.NetworkID = "1029";
                    break;
                case "cfxtest":
                    oldAddress.NetworkID = "1";
                    break;
                default:
                    oldAddress.IsValid = false;
                    break;
            }
            var decodedBytes = Base32.Rfc4648.Decode(arrAddress[1].ToLower());
            List<byte> decodedByteList = new List<byte>();
            foreach (var decodedByte in decodedBytes)
            {
                decodedByteList.Add(decodedByte);
            }
            var str = ByteArrayToString(decodedBytes);
            if (Encode(str, oldAddress.NetworkID == "1" ? NetworkType.cfxtest : NetworkType.cfx) == address)
            {
                oldAddress.Address = str;
            }
            else
            {
                oldAddress.IsValid = false;
            }
            return oldAddress;
        }

        public static string Encode(string address, NetworkType type)
        {
            List<byte> sum = new List<byte> { 0 };

            var rawBytes = StringToByteArray(address.Replace("0x", ""));
            sum.AddRange(rawBytes);
            string firstPart = Base32.Rfc4648.Encode(sum.ToArray(), padding: false);
            var secondPartBytes = Base32.Rfc4648.EncodeWithBytes(sum.ToArray(), padding: false);
            var secondPart = Base32.Rfc4648.Encode(StringToByteArray(PolyMode(secondPartBytes, type).ToString("X")).ToArray(), padding: false);
            if (type == NetworkType.cfx)
            {
                return "cfx:" + firstPart + secondPart;
            }
            else
            {
                return "cfxtest:" + firstPart + secondPart;
            }
        }

        static long PolyMode(List<byte> input, NetworkType type)
        {
            //createa checksum input
            List<byte> inputSum = new List<byte>();
            if (type == NetworkType.cfx)
            {
                inputSum = new List<byte> { 0x03, 0x06, 0x18, 0x00, };
                inputSum.AddRange(input);
                inputSum.AddRange(new List<byte> { 0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, });
            }
            else if (type == NetworkType.cfxtest)
            {
                inputSum = new List<byte> { 0x03, 0x06, 0x18, 0x14, 0x05, 0x13, 0x14, 0x00 };
                inputSum.AddRange(input);
                inputSum.AddRange(new List<byte> { 0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, });
            }

            long c = 1;
            long zero = 0;
            foreach (var d in inputSum)
            {
                long c0 = c >> 35;
                c = ((c & 0x07ffffffff) << 5) ^ d;

                if ((c0 & 0x01) != zero) c ^= 0x98f2bc8e61;
                if ((c0 & 0x02) != zero) c ^= 0x79b76d99e2;
                if ((c0 & 0x04) != zero) c ^= 0xf33e5fb3c4;
                if ((c0 & 0x08) != zero) c ^= 0xae2eabe2a8;
                if ((c0 & 0x10) != zero) c ^= 0x1e4f43e470;
            }
            return c ^ 1;
        }
        static long PolyMode(List<byte> input)
        {
            //createa checksum input
            List<byte> inputSum = new List<byte>();

            inputSum.AddRange(input);


            long c = 1;
            long zero = 0;
            foreach (var d in inputSum)
            {
                long c0 = c >> 35;
                c = ((c & 0x07ffffffff) << 5) ^ d;

                if ((c0 & 0x01) != zero) c ^= 0x98f2bc8e61;
                if ((c0 & 0x02) != zero) c ^= 0x79b76d99e2;
                if ((c0 & 0x04) != zero) c ^= 0xf33e5fb3c4;
                if ((c0 & 0x08) != zero) c ^= 0xae2eabe2a8;
                if ((c0 & 0x10) != zero) c ^= 0x1e4f43e470;
            }
            return c ^ 1;
        }

    }
}