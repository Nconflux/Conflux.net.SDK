using System;
using System.Reactive;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Reactive.Polling.Streams;

namespace Conflux.RPC.Reactive.Polling
{
    public static partial class Polling
    {
        public static IObservable<Transaction> GetPendingTransactions(this IEthApiService eth,
            IObservable<Unit> poller = null) => new PendingTransactionStreamProvider(
                poller ?? DefaultPoller,
                eth.Filters,
                eth.Transactions)
            .GetPendingTransactions();
    }
}