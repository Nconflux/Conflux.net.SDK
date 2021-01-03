using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth
{
    public interface IDefaultBlock
    {
        BlockParameter DefaultBlock { get; set; }
    }
}