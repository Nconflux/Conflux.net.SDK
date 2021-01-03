using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conflux.RPC.Shh.KeyPair
{
    public class ShhDeleteKeyPair : GenericRpcRequestResponseHandlerParamString<bool>, IShhDeleteKeyPair
    {
        public ShhDeleteKeyPair(IClient client) : base(client, ApiMethods.shh_deleteKeyPair.ToString())
        {
        } 
    }
}
