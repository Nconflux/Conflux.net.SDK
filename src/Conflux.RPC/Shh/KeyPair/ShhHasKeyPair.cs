using Conflux.Hex.HexConvertors.Extensions;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Conflux.RPC.Shh.KeyPair;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conflux.RPC.Shh.KeyPair
{
    public class ShhHasKeyPair : GenericRpcRequestResponseHandlerParamString<bool>, IShhHasKeyPair
    {
        public ShhHasKeyPair(IClient client) : base(client, ApiMethods.shh_hasKeyPair.ToString())
        {
        } 
    }
}