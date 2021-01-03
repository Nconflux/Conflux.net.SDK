using System;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Reactive.Streams
{
    public interface ITransactionStreamProvider
    {
        IObservable<Transaction> GetTransactions();

        IObservable<Transaction> GetTransactions(
            BlockParameter start);

        IObservable<Transaction> GetTransactions(
            BlockParameter start,
            BlockParameter end);
    }
}