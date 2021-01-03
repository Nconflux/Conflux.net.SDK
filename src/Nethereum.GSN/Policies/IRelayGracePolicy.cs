using Conflux.GSN.Models;
using System.Threading.Tasks;

namespace Conflux.GSN.Policies
{
    public interface IRelayGracePolicy
    {
        Task GraceAsync(RelayOnChain relay);
    }
}
