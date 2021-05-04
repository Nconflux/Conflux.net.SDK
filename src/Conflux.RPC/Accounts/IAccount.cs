using Conflux.RPC.TransactionManagers;
using System;
using System.Collections.Generic;
using System.Text;
using Conflux.RPC.NonceServices;
using Conflux.RPC.Eth;

namespace Conflux.RPC.Accounts
{
    public interface IAccount
    {
        public string Hex40Address { get; }
        string Address { get; }
        ITransactionManager TransactionManager { get; }

        INonceService NonceService { get; set; } 
    }
}
