using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Transactions;

namespace Conflux.Contracts.QueryHandlers
{
#if !DOTNET35

    public class QueryToDTOHandler<TFunctionMessage, TFunctionOutput> :
        QueryDecoderBaseHandler<TFunctionMessage, TFunctionOutput> where TFunctionMessage : FunctionMessage, new()
        where TFunctionOutput: IFunctionOutputDTO, new()
    {

        public QueryToDTOHandler(IClient client, string defaultAddressFrom = null, BlockParameter defaultBlockParameter = null) : base(client, defaultAddressFrom, defaultBlockParameter)
        {
            QueryRawHandler = new QueryRawHandler<TFunctionMessage>(client, defaultAddressFrom, defaultBlockParameter);
        }

        public QueryToDTOHandler(IEthCall ethCall, string defaultAddressFrom = null, BlockParameter defaultBlockParameter = null) : base(ethCall, defaultAddressFrom, defaultBlockParameter)
        {
            QueryRawHandler = new QueryRawHandler<TFunctionMessage>(ethCall, defaultAddressFrom, defaultBlockParameter);
        }

        protected override TFunctionOutput DecodeOutput(string output)
        {
            return QueryRawHandler.FunctionMessageEncodingService.DecodeDTOTypeOutput<TFunctionOutput>(output);
        }
    }
#endif
}