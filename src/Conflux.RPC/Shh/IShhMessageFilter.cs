using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Conflux.RPC.Shh.DTOs;
using Conflux.RPC.Shh.MessageFilter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conflux.RPC.Shh
{
    public interface IShhMessageFilter
    {
        IShhNewMessageFilter NewMessageFilter { get; }
        IShhDeleteMessageFilter DeleteMessageFilter { get; }
        IShhGetFilterMessages GetFilterMessages { get; }
    }
}
