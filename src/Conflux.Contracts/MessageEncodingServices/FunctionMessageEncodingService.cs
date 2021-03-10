using System;
using Conflux.Contracts.CQS;
using Conflux.Contracts.Extensions;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts.MessageEncodingServices
{
    public class FunctionMessageEncodingService<TContractFunction> :
        IContractMessageTransactionInputCreator<TContractFunction>,
        IFunctionMessageEncodingService<TContractFunction> where TContractFunction : ContractMessageBase
    {
        protected FunctionBuilder<TContractFunction> FunctionBuilder { get; set; }

        public string ContractAddress => FunctionBuilder.ContractAddress;

        public string DefaultAddressFrom { get; set; }

        public void SetContractAddress(string address)
        {
            FunctionBuilder.ContractAddress = address;
        }

        public FunctionMessageEncodingService(string contractAddress = null, string defaultAddressFrom = null)
        {
            FunctionBuilder = new FunctionBuilder<TContractFunction>(contractAddress);
            DefaultAddressFrom = defaultAddressFrom;
        }

        public byte[] GetCallData(TContractFunction contractMessage)
        {
            return FunctionBuilder.GetDataAsBytes(contractMessage);
        }

        public byte[] GetCallDataHash(TContractFunction contractMessage)
        {
            return Util.Sha3Keccack.Current.CalculateHash(GetCallData(contractMessage));
        }

        public CallInput CreateCallInput(TContractFunction contractMessage)
        {
            return FunctionBuilder.CreateCallInput(contractMessage,
                contractMessage.SetDefaultFromAddressIfNotSet(DefaultAddressFrom),
                contractMessage.Gas.ToHexBigInteger(),
                contractMessage.AmountToSend.ToHexBigInteger());
        }

        public TransactionInput CreateTransactionInput(TContractFunction contractMessage)
        {
            var transactionInput = FunctionBuilder.CreateTransactionInput(contractMessage,
                contractMessage.SetDefaultFromAddressIfNotSet(DefaultAddressFrom),
                contractMessage.Gas.ToHexBigInteger(),
                contractMessage.GasPrice.ToHexBigInteger(),
                contractMessage.AmountToSend.ToHexBigInteger());
            transactionInput.Nonce = contractMessage.Nonce.ToHexBigInteger();
            return transactionInput;
        }

        public bool IsTransactionForFunction(Transaction transaction)
        {
            return FunctionBuilder.IsTransactionInputDataForFunction(transaction.Input);
        }

        public TContractFunction DecodeTransactionInput(TContractFunction contractMessageOuput, Transaction transaction)
        {
            if (!IsTransactionForFunction(transaction))
                throw new ArgumentException("The transaction given is not for the current function",
                    nameof(transaction));

            contractMessageOuput = DecodeInput(contractMessageOuput, transaction.Input);
            contractMessageOuput.Nonce = transaction.Nonce?.Value;
            contractMessageOuput.GasPrice = transaction.GasPrice?.Value;
            contractMessageOuput.AmountToSend = transaction.Value == null ? 0 : transaction.Value.Value;
            contractMessageOuput.Gas = transaction.Gas?.Value;
            contractMessageOuput.FromAddress = transaction.From;
            return contractMessageOuput;
        }

        public TReturn DecodeSimpleTypeOutput<TReturn>(string output)
        {
            return FunctionBuilder.DecodeSimpleTypeOutput<TReturn>(output);
        }

        public TReturn DecodeDTOTypeOutput<TReturn>(string output) where TReturn : new()
        {
            return FunctionBuilder.DecodeDTOTypeOutput<TReturn>(output);
        }

        public TContractFunction DecodeInput(TContractFunction function, string data)
        {

            return FunctionBuilder.DecodeFunctionInput(function, data);
        }

    }
}