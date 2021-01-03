using Conflux.RPC.TransactionManagers;
using System;
using System.Collections.Generic;
using System.Text;
using Conflux.RPC.NonceServices;

namespace Conflux.RPC.Accounts
{
    public interface IAccount
    {
        string Address { get; }
        ITransactionManager TransactionManager { get; }

        INonceService NonceService { get; set; }
    }
}
