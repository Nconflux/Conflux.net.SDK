using Conflux.ABI.Decoders;
using Conflux.ABI.Encoders;

namespace Conflux.ABI
{
    public class BoolType : ABIType
    {
        public BoolType() : base("bool")
        {
            Decoder = new BoolTypeDecoder();
            Encoder = new BoolTypeEncoder();
        }
    }
}