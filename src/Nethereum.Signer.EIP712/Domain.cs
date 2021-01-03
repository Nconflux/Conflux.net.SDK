using System.Numerics;

namespace Conflux.Signer.EIP712
{
    public class Domain
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public BigInteger? ChainId { get; set; }

        public string VerifyingContract { get; set; }

        public byte[] Salt { get; set; }
    }
}