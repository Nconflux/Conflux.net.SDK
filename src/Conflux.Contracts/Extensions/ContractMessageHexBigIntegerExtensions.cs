using System.Numerics;
using Conflux.Contracts.CQS;
using Conflux.Hex.HexTypes;
using Conflux.Util;

namespace Conflux.Contracts
{
    public static class ContractMessageHexBigIntegerExtensions
    {
 
        /// <summary>
        /// Convert BigInteger to HexBigInteger, if input is null, return null;
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static HexBigInteger ToHexBigInteger(this BigInteger? input) => input?.ToHexBigInteger();

        /// <summary>
        /// Convert BigInteger to HexBigInteger
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HexBigInteger ToHexBigInteger(this BigInteger input) => new HexBigInteger(input);
 

        public static string SetDefaultFromAddressIfNotSet(this ContractMessageBase contractMessage, string defaultFromAdddress)
        {
            if (string.IsNullOrEmpty(contractMessage.FromAddress))
            {
                contractMessage.FromAddress = defaultFromAdddress;
            }
            return contractMessage.FromAddress;
        }

    }
}