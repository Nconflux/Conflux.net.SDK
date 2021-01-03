using Conflux.JsonRpc.Client;
using Conflux.Quorum.RPC.DTOs;
using Conflux.RPC.Infrastructure;

namespace Conflux.Quorum.RPC
{
    public class QuorumNodeInfo : GenericRpcRequestResponseHandlerNoParam<NodeInfo>, IQuorumNodeInfo
    {
        public QuorumNodeInfo(IClient client) : base(client, ApiMethods.quorum_nodeInfo.ToString())
        {
        }
    }

    public interface IQuorumNodeInfo : IGenericRpcRequestResponseHandlerNoParam<NodeInfo>
    {

    }
}