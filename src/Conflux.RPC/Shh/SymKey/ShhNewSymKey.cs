using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conflux.RPC.Shh.SymKey
{
    public class ShhNewSymKey : GenericRpcRequestResponseHandlerNoParam<string>, IShhNewSymKey
    {
        public ShhNewSymKey(IClient client) : base(client, ApiMethods.shh_newSymKey.ToString())
        {
        }
    }
}
