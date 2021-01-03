using Conflux.Geth.RPC.Miner;
using Conflux.JsonRpc.Client;
using Conflux.RPC;

namespace Conflux.Geth
{
    public class MinerApiService : RpcClientWrapper, IMinerApiService
    {
        public MinerApiService(IClient client) : base(client)
        {
            SetGasPrice = new MinerSetGasPrice(client);
            Start = new MinerStart(client);
            Stop = new MinerStop(client);
        }

        public IMinerSetGasPrice SetGasPrice { get; }
        public IMinerStart Start { get; }
        public IMinerStop Stop { get; }
    }
}