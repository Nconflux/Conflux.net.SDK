using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conflux.RPC.Shh.SymKey
{
    public class ShhHasSymKey : GenericRpcRequestResponseHandlerParamString<bool>, IShhHasSymKey
    {
        public ShhHasSymKey(IClient client) : base(client, ApiMethods.shh_hasSymKey.ToString())
        {
        }
    }
}
