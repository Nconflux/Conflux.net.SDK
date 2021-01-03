﻿using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Services;
using Conflux.RPC.Reactive.Streams;

namespace Conflux.RPC.Reactive.Polling.Streams
{
    public sealed class PendingTransactionStreamProvider : IPendingTransactionStreamProvider
    {
        private readonly IEthApiFilterService FilterService;
        private readonly IObservable<Unit> Poller;
        private readonly IEthApiTransactionsService TransactionsService;

        public PendingTransactionStreamProvider(
            IObservable<Unit> poller,
            IEthApiFilterService filterService,
            IEthApiTransactionsService transactionsService)
        {
            Poller = poller;
            FilterService = filterService;
            TransactionsService = transactionsService;
        }

        public IObservable<Transaction> GetPendingTransactions() => ObservableExtensions
            .Using(
                async () => new DisposableFilter(await FilterService.NewPendingTransactionFilter.SendRequestAsync(), FilterService.UninstallFilter),
                filter => Observable
                    .FromAsync(ct => FilterService.GetFilterChangesForBlockOrTransaction.SendRequestAsync(filter.ID))
                    .SelectMany(transactionHashes => transactionHashes
                        .Select(hash => Observable.FromAsync(ct => TransactionsService.GetTransactionByHash.SendRequestAsync(hash)))
                        .ToObservable())
                    .SelectMany(x => x)
                    .Poll(Poller))
            .Publish()
            .RefCount();
    }
}