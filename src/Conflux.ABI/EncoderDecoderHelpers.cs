using System.Linq;
using Conflux.ABI.Decoders;

namespace Conflux.ABI
{
    public class EncoderDecoderHelpers
    {
        public static int GetNumberOfBytes(byte[] encoded)
        {
            var intDecoder = new IntTypeDecoder();
            var numberOfBytesEncoded = encoded.Take(32);
            return intDecoder.DecodeInt(numberOfBytesEncoded.ToArray());
        }
    }
}