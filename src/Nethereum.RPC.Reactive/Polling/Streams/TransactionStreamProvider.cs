using System;
using System.Reactive.Linq;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Reactive.Streams;

namespace Conflux.RPC.Reactive.Polling.Streams
{
    public sealed class TransactionStreamProvider : ITransactionStreamProvider
    {
        private readonly IBlockStreamProvider BlockStreamProvider;

        public TransactionStreamProvider(
            IBlockStreamProvider blockStreamProvider) =>
            BlockStreamProvider = blockStreamProvider;

        public IObservable<Transaction> GetTransactions() => BlockStreamProvider
            .GetBlocksWithTransactions()
            .SelectMany(block => block.Transactions)
            .Publish()
            .RefCount();

        public IObservable<Transaction> GetTransactions(BlockParameter start) => BlockStreamProvider
            .GetBlocksWithTransactions(start)
            .SelectMany(block => block.Transactions)
            .Publish()
            .RefCount();

        public IObservable<Transaction> GetTransactions(BlockParameter start, BlockParameter end) => BlockStreamProvider
            .GetBlocksWithTransactions(start, end)
            .SelectMany(block => block.Transactions)
            .Publish()
            .RefCount();
    }
}