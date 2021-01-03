using System;
using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.Transactions;
using Conflux.RPC.Eth.DTOs;
using System.Numerics;

namespace Conflux.RPC.TransactionManagers
{
    public class TransactionManager : TransactionManagerBase
    {
        public override BigInteger DefaultGas { get; set; }

        public TransactionManager(IClient client)
        {
            this.Client = client;
        }

#if !DOTNET35
        
        public override Task<string> SignTransactionAsync(TransactionInput transaction)
        {
            throw new InvalidOperationException("Default transaction manager cannot sign offline transactions");
        }

        public override Task<string> SendTransactionAsync(TransactionInput transactionInput)
        {
            if (Client == null) throw new NullReferenceException("Client not configured");
            if (transactionInput == null) throw new ArgumentNullException(nameof(transactionInput));
            SetDefaultGasPriceAndCostIfNotSet(transactionInput);
            return new EthSendTransaction(Client).SendRequestAsync(transactionInput);
        }
#endif
    }

}