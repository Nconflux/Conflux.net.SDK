﻿using System;
using System.Reactive;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Reactive.Polling.Streams;

namespace Conflux.RPC.Reactive.Polling
{
    public static partial class Polling
    {
        public static IObservable<Transaction> GetTransactions(this IEthApiService eth,
            IObservable<Unit> poller = null) => new TransactionStreamProvider(
                new BlockStreamProvider(
                    poller ?? DefaultPoller,
                    eth.Filters,
                    eth.Blocks))
            .GetTransactions();

        public static IObservable<Transaction> GetTransactions(this IEthApiService eth,
            BlockParameter start,
            IObservable<Unit> poller = null) => new TransactionStreamProvider(
                new BlockStreamProvider(
                    poller ?? DefaultPoller,
                    eth.Filters,
                    eth.Blocks))
            .GetTransactions(start);

        public static IObservable<Transaction> GetTransactions(this IEthApiService eth,
            BlockParameter start,
            BlockParameter end,
            IObservable<Unit> poller = null) => new TransactionStreamProvider(
                new BlockStreamProvider(
                    poller ?? DefaultPoller,
                    eth.Filters,
                    eth.Blocks))
            .GetTransactions(start, end);
    }
}