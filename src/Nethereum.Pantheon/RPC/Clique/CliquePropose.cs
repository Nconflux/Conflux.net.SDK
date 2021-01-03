using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.Clique
{
    /// <Summary>
    ///     Proposes adding or removing a signer with the specified address.
    /// </Summary>
    public class CliquePropose : RpcRequestResponseHandler<bool>, ICliquePropose
    {
        public CliquePropose(IClient client) : base(client, ApiMethods.clique_propose.ToString())
        {
        }

        public async Task<bool> SendRequestAsync(string address, bool addSigner, object id = null)
        {
            return await base.SendRequestAsync(id, address, addSigner);
        }

        public RpcRequest BuildRequest(string address, bool addSigner, object id = null)
        {
            return base.BuildRequest(id, address, addSigner);
        }
    }
}