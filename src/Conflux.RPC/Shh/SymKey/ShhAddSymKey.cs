using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conflux.RPC.Shh.SymKey
{
    public class ShhAddSymKey : GenericRpcRequestResponseHandlerParamString<string>, IShhAddSymKey
    {
        public ShhAddSymKey(IClient client) : base(client, ApiMethods.shh_addSymKey.ToString())
        {
        }
    }
}