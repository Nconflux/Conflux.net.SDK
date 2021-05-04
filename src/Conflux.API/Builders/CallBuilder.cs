using Common.Logging;
using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;
using Conflux.Contracts.ContractHandlers;
using Conflux.Web3;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Conflux.API.Builders
{
    public class CallBuilder
    {
        public static IAbstractCallBuilder<TReturn> CreateAsNoParameterSimpleResult<TReturn>(string url, string contractAddress, string abi, string functionName)
        {
            return new NoParameterCallBuilder<TReturn>(url, contractAddress, abi, functionName);
        }

        public static IAbstractCallBuilder<TReturn> CreateAsNoParameterObjectResult<TReturn>(string url, string contractAddress, string abi, string functionName) where TReturn : new()
        {
            return new ObjectNoParameterCallBuilder<TReturn>(url, contractAddress, abi, functionName);
        }

        public static IAbstractCallBuilder<TReturn, object[]> CreateAsSimpleResult<TReturn>(string url, string contractAddress, string abi, string functionName)
        {
            return new SimpleParameterCallBuilder<TReturn>(url, contractAddress, abi, functionName);
        }
        public static IAbstractCallBuilder<TReturn, TFunction> CreateAsSimpleResult<TReturn, TFunction>(string url, string contractAddress) where TFunction : FunctionMessage, new()
        {
            return new SimpleFunctionCallBuilder<TReturn, TFunction>(url, contractAddress);
        }

        public static IAbstractCallBuilder<TReturn, object[]> CreateAsObjectResult<TReturn>(string url, string contractAddress, string abi, string functionName) where TReturn : new()
        {
            return new ObjectParameterCallBuilder<TReturn>(url, contractAddress, abi, functionName);
        }

        public static IAbstractCallBuilder<TReturn, TFunction> CreateAsObjectResult<TReturn, TFunction>(string url, string contractAddress) where TFunction : FunctionMessage, new() where TReturn : IFunctionOutputDTO, new()
        {
            return new ObjectFunctionCallBuilder<TReturn, TFunction>(url, contractAddress);
        }
    }

    public abstract class AbstractCallBuilder<TReturn> : IContractCaller<TReturn>, IAbstractCallBuilder<TReturn>
    {
        protected IWeb3 web3;
        public string Url { get; }
        public string ContractAddress { get; }
        public ILog Log { get; protected set; }
        public AuthenticationHeaderValue Auth { get; protected set; }
        protected AbstractCallBuilder(string url, string contractAddress)
        {
            this.Url = url;
            this.ContractAddress = contractAddress;
        }

        public virtual IContractCaller<TReturn> Build()
        {
            this.web3 = new Web3.Web3(Url, Log, Auth);
            return this;
        }

        public AbstractCallBuilder<TReturn> LogWith(ILog log)
        {
            this.Log = log;
            return this;
        }
        public AbstractCallBuilder<TReturn> Authentication(AuthenticationHeaderValue auth)
        {
            this.Auth = auth;
            return this;
        }

        public abstract Task<TReturn> CallAsync();
    }

    public class NoParameterCallBuilder<TReturn> : AbstractCallBuilder<TReturn>
    {
        public string ABI { get; }
        public string FunctionName { get; }
        protected Contracts.Contract Contract { get; private set; }
        protected Function Function { get; private set; }

        internal NoParameterCallBuilder(string url, string contractAddress, string abi, string functionName) : base(url, contractAddress)
        {
            this.ABI = abi;
            this.FunctionName = functionName;
        }

        public override IContractCaller<TReturn> Build()
        {
            base.Build();
            this.Contract = this.web3.Cfx.GetContract(this.ABI, this.ContractAddress);
            this.Function = this.Contract.GetFunction(this.FunctionName);
            return this;
        }

        public override Task<TReturn> CallAsync()
        {
            return this.Function.CallAsync<TReturn>();
        }
    }

    public class ObjectNoParameterCallBuilder<TReturn> : NoParameterCallBuilder<TReturn> where TReturn : new()
    {

        internal ObjectNoParameterCallBuilder(string url, string contractAddress, string abi, string functionName) : base(url, contractAddress, abi, functionName) { }

        public override Task<TReturn> CallAsync()
        {
            return this.Function.CallDeserializingToObjectAsync<TReturn>();
        }
    }


    public abstract class AbstractCallBuilder<TReturn, TInput> : IContractCaller<TReturn, TInput>, IAbstractCallBuilder<TReturn, TInput>
    {
        protected IWeb3 web3;
        public string Url { get; }
        public string ContractAddress { get; }
        public ILog Log { get; protected set; }
        public AuthenticationHeaderValue Auth { get; protected set; }
        protected AbstractCallBuilder(string url, string contractAddress)
        {
            this.Url = url;
            this.ContractAddress = contractAddress;
        }

        public virtual IContractCaller<TReturn, TInput> Build()
        {
            this.web3 = new Web3.Web3(Url, Log, Auth);
            return this;
        }

        public AbstractCallBuilder<TReturn, TInput> LogWith(ILog log)
        {
            this.Log = log;
            return this;
        }
        public AbstractCallBuilder<TReturn, TInput> Authentication(AuthenticationHeaderValue auth)
        {
            this.Auth = auth;
            return this;
        }

        public abstract Task<TReturn> CallAsync(TInput input);
    }

    public class SimpleParameterCallBuilder<TReturn> : AbstractCallBuilder<TReturn, object[]>
    {
        public string ABI { get; }
        public string FunctionName { get; }
        protected Contracts.Contract Contract { get; private set; }
        protected Function Function { get; private set; }

        internal SimpleParameterCallBuilder(string url, string contractAddress, string abi, string functionName) : base(url, contractAddress)
        {
            this.ABI = abi;
            this.FunctionName = functionName;
        }

        public override IContractCaller<TReturn, object[]> Build()
        {
            base.Build();
            this.Contract = this.web3.Cfx.GetContract(this.ABI, this.ContractAddress);
            this.Function = this.Contract.GetFunction(this.FunctionName);
            return this;
        }

        public override Task<TReturn> CallAsync(params object[] parameters)
        {
            return this.Function.CallAsync<TReturn>(parameters);
        }
    }

    public class ObjectParameterCallBuilder<TReturn> : SimpleParameterCallBuilder<TReturn> where TReturn : new()
    {

        internal ObjectParameterCallBuilder(string url, string contractAddress, string abi, string functionName) : base(url, contractAddress, abi, functionName) { }

        public override Task<TReturn> CallAsync(params object[] parameters)
        {
            return this.Function.CallDeserializingToObjectAsync<TReturn>(parameters);
        }
    }

    public class SimpleFunctionCallBuilder<TReturn, TFunction> : AbstractCallBuilder<TReturn, TFunction> where TFunction : FunctionMessage, new()
    {
        public ContractHandler ContractHandler { get; protected set; }
        internal SimpleFunctionCallBuilder(string url, string contractAddress) : base(url, contractAddress) { }

        public override IContractCaller<TReturn, TFunction> Build()
        {
            base.Build();
            this.ContractHandler = this.web3.Cfx.GetContractHandler(this.ContractAddress);
            return this;
        }

        public override Task<TReturn> CallAsync(TFunction functionInput)
        {
            return this.ContractHandler.QueryAsync<TFunction, TReturn>(functionInput);
        }
    }

    public class ObjectFunctionCallBuilder<TReturn, TFunction> : SimpleFunctionCallBuilder<TReturn, TFunction> where TFunction : FunctionMessage, new() where TReturn : IFunctionOutputDTO, new()
    {
        internal ObjectFunctionCallBuilder(string url, string contractAddress) : base(url, contractAddress) { }


        public override Task<TReturn> CallAsync(TFunction functionInput)
        {
            return this.ContractHandler.QueryDeserializingToObjectAsync<TFunction, TReturn>(functionInput);
        }
    }
}
