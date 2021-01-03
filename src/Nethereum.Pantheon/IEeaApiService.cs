using Conflux.Pantheon.RPC.EEA;

namespace Conflux.Pantheon
{
    public interface IEeaApiService
    {
        IEeaGetTransactionReceipt GetTransactionReceipt { get; }
        IEeaSendRawTransaction SendRawTransaction { get; }
    }
}