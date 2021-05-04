using Common.Logging;
using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;
using Conflux.Contracts.ContractHandlers;
using Conflux.RPC.Eth.DTOs;
using Conflux.Web3;
using Conflux.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Conflux.API.Builders
{
    public static class TranscationBuilder
    {
        public static IAbstractTranscationBuilder Create(string url, string contractAddress, string abi, string functionName) => new ParameterTranscationBuilder(url, contractAddress, abi, functionName);
        public static IAbstractTranscationBuilder<TFunction> Create<TFunction>(string url, string contractAddress) where TFunction : FunctionMessage, new()
            => new FunctionTranscationBuilder<TFunction>(url, contractAddress);
    }

    public abstract class AbstractTranscationBuilder<TInput> : ITranscationSender<TInput>, IAbstractTranscationBuilder<TInput> where TInput : class
    {
        protected IWeb3 _web3;
        protected string _url { get; }
        protected string _contractAddress { get; }
        protected ILog _log { get; private set; }
        protected AuthenticationHeaderValue _auth { get; private set; }
        protected BigInteger _amountToSend { get; private set; }
        protected BigInteger? _gas { get; private set; }
        protected BigInteger? _gasPrice { get; private set; }
        protected BigInteger? _storage { get; private set; }
        protected BigInteger? _nonce { get; private set; }
        protected BigInteger? _epochNumber { get; private set; }
        protected Account _account { get; private set; }

        protected AbstractTranscationBuilder(string url, string contractAddress)
        {
            this._url = url;
            this._contractAddress = contractAddress;
        }

        public AbstractTranscationBuilder<TInput> LogWith(ILog log)
        {
            this._log = log;
            return this;
        }
        public AbstractTranscationBuilder<TInput> Authentication(AuthenticationHeaderValue auth)
        {
            this._auth = auth;
            return this;
        }
        public AbstractTranscationBuilder<TInput> WithValue(BigInteger value)
        {
            this._amountToSend = value;
            return this;
        }
        public AbstractTranscationBuilder<TInput> GasLimit(BigInteger? gas)
        {
            this._gas = gas;
            return this;
        }
        public AbstractTranscationBuilder<TInput> WithGasPrice(BigInteger? gasPrice)
        {
            this._gasPrice = gasPrice;
            return this;
        }
        public AbstractTranscationBuilder<TInput> StorageLimit(BigInteger? storageLimit)
        {
            this._storage = storageLimit;
            return this;
        }
        public AbstractTranscationBuilder<TInput> WithNonce(BigInteger? nonce)
        {
            this._nonce = nonce;
            return this;
        }
        public AbstractTranscationBuilder<TInput> WithEpochNumber(BigInteger? epochNumber)
        {
            this._epochNumber = epochNumber;
            return this;
        }

        public virtual ITranscationSender<TInput> Build(Account account)
        {
            this._account = account;
            this._web3 = new Web3.Web3(account, _url, _log, _auth);
            return this;
        }

        public abstract Task<string> SendTranscationAsync(TInput parameters = null);
        public abstract Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(TInput parameters = null);
    }

    public class FunctionTranscationBuilder<TFunction> : AbstractTranscationBuilder<TFunction> where TFunction : FunctionMessage, new()
    {
        private ContractHandler _contractHandler;

        internal FunctionTranscationBuilder(string url, string contractAddress) : base(url, contractAddress) { }

        public override ITranscationSender<TFunction> Build(Account account)
        {
            base.Build(account);
            this._contractHandler = this._web3.Cfx.GetContractHandler(this._contractAddress);
            return this;
        }

        public override Task<string> SendTranscationAsync(TFunction parameters)
        {
            return this._contractHandler.SendRequestAsync(prepareParameter(parameters));
        }

        public override Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(TFunction parameters)
        {
            return this._contractHandler.SendRequestAndWaitForReceiptAsync(prepareParameter(parameters));
        }

        private TFunction prepareParameter(TFunction parameters)
        {
            if (parameters == null)
                parameters = new TFunction();
            parameters.AmountToSend = this._amountToSend;
            parameters.EpochNumber = this._epochNumber;
            parameters.FromAddress = this._account.Address;
            parameters.Gas = this._gas;
            parameters.GasPrice = this._gasPrice;
            parameters.Storage = this._storage;
            return parameters;
        }
    }

    public abstract class AbstractTranscationBuilder : ITranscationSender, IAbstractTranscationBuilder
    {
        protected IWeb3 _web3;
        protected string _url { get; }
        protected string _contractAddress { get; }
        protected ILog _log { get; private set; }
        protected AuthenticationHeaderValue _auth { get; private set; }
        protected BigInteger _amountToSend { get; private set; }
        protected BigInteger? _gas { get; private set; }
        protected BigInteger? _gasPrice { get; private set; }
        protected BigInteger? _storage { get; private set; }
        protected BigInteger? _nonce { get; private set; }
        protected BigInteger? _epochNumber { get; private set; }
        protected Account _account { get; private set; }

        protected AbstractTranscationBuilder(string url, string contractAddress)
        {
            this._url = url;
            this._contractAddress = contractAddress;
        }

        public AbstractTranscationBuilder LogWith(ILog log)
        {
            this._log = log;
            return this;
        }
        public AbstractTranscationBuilder Authentication(AuthenticationHeaderValue auth)
        {
            this._auth = auth;
            return this;
        }
        public AbstractTranscationBuilder WithValue(BigInteger value)
        {
            this._amountToSend = value;
            return this;
        }
        public AbstractTranscationBuilder GasLimit(BigInteger? gas)
        {
            this._gas = gas;
            return this;
        }
        public AbstractTranscationBuilder WithGasPrice(BigInteger? gasPrice)
        {
            this._gasPrice = gasPrice;
            return this;
        }
        public AbstractTranscationBuilder StorageLimit(BigInteger? storageLimit)
        {
            this._storage = storageLimit;
            return this;
        }
        public AbstractTranscationBuilder WithNonce(BigInteger? nonce)
        {
            this._nonce = nonce;
            return this;
        }
        public AbstractTranscationBuilder WithEpochNumber(BigInteger? epochNumber)
        {
            this._epochNumber = epochNumber;
            return this;
        }

        public virtual ITranscationSender Build(Account account)
        {
            this._account = account;
            this._web3 = new Web3.Web3(account, _url, _log, _auth);
            return this;
        }

        public abstract Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(params object[] parameters);
        public abstract Task<string> SendTranscationAsync(params object[] parameters);
    }
    public class ParameterTranscationBuilder : AbstractTranscationBuilder
    {
        public string ABI { get; }
        public string FunctionName { get; }
        protected Contracts.Contract Contract { get; private set; }
        protected Function Function { get; private set; }

        internal ParameterTranscationBuilder(string url, string contractAddress, string abi, string functionName) : base(url, contractAddress)
        {
            this.ABI = abi;
            this.FunctionName = functionName;
        }

        public override ITranscationSender Build(Account account)
        {
            base.Build(account);
            this.Contract = this._web3.Cfx.GetContract(this.ABI, this._contractAddress);
            this.Function = this.Contract.GetFunction(this.FunctionName);
            return this;
        }

        public override Task<string> SendTranscationAsync(params object[] parameters)
        {
            return this.Function.SendTransactionAsync(this._account.Address,
                this._gas.ToHexBigInteger(),
                this._gasPrice.ToHexBigInteger(),
                this._storage.ToHexBigInteger(),
                this._amountToSend.ToHexBigInteger(),
                this._epochNumber.ToHexBigInteger(),
                this._nonce.ToHexBigInteger(),
                parameters);
        }

        public override Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(params object[] parameters)
        {
            return this.Function.SendTransactionAndWaitForReceiptAsync(this._account.Address,
                this._gas.ToHexBigInteger(),
                this._gasPrice.ToHexBigInteger(),
                this._storage.ToHexBigInteger(),
                this._amountToSend.ToHexBigInteger(),
                this._epochNumber.ToHexBigInteger(),
                this._nonce.ToHexBigInteger(),
                null,
                parameters);
        }
    }
}
