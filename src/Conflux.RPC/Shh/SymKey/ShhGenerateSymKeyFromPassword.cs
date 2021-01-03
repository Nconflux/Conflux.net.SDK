using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conflux.RPC.Shh.SymKey
{
    public class ShhGenerateSymKeyFromPassword : GenericRpcRequestResponseHandlerParamString<string>, IShhGenerateSymKeyFromPassword
    {
        public ShhGenerateSymKeyFromPassword(IClient client) : base(client, ApiMethods.shh_generateSymKeyFromPassword.ToString())
        {
        }
    }
}
