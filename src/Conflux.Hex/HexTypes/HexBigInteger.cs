using System;
using System.Numerics;
using Conflux.Hex.HexConvertors;
using Newtonsoft.Json;

namespace Conflux.Hex.HexTypes
{
    [JsonConverter(typeof(HexRPCTypeJsonConverter<HexBigInteger, BigInteger>))]
    public class HexBigInteger : HexRPCType<BigInteger>
    {
        public HexBigInteger(string hex) : base(new HexBigIntegerBigEndianConvertor(), hex)
        {
        }

        public HexBigInteger(BigInteger value) : base(value, new HexBigIntegerBigEndianConvertor())
        {
        }

        public static HexBigInteger Zero { get; } = new HexBigInteger(0);

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}