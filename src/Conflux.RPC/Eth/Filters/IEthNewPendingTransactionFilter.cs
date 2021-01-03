using Conflux.Hex.HexTypes;
using Conflux.RPC.Infrastructure;

namespace Conflux.RPC.Eth.Filters
{
    public interface IEthNewPendingTransactionFilter : IGenericRpcRequestResponseHandlerNoParam<HexBigInteger>
    {

    }
}