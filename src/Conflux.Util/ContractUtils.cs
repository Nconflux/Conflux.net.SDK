using System.Numerics;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.RLP;

namespace Conflux.Util
{
    public static class ContractUtils
    {
        public static string CalculateContractAddress(string address, BigInteger nonce)
        {
            var sha3 = new Sha3Keccack();
            return  
                sha3.CalculateHash(RLP.RLP.EncodeList(RLP.RLP.EncodeElement(address.HexToByteArray()),
                    RLP.RLP.EncodeElement(nonce.ToBytesForRLPEncoding()))).ToHex().Substring(24);
        }
    }
}