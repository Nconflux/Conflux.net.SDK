using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Clique
{
    /// <Summary>
    ///     Returns current proposals.
    /// </Summary>
    public class CliqueProposals : GenericRpcRequestResponseHandlerNoParam<JObject>, ICliqueProposals
    {
        public CliqueProposals(IClient client) : base(client, ApiMethods.clique_proposals.ToString())
        {
        }
    }
}