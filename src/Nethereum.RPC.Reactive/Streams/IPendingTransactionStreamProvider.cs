using System;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Reactive.Streams
{
    public interface IPendingTransactionStreamProvider
    {
        IObservable<Transaction> GetPendingTransactions();
    }
}