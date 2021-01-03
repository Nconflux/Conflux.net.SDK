namespace Conflux.BlockchainProcessing.ProgressRepositories
{
    public interface IBlockProgressRepositoryFactory
    {
        IBlockProgressRepository CreateBlockProgressRepository();
    }
}
