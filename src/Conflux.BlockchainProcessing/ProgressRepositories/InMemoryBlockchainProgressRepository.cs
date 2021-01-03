﻿using System.Numerics;
using System.Threading.Tasks;

namespace Conflux.BlockchainProcessing.ProgressRepositories
{
    public class InMemoryBlockchainProgressRepository : IBlockProgressRepository
    {
        public InMemoryBlockchainProgressRepository()
        {

        }

        public InMemoryBlockchainProgressRepository(BigInteger lastBlockProcessed)
        {
            LastBlockProcessed = lastBlockProcessed;
        }

        public BigInteger? LastBlockProcessed { get; private set;}

        public Task<BigInteger?> GetLastBlockNumberProcessedAsync() => Task.FromResult(LastBlockProcessed);

        public Task UpsertProgressAsync(BigInteger blockNumber)
        {
            LastBlockProcessed = blockNumber;
            return Task.FromResult(0);
        }
    }
}
