using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts
{
    public class EventLog<T> : IEventLog
    {
        public EventLog(T eventObject, FilterLog log)
        {
            Event = eventObject;
            Log = log;
        }

        public T Event { get; }
        public FilterLog Log { get; }
    }
}