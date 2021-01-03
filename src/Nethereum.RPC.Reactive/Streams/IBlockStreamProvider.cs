using System;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Reactive.Streams
{
    public interface IBlockStreamProvider
    {
        IObservable<BlockWithTransactionHashes> GetBlocksWithTransactionHashes();

        IObservable<BlockWithTransactions> GetBlocksWithTransactions();

        IObservable<BlockWithTransactionHashes> GetBlocksWithTransactionHashes(
            BlockParameter start,
            IObservable<BlockWithTransactionHashes> newBlockSource = null);

        IObservable<BlockWithTransactionHashes> GetBlocksWithTransactionHashes(
            BlockParameter start,
            BlockParameter end,
            IObservable<BlockWithTransactionHashes> newBlockSource = null);

        IObservable<BlockWithTransactions> GetBlocksWithTransactions(
            BlockParameter start,
            IObservable<BlockWithTransactions> newBlockSource = null);

        IObservable<BlockWithTransactions> GetBlocksWithTransactions(
            BlockParameter start,
            BlockParameter end,
            IObservable<BlockWithTransactions> newBlockSource = null);
    }
}