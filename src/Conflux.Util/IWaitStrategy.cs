using System.Threading.Tasks;

namespace Conflux.Utils
{
    public interface IWaitStrategy
    {
        Task Apply(uint retryCount);
    }
}