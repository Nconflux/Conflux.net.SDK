using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conflux.RPC.Shh.SymKey
{
    public class ShhDeleteSymKey : GenericRpcRequestResponseHandlerParamString<bool>, IShhDeleteSymKey
    {
        public ShhDeleteSymKey(IClient client) : base(client, ApiMethods.shh_deleteSymKey.ToString())
        {
        }
    }
}
