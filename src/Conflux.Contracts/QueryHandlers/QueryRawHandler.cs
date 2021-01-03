﻿using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Transactions;

namespace Conflux.Contracts.QueryHandlers
{
#if !DOTNET35
    public class QueryRawHandler<TFunctionMessage> :
        QueryHandlerBase<TFunctionMessage>, IQueryHandler<TFunctionMessage, string> 
        where TFunctionMessage : FunctionMessage, new()
    {
        
        public QueryRawHandler(IClient client, string defaultAddressFrom = null, BlockParameter defaultBlockParameter = null):base(client, defaultAddressFrom, defaultBlockParameter)
        {

        }

        public QueryRawHandler(IEthCall ethCall, string defaultAddressFrom = null, BlockParameter defaultBlockParameter = null) : base(ethCall, defaultAddressFrom, defaultBlockParameter)
        {

        }


        public Task<string> QueryAsync(
            string contractAddress,
            TFunctionMessage contractFunctionMessage = null,
            BlockParameter block = null)
        {
            if (contractFunctionMessage == null) contractFunctionMessage = new TFunctionMessage();
            if (block == null) block = DefaultBlockParameter;
            FunctionMessageEncodingService.SetContractAddress(contractAddress);
            EnsureInitialiseAddress();
            var callInput = FunctionMessageEncodingService.CreateCallInput(contractFunctionMessage);
            return EthCall.SendRequestAsync(callInput, block);
        }
    }
#endif
}