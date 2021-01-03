﻿using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Admin
{
    /// <Summary>
    ///     Returns networking information about the node.
    ///     The information includes general information about the node and specific information from each running Ethereum
    ///     sub-protocol (for example, eth).
    /// </Summary>
    public class AdminNodeInfo : GenericRpcRequestResponseHandlerNoParam<JObject>, IAdminNodeInfo
    {
        public AdminNodeInfo(IClient client) : base(client, ApiMethods.admin_nodeInfo.ToString())
        {
        }
    }
}