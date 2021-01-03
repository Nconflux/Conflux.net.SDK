using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Conflux.RPC.Eth.Blocks
{
    public interface ILastConfirmedBlockNumberService
    {
        Task<BigInteger> GetLastConfirmedBlockNumberAsync(BigInteger? waitForConfirmedBlockNumber, CancellationToken cancellationToken);
    }
}