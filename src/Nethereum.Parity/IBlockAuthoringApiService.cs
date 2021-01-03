using Conflux.Parity.RPC.BlockAuthoring;

namespace Conflux.Parity
{
    public interface IBlockAuthoringApiService
    {
        IParityDefaultExtraData DefaultExtraData { get; }
        IParityExtraData ExtraData { get; }
        IParityGasCeilTarget GasCeilTarget { get; }
        IParityGasFloorTarget GasFloorTarget { get; }
        IParityMinGasPrice MinGasPrice { get; }
        IParityTransactionsLimit TransactionsLimit { get; }
    }
}