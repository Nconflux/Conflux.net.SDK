using Conflux.GSN.Models;
using System.Collections.Generic;

namespace Conflux.GSN.Policies
{
    public interface IRelayPriorityPolicy
    {
        IEnumerable<RelayOnChain> Execute(IEnumerable<RelayOnChain> relays);
    }
}
