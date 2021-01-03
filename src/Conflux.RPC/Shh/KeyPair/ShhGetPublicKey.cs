using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conflux.RPC.Shh.KeyPair
{
    public class ShhGetPublicKey : GenericRpcRequestResponseHandlerParamString<string>, IShhGetPublicKey
    {
        public ShhGetPublicKey(IClient client) : base(client, ApiMethods.shh_getPublicKey.ToString())
        {
        } 
    }
}
