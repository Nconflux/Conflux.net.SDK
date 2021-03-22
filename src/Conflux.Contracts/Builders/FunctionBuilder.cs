using Conflux.ABI.Model;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts
{
    public class FunctionBuilder : FunctionBuilderBase
    {

        public FunctionBuilder(string contractAddress, FunctionABI function)
            : base(contractAddress, function)
        {
        }

        public CallInput CreateCallInput(params object[] functionInput)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateCallInput(encodedInput);
        }

        public CallInput CreateCallInput(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateCallInput(encodedInput, from, gas, value);
        }

        public string GetData(params object[] functionInput)
        {
            return FunctionCallEncoder.EncodeRequest(FunctionABI.Sha3Signature, FunctionABI.InputParameters,
                functionInput);
        }

        public TransactionInput CreateTransactionInput(string from, HexBigInteger gas, HexBigInteger gasPrice, HexBigInteger storage,
            HexBigInteger value, params object[] functionInput)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateTransactionInput(encodedInput, from, gas, gasPrice, storage, value, null, null);
        }

        public TransactionInput CreateTransactionInput(string from, HexBigInteger gas, HexBigInteger gasPrice, HexBigInteger storage,
          HexBigInteger value, HexBigInteger epochNumber, HexBigInteger nonce, params object[] functionInput)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateTransactionInput(encodedInput, from, gas, gasPrice, storage, value, epochNumber, nonce);
        }

        public TransactionInput CreateTransactionInput(TransactionInput input, params object[] functionInput)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateTransactionInput(encodedInput, input);
        }
    }


    public class FunctionBuilder<TFunctionInput> : FunctionBuilderBase
    {
        public FunctionBuilder(string contractAddres) : base(contractAddres)
        {

            FunctionABI = ABITypedRegistry.GetFunctionABI<TFunctionInput>();
        }

        public FunctionBuilder(string contractAddress, FunctionABI function)
            : base(contractAddress, function)
        {
        }

        public CallInput CreateCallInputParameterless()
        {
            return CreateCallInput(FunctionCallEncoder.EncodeRequest(FunctionABI.Sha3Signature));
        }

        public CallInput CreateCallInput(TFunctionInput functionInput)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateCallInput(encodedInput);
        }

        public CallInput CreateCallInput(TFunctionInput functionInput, string from, HexBigInteger gas,
            HexBigInteger value)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateCallInput(encodedInput, from, gas, value);
        }

        public string GetData(TFunctionInput functionInput)
        {
            return FunctionCallEncoder.EncodeRequest(functionInput, FunctionABI.Sha3Signature);
        }

        public byte[] GetDataAsBytes(TFunctionInput functionInput)
        {
            return GetData(functionInput).HexToByteArray();
        }

        public TFunctionInput DecodeFunctionInput(TFunctionInput functionInput, TransactionInput transactionInput)
        {
            return FunctionCallDecoder.DecodeFunctionInput<TFunctionInput>(functionInput, FunctionABI.Sha3Signature, transactionInput.Data);
        }

        public TFunctionInput DecodeFunctionInput(TFunctionInput functionInput, string data)
        {
            return FunctionCallDecoder.DecodeFunctionInput<TFunctionInput>(functionInput, FunctionABI.Sha3Signature, data);
        }

        public TransactionInput CreateTransactionInput(TFunctionInput functionInput, string from)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateTransactionInput(encodedInput, from);
        }

        public TransactionInput CreateTransactionInput(TFunctionInput functionInput, string from, HexBigInteger gas,
           HexBigInteger storage, HexBigInteger value)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateTransactionInput(encodedInput, from, gas, storage, value);
        }

        public TransactionInput CreateTransactionInput(TFunctionInput functionInput, string from, HexBigInteger gas,
            HexBigInteger gasPrice, HexBigInteger storage, HexBigInteger value)
        {
            var encodedInput = GetData(functionInput);
            return base.CreateTransactionInput(encodedInput, from, gas, gasPrice, storage, value, null, null);
        }
    }
}