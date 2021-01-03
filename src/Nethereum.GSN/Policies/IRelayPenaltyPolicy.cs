using Conflux.GSN.Models;
using System.Threading.Tasks;

namespace Conflux.GSN.Policies
{
    public interface IRelayPenaltyPolicy
    {
        Task PenalizeAsync(RelayOnChain relay);
    }
}
