using Conflux.Contracts.CQS;
using Conflux.Util;
using Conflux.Hex.HexConvertors.Extensions;
namespace Conflux.Contracts
{
    public class ContractDeploymentMessage : ContractMessageBase
    {

        public ContractDeploymentMessage(string byteCode)
        {
            ByteCode = byteCode;
        }

        /// <summary>
        /// ByteCode (Compiled code) used for deployment
        /// </summary>
        public string ByteCode { get; internal set; }

    }
}