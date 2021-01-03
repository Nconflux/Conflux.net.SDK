using Conflux.JsonRpc.Client;
using Conflux.RPC.Shh.MessageFilter;

namespace Conflux.RPC.Shh
{
    public class ShhMessageFilter : RpcClientWrapper, IShhMessageFilter
    {
        public ShhMessageFilter(IClient client) : base(client)
        {
            NewMessageFilter = new ShhNewMessageFilter(client);
            DeleteMessageFilter = new ShhDeleteMessageFilter(client);
            GetFilterMessages = new ShhGetFilterMessages(client);
        }

        public IShhNewMessageFilter NewMessageFilter { get; private set; }

        public IShhDeleteMessageFilter DeleteMessageFilter { get; private set; }

        public IShhGetFilterMessages GetFilterMessages { get; private set; }
    }
}
