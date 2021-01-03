using Conflux.Contracts.CQS;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts.MessageEncodingServices
{
    public interface IContractMessageTransactionInputCreator<TContractMessage>: IDefaultAddressService
        where TContractMessage : ContractMessageBase
    {
        TransactionInput CreateTransactionInput(TContractMessage contractMessage);
        CallInput CreateCallInput(TContractMessage contractMessage);
    }


    public interface IDefaultAddressService
    {
        string DefaultAddressFrom { get; set; }
    }
}