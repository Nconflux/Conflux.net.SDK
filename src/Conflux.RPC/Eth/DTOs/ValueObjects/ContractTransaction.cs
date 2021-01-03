using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.DTOs
{
    public class ContractTransactionVO
    {
        public ContractTransactionVO(string contractAddress, string code, Transaction transaction)
        {
            ContractAddress = contractAddress;
            Code = code;
            Transaction = transaction;
        }

        public string ContractAddress { get; private set; }
        public string Code { get; private set; }
        public Transaction Transaction { get; private set; }
    }
}
