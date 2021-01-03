using Conflux.RPC.Eth.Compilation;

namespace Conflux.RPC.Eth.Services
{
    public interface IEthApiCompilerService
    {
        IEthCompileLLL CompileLLL { get; }
        IEthCompileSerpent CompileSerpent { get; }
        IEthCompileSolidity CompileSolidity { get; }
        IEthGetCompilers GetCompilers { get; }
    }
}